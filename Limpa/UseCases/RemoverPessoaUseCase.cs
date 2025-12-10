using Limpa.Gateways;
using Serilog;
using Serilog.Core;

namespace Limpa.UseCases;

internal class RemoverPessoaUseCase
{
    private readonly PessoaGateway _pessoaGateway;
    private readonly Logger _logger;

    public RemoverPessoaUseCase(PessoaGateway pessoaGateway)
    {
        _pessoaGateway = pessoaGateway;
        this._logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
    }

    public void Run(string cpf)
    {
        var pessoaExistente = _pessoaGateway.ObterPorCpf(cpf);
        if (pessoaExistente == null)
        {
            _logger.Error("Pessoa com Cpf: {cpf} não encontrada.", cpf);
            throw new Exception("Pessoa não encontrada");
        }
        _pessoaGateway.Remover(pessoaExistente.Cpf);
        _logger.Information("Pessoa com Cpf: {cpf} removida com sucesso.", cpf);
    }
}

