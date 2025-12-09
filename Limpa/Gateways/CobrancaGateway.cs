using Limpa.Comm;
using Limpa.Entities;

namespace Limpa.Gateways;

internal class CobrancaGateway(IDataSource dataSource)
{
    internal List<Cobranca> ObterCobrancasSacado(Pessoa sacado, DateTime dataInicio)
    {
        List<CobrancaDto> cobrancas = dataSource.GetCobrancaList(sacado.Identificacao.ToString(), dataInicio);
        return null;    // TODO implementar
    }

    public void Incluir(Cobranca cobranca)
    {
        var cobrancaDto = new CobrancaDto(
            cobranca.Cedente.Identificacao.ToString(),
            cobranca.Sacado.Identificacao.ToString(),
            cobranca.Valor,
            cobranca.DataVencimento,
            cobranca.DataRegistro
        );
        try
        {
            dataSource.NewCobranca(cobrancaDto);
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao incluir o cobranca", ex);
        }

        return;
    }
}