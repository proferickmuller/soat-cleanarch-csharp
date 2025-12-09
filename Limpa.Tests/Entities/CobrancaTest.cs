using System;
using JetBrains.Annotations;
using Limpa.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Limpa.Tests.Entities;

[TestClass]
[TestSubject(typeof(Cobranca))]
public class CobrancaTest
{
    Pessoa _pessoaCedente;
    Pessoa _pessoaSacada;

    [TestInitialize]
    public void SetupTest()
    {
        _pessoaCedente = Pessoa.Create(Ulid.NewUlid(), "ErickC", "456");
        _pessoaSacada = Pessoa.Create(Ulid.NewUlid(), "ErickS", "123");
    }

    [TestMethod]
    public void NovaCobrancaOk()
    {

        var c = Cobranca.Create(
            identificacao: "1", _pessoaCedente, _pessoaSacada, Convert.ToDecimal(100), DateTime.Now.AddDays(90),
            dataRegistro: DateTime.Now
        );
        Assert.IsNotNull(c);
    }

    [TestMethod]
    public void NovaCobrancaRegistroInvalido()
    {      
        var ex = Assert.Throws<DataRegistroInvalidoException>(() =>
        {
            var c = Cobranca.Create(
                identificacao: "1", _pessoaCedente, _pessoaSacada, Convert.ToDecimal(100), DateTime.Now.AddDays(90),
                dataRegistro: DateTime.Now.AddDays(95)
            );
        });
    }
    
    [TestMethod]
    public void NovaCobrancaVencimentoInvalido()
    {      
        var ex = Assert.Throws<DataVencimentoInvalidoException>(() =>
        {
            var c = Cobranca.Create(
                identificacao: "1", _pessoaCedente, _pessoaSacada, Convert.ToDecimal(100), DateTime.Now.AddDays(-1),
                dataRegistro: DateTime.Now
            );
        });
    }
}