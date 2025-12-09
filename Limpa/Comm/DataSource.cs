using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Limpa.Comm;

namespace Limpa.Comm;

public interface IDataSource
{
    PessoaDto? GetPessoa(string? cpf = null, string? identificacao = null);
    bool PessoaCheckIdentificacao(string identificacao);
    List<CobrancaDto> GetCobrancaList(string sacado, DateTime? dataInicio);
    void NewCobranca(CobrancaDto cobrancaDto);
    void NewPessoa(PessoaDto pessoaDto);
}