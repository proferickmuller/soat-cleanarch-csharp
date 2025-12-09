using Limpa;

var csvDataSource = new LimpaCLI.CsvDataSource();
var controller = new Limpa.Controllers.CobrancaController(csvDataSource);
var request = new Limpa.Comm.PessoaRequest(Nome: "João da Silva", Cpf: "999.888.777-66");
var retorno = controller.NovaPessoa(request);

Console.WriteLine(retorno);

var retorno2 = controller.NovaPessoa(request);

Console.WriteLine(retorno2);

// Console.WriteLine("Hello, World!");