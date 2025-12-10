using Limpa.Comm;
using Limpa.Entities;

namespace Limpa.Gateways
{
    internal class PessoaGateway(IDataSource dataSource)
    {
        private readonly IDataSource DataSource = dataSource;

        internal Pessoa? ObterPorCpf(string cpf)
        {
            var pessoaDto = DataSource.GetPessoa(cpf: cpf);
            if (pessoaDto is null)
            {
                return null;
            }
            
            var pessoa = Pessoa.Create(
                identificacao: Ulid.Parse(pessoaDto.Identificacao), 
                nome: pessoaDto.Nome, 
                cpf: pessoaDto.Cpf);
            return pessoa;
        }

        internal Pessoa? ObterPorIdentificacao(Ulid identificacao)
        {
            var pessoaDto = DataSource.GetPessoa(identificacao: identificacao.ToString());
            if (pessoaDto is null)
            {
                return null;
            }
            
            var pessoa = Pessoa.Create(
                identificacao: Ulid.Parse(pessoaDto.Identificacao), 
                nome: pessoaDto.Nome, 
                cpf: pessoaDto.Cpf);
            return pessoa;
        }

        public bool VerificarIdentificacaoExistente(Ulid novaIdentificacao)
        {
            return DataSource.PessoaCheckIdentificacao(novaIdentificacao.ToString());
            
        }

        public void Registrar(Pessoa pessoa)
        {
            var pessoaDto = new PessoaDto
            {
                Identificacao = pessoa.Identificacao.ToString(),
                Nome = pessoa.Nome,
                Cpf = pessoa.Cpf
            };
            try
            {
                DataSource.NewPessoa(pessoaDto);
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        internal void Remover(string cpf)
        {
            DataSource.PessoaDeleteByCpf(cpf);
        }
    }
}
