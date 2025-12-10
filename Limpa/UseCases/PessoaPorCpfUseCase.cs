using Limpa.Gateways;

namespace Limpa.UseCases;

internal class PessoaPorCpfUseCase
{
    private PessoaGateway pessoaGateway;

    public PessoaPorCpfUseCase(PessoaGateway pessoaGateway)
    {
        this.pessoaGateway = pessoaGateway;
    }

    public Entities.Pessoa? Run(string cpf)
    {
        var pessoa = pessoaGateway.ObterPorCpf(cpf);
        return pessoa;
    }
}
