using System.Globalization;
using CsvHelper;

namespace LimpaCLI;

using Limpa.Comm;

public class CsvDataSource : IDataSource
{
    public class PessoaCsvRecord
    {
        public string Identificador { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
    }
    
    public class CobrancaCsvRecord
    {
        public string IdentificacaoCedente { get; set; }
        public string IdentificacaoSacado { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime DataRegistro { get; set; }
        public DateTime? DataPagamento { get; set; }
    }
    
    List<PessoaCsvRecord> _pessoas;
    List<CobrancaCsvRecord> _cobrancas;

    public CsvDataSource(string path)
    {
        using (var reader = new StreamReader(path))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            _pessoas = csv.GetRecords<PessoaCsvRecord>().ToList();
        }
    }
    
    public PessoaDto? GetPessoa(string? cpf = null, string? identificacao = null)
    {
        PessoaCsvRecord? pessoa = null;
        if (!string.IsNullOrEmpty(cpf))
        {
             pessoa =  _pessoas.FirstOrDefault(p => p.Cpf == cpf);
        } 
        else if (!string.IsNullOrEmpty(identificacao))
        {
            
             pessoa =  _pessoas.FirstOrDefault(p => p.Identificador == identificacao);
        }
        else
        {
            return null;
        }
        
        if (pessoa == null)
        {
            return null;
        }

        PessoaDto pessoaDto = new PessoaDto()
        {
            Cpf = pessoa.Cpf, Identificacao = pessoa.Identificador, Nome = pessoa.Nome
        };
        return pessoaDto;
    }

    public bool PessoaCheckIdentificacao(string identificacao)
    {
        return _pessoas.Any(p => p.Identificador == identificacao);
    }

    public List<CobrancaDto> GetCobrancaList(string sacado, DateTime? dataInicio)
    {
        return new List<CobrancaDto>();
    }

    public void NewCobranca(CobrancaDto cobrancaDto)
    {
        return;
    }

    public void NewPessoa(PessoaDto pessoaDto)
    {
        var pessoaCsvRecord = new PessoaCsvRecord()
        {
            Cpf = pessoaDto.Cpf,
            Identificador = pessoaDto.Identificacao,
            Nome = pessoaDto.Nome
        };
        _pessoas.Add(pessoaCsvRecord);
    }
}