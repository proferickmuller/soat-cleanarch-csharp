namespace Limpa.Comm;

public record CobrancaDto(
    string IdentificacaoCedente,
    string IdentificacaoSacado,
    decimal Valor,
    DateTime DataVencimento,
    DateTime DataRegistro,
    DateTime? DataPagamento = null
);
