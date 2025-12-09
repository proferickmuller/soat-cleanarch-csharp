using System.Security.Cryptography;
using System.Text;
using Limpa.Comm;
using Limpa.Entities;
using Limpa.Gateways;

namespace Limpa.UseCases;

internal class RegistrarCobrancaUseCase(
    PessoaGateway pessoaGateway,
    CobrancaGateway cobrancaGateway)
{
    public Cobranca Run(NovaCobrancaDto novaCobrancaDto)
    {
        // ver se as pessoas existem 
        var cedente = pessoaGateway.ObterPorIdentificacao(
            Ulid.Parse(novaCobrancaDto.IdentificacaoCedente)
        );
        if (cedente == null)
        {
            throw new ArgumentException(nameof(novaCobrancaDto.IdentificacaoCedente));
        }

        var sacado = pessoaGateway.ObterPorIdentificacao(
            Ulid.Parse(novaCobrancaDto.IdentificacaoSacado)
        );
        if (sacado == null)
        {
            throw new ArgumentException(nameof(novaCobrancaDto.IdentificacaoCedente));
        }

        // ver se o valor de pagamento é válido
        //   feito na criação do objeto de cobrança

        // ver se a pessoa pode registrar a cobranca (nao tem nenhuma divida atrasada) 
        var cobrancasAbertasParaCedente = cobrancaGateway.ObterCobrancasSacado(
            sacado: sacado,
            dataInicio: DateTime.Now);
        if (cobrancasAbertasParaCedente.Count >= 0)
        {
            throw new ArgumentException("Cedente tem dívidas em aberto, portanto não pode registrar cobrança");
        }

        string diaHoje = DateTime.Now.ToString("yyyyMMddHHmmss");

        // criar uma identificacao para a cobranca, vamos partir do principio que a identificacao nunca será repetida.
        string identificacao = $"{cedente.Identificacao.ToString()}{sacado.Identificacao.ToString()}{diaHoje}";

        var cobranca = Cobranca.Create(
            identificacao: identificacao, 
            cedente: cedente, 
            sacado: sacado, 
            valor: novaCobrancaDto.Valor,
            dataVencimento: novaCobrancaDto.DataVencimento);

        try
        {
            cobrancaGateway.Incluir(cobranca);
        }
        catch (Exception e)
        {
            throw new Exception("Erro ao registrar cobranca: ", e);
        }


        return cobranca;
    }
}