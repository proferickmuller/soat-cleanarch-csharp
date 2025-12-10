/*  
  Esse código de exemplo usa o Csv como fonte de dados, e tenta registrar a mesma pessoa duas vezes. 
  A primeira retorna como esperado, e a segunda retorna com erro.
*/
var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
var path = Path.Combine(desktopPath, "ArquivoCSV", "pessoas.csv");   // Defina em path qual o caminho do arquivo csv

var csvDataSource = new LimpaCLI.CsvDataSource(path);
var controller = new Limpa.Controllers.CobrancaController(csvDataSource);
var request = new Limpa.Comm.PessoaRequest(Nome: "Nome de Pessoa", Cpf: "111.111.111-11");
var retorno = controller.NovaPessoa(request);

Console.WriteLine(retorno);

var retorno2 = controller.NovaPessoa(request);

Console.WriteLine(retorno2);