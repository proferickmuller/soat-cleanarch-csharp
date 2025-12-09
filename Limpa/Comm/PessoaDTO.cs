using System;
using System.Collections.Generic;
using System.Text;

namespace Limpa.Comm;

public class PessoaDto()
{
    public required string Identificacao { get; set; }
    public required string Nome { get; set; }
    public required string Cpf { get; set; }
}