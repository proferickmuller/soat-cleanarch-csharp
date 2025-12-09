using Limpa.Comm;
using Limpa.Gateways;
using Limpa.Presenters;
using Limpa.UseCases;

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
}