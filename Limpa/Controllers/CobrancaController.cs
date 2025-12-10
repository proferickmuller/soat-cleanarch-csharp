using Limpa.Comm;
using Limpa.Gateways;
using Limpa.Presenters;
using Limpa.UseCases;
using System.Security.Cryptography.X509Certificates;

namespace Limpa.Controllers;

public class CobrancaController(IDataSource dataSource)
{
    public PessoaResponse NovaPessoa(PessoaRequest request)
    {
        var pessoaGateway = new PessoaGateway(dataSource);
        var useCase = new NovaPessoaUseCase(pessoaGateway);
        
        try
        {
            var pessoa = useCase.Run(request.Nome, request.Cpf);
            return PessoaPresenter.ToResponse(null, pessoa);
        }
        catch (Exception e)
        {
            var errorData = new ErrorData(Mensagem: e.Message);
            return PessoaPresenter.ToResponse(errorData, null);
        }
    }

    public PessoaResponse PessoaPorCpf(string cpf)
    {
        var pessoaGateway = new PessoaGateway(dataSource);
        var useCase = new PessoaPorCpfUseCase(pessoaGateway);

        var pessoa = useCase.Run(cpf);

        if (pessoa == null)
        {
            var errorData = new ErrorData(Mensagem: "Pessoa não encontrada.");
            return PessoaPresenter.ToResponse(errorData, null);
        }
        return PessoaPresenter.ToResponse(null, pessoa);
    }

    public PessoaResponse RemoverPessoa(string cpf)
    {
        var pessoaGateway = new PessoaGateway(dataSource);
        var useCase = new RemoverPessoaUseCase(pessoaGateway);

        try
        {
            useCase.Run(cpf);
            return PessoaPresenter.ToResponse(null, null);
        }
        catch (Exception e)
        {
            var errorData = new ErrorData(Mensagem: e.Message);
            return PessoaPresenter.ToResponse(errorData, null);
        }
    }
}