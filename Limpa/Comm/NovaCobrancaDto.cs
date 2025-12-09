namespace Limpa.Comm;

public record NovaCobrancaDto(
    string IdentificacaoCedente, string IdentificacaoSacado, 
    decimal Valor, DateTime DataVencimento, DateTime DataRegistro
    );