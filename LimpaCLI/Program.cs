using Limpa;

var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
var path = Path.Combine(desktopPath, "ArquivoCSV", "pessoas.csv");
        
var csvDataSource = new LimpaCLI.CsvDataSource(path);
var controller = new Limpa.Controllers.CobrancaController(csvDataSource);
var request = new Limpa.Comm.PessoaRequest(Nome: "João da Silva", Cpf: "999.888.777-66");
var retorno = controller.NovaPessoa(request);

Console.WriteLine(retorno);

var retorno2 = controller.NovaPessoa(request);

Console.WriteLine(retorno2);