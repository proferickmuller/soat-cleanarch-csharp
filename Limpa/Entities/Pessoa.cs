namespace Limpa.Entities
{
    public class Pessoa
    {
        private string _nome;
        private string _cpf;
        
        public Ulid Identificacao { get; set; }

        public string Nome
        {
            get => _nome;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Campo Nome não pode estar vazio.");
                }
                _nome = value;
            }
        }

        public string Cpf
        {
            get =>  _cpf;
            set
            {
                // validar se o numero do cpf está válido. 
                
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Campo Nome não pode estar vazio.");
                }
                
                _cpf = value;
            }
        }

        public static Pessoa Create(Ulid identificacao, string nome, string cpf)
        {
            var p = new Pessoa
            {
                Identificacao = identificacao,
                Nome = nome,
                Cpf = cpf
            };

            return p;
        }

    }
}
