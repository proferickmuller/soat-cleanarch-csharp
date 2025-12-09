namespace Limpa.Comm;

public record DadosPessoaResponse(string Nome, string Cpf);
public record PessoaResponse(ErrorData? Error, DadosPessoaResponse? DadosPessoa);
