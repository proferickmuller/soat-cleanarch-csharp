using System;
using JetBrains.Annotations;
using Limpa.Comm;
using Limpa.Entities;
using Limpa.Gateways;
using Limpa.UseCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Limpa.Tests.UseCases;

[TestClass]
[TestSubject(typeof(NovaPessoaUseCase))]
public class NovaPessoaUseCaseTest
{

    Mock<IDataSource> _dataSourceMock;
    PessoaGateway _pessoaGateway;
    
    [TestInitialize]
    public void TestInitialize()
    {
        _dataSourceMock = new Mock<IDataSource>();
        _dataSourceMock.Setup(s => s.PessoaCheckIdentificacao( It.IsAny<string>() )).Returns(false);
        _dataSourceMock.Setup(s => s.NewPessoa(It.IsAny<PessoaDto>())).Verifiable();
    }
    
    [TestMethod]
    public void criacaoOk()
    {
        _dataSourceMock.Setup(s => s.GetPessoa(cpf: It.IsAny<string>())).Returns((PessoaDto)null);
        _pessoaGateway = new PessoaGateway(_dataSourceMock.Object);
        
        
        var g = _pessoaGateway;
        var useCase = new NovaPessoaUseCase(g);
        var p = useCase.Run("Erick", "123");
        Assert.AreEqual("Erick", p.Nome);
        Assert.AreEqual("123", p.Cpf);
    }
    
    [TestMethod]
    public void pessoaComCpfExistente()
    {
        var pd = new PessoaDto()
        {
            Nome = "Erick",
            Cpf = "123",
            Identificacao = Ulid.NewUlid().ToString()
        };
        _dataSourceMock.Setup(s => s.GetPessoa(cpf: It.IsAny<string>())).Returns(pd);
        _pessoaGateway = new PessoaGateway(_dataSourceMock.Object);
        
        var ex = Assert.Throws<PessoaCpfJaExistenteException>(() =>
        {
            var g = _pessoaGateway;
            var useCase = new NovaPessoaUseCase(g);
            var p = useCase.Run("Erick", "123");
        });
        
    }
}