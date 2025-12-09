using Limpa.Comm;
using Limpa.Entities;

namespace Limpa.Presenters;

public class PessoaPresenter
{
    public static PessoaResponse ToResponse(ErrorData? erro, Pessoa? pessoa)
    {
        return erro != null ? 
            new PessoaResponse(erro, null) : 
            new PessoaResponse(null, new DadosPessoaResponse(Nome: pessoa.Nome, Cpf: pessoa.Cpf));
    }
}