using JetBrains.Annotations;
using Limpa.Comm;
using LimpaCLI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LimpaCLI.Tests;

[TestClass]
[TestSubject(typeof(CsvDataSource))]
public class CsvDataSourceTest
{
    

    [TestMethod]
    public void AbreArquivoOk()
    {
        var csv = new CsvDataSource();
        var pessoa = csv.GetPessoa(cpf: "12345678900");
        Assert.IsNull(pessoa);
    }
    
    [TestMethod]
    public void EncontraPessoaCpf()
    {
        var csv = new CsvDataSource();
        var pessoa = csv.GetPessoa(cpf: "123.456.789-01");
        Assert.IsNotNull(pessoa);
        Assert.AreEqual("Ana Paula Silva", pessoa.Nome);
    }
    
    [TestMethod]
    public void EncontraPessoaIdentificacao()
    {
        var csv = new CsvDataSource();
        var pessoa = csv.GetPessoa(identificacao: "01J9Z3M6A7B2C4D5E6F7G8H9JK");
        Assert.IsNotNull(pessoa);
        Assert.AreEqual("Ana Paula Silva", pessoa.Nome);
    }

    [TestMethod]
    public void ExisteIdentificacao()
    {
        var csv = new CsvDataSource();
        var encontrado = csv.PessoaCheckIdentificacao("01J9Z3M6A7B2C4D5E6F7G8H9JK");
        Assert.IsTrue(encontrado);
        
        encontrado = csv.PessoaCheckIdentificacao("XXJ9Z3M6A7B2C4D5E6F7G8H9JK");
        Assert.IsFalse(encontrado);
    }
    
    [TestMethod]
    public void NovaPessoa()
    {
        var novaPessoa = new PessoaDto()
        {
            Cpf = "999.888.777-66",
            Identificacao = "ZZZ9Z3M6A7B2C4D5E6F7G8H9ZZ",
            Nome = "João da Silva"
        };
        
        
        var csv = new CsvDataSource();
        csv.NewPessoa(novaPessoa);
        
        var pessoa = csv.GetPessoa(cpf: novaPessoa.Cpf);
        Assert.IsNotNull(pessoa);
        Assert.AreEqual(novaPessoa.Nome, pessoa.Nome);
    }
}