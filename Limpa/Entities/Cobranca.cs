namespace Limpa.Entities;

public class Cobranca
{
    private Cobranca() { }

    public required Pessoa Cedente { get; set; }    // quem cobra
    public required Pessoa Sacado { get; set; }      // quem paga    

    public required string Identificacao { get; set; }

    public required decimal Valor
    {
        get;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(Valor));
            }

            field = value;
        }
    }

    public required DateTime DataVencimento
    {
        get;
        set
        {
            if (value <= DateTime.Now)
            {
                throw new DataVencimentoInvalidoException(nameof(DataVencimento));
            }

            field = value;
        }
    }

    public required DateTime DataRegistro
    {
        get;
        set
        {

            if (value >= DataVencimento)
            {
                throw new DataRegistroInvalidoException(nameof(DataRegistro));
            }
            field = value;
        }
    }

    public DateTime? DataPagamento
    {
        get;
        set
        {
            // o pagamento NUNCA pode ser antes da DataRegistro e da DataVencimento
            if (value <= DateTime.Now && (value <= DataRegistro || value <= DataVencimento))
            {
                throw new ArgumentOutOfRangeException(nameof(DataPagamento));
            }
            field = value;
        }
    }



    public static Cobranca Create(
        string identificacao,
        Pessoa cedente,
        Pessoa sacado,
        decimal valor,
        DateTime dataVencimento,
        DateTime? dataRegistro = null,
        DateTime? dataPagamento = null)
    {
        return new Cobranca
        {
            Identificacao = identificacao,
            Cedente = cedente,
            Sacado = sacado,
            Valor = valor,
            DataVencimento = dataVencimento,
            DataRegistro = dataRegistro ?? DateTime.Now,
            DataPagamento = dataPagamento
        };

    }
}



public class DataRegistroInvalidoException : Exception
{
    public DataRegistroInvalidoException(string msg) : base(msg) { }
    public DataRegistroInvalidoException(string msg, Exception inner) : base(msg, inner) { }
    public DataRegistroInvalidoException() { }
}

public class DataVencimentoInvalidoException : Exception
{
    public DataVencimentoInvalidoException(string msg) : base(msg) { }
    public DataVencimentoInvalidoException(string msg, Exception inner) : base(msg, inner) { }
    public DataVencimentoInvalidoException() { }
}