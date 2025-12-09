using System.Diagnostics;
using System.Globalization;
using Limpa.Entities;
using Limpa.Gateways;
using Serilog;
using Serilog.Core;

namespace Limpa.UseCases;

internal class NovaPessoaUseCase
{
    private readonly PessoaGateway _pessoaGateway;
    private readonly Logger _logger;

    public NovaPessoaUseCase(PessoaGateway pessoaGateway)
    {
        _pessoaGateway = pessoaGateway;
        this._logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
    }

    public Pessoa Run(string nome, string cpf)
    {
        // este cpf já está registrado? 
        var pessoaExistente = _pessoaGateway.ObterPorCpf(cpf);
        if (pessoaExistente != null)
        {
            _logger.Error("Pessoa com Cpf: {cpf} já cadastrada.", cpf);
            
            throw new PessoaCpfJaExistenteException("CPF já registrado");
        }
        _logger.Information("Nome: {nome}, Cpf: {cpf}", nome, cpf);
        
        Ulid? novaIdentificacao = null;
        while (novaIdentificacao is null)
        {
            novaIdentificacao = Ulid.NewUlid();
            if (_pessoaGateway.VerificarIdentificacaoExistente(novaIdentificacao.Value))
            {
                novaIdentificacao = null;
            }
            _logger.Information("Identificacao: {novaIdentificacao}, Nome: {nome}, Cpf: {cpf}", novaIdentificacao.ToString(), nome, cpf);
        }

        var pessoa = new Pessoa
        {
            Nome = nome,
            Cpf = cpf,
            Identificacao = novaIdentificacao.Value
        };

        _pessoaGateway.Registrar(pessoa);

        return pessoa;
    }
}

internal class PessoaCpfJaExistenteException : Exception
{
    public PessoaCpfJaExistenteException(string msg) : base(msg){}

    public PessoaCpfJaExistenteException(string msg, Exception inner) : base(msg, inner) { }
    
    public PessoaCpfJaExistenteException() { }
}