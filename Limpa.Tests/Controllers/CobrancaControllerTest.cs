using System;
using System.Reflection;
using JetBrains.Annotations;
using Limpa.Comm;
using Limpa.Controllers;
using Limpa.UseCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Limpa.Tests.Controllers;

[TestClass]
[TestSubject(typeof(CobrancaController))]
public class CobrancaControllerTest
{
    private Mock<IDataSource> _dataSourceMock;

    [TestInitialize]
    public void TestInitialize()
    {
        _dataSourceMock = new Mock<IDataSource>();
        _dataSourceMock.Setup(s => s.PessoaCheckIdentificacao( It.IsAny<string>() )).Returns(false);
        _dataSourceMock.Setup(s => s.NewPessoa(It.IsAny<PessoaDto>())).Verifiable();
    }

    [TestMethod]
    public void TestOk()
    {
        var controller = new CobrancaController(_dataSourceMock.Object);
        var pr = new PessoaRequest("Erick", "1234");
        var ret = controller.NovaPessoa(pr);
        
        Assert.AreEqual("Erick", ret.DadosPessoa.Nome);
        Assert.AreEqual("1234", ret.DadosPessoa.Cpf);
    }
    
    [TestMethod]
    public void TestPessoaCpfExistente()
    {
        var p = new PessoaDto()
        {
            Cpf = "1234", Identificacao = Ulid.NewUlid().ToString(), Nome = "Erick"
        };
        _dataSourceMock.Setup(s => s.GetPessoa(It.IsAny<string>())).Returns(p);
        var controller = new CobrancaController(_dataSourceMock.Object);
        var pr = new PessoaRequest("Erick", "1234");
        var ret = controller.NovaPessoa(pr);

        Assert.IsNotNull(ret.Error);
        Assert.IsNull(ret.DadosPessoa);
    }
}