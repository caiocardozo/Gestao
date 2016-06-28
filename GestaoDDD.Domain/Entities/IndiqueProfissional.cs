using EnumClass = GestaoDDD.Domain.Entities.NoSql;

namespace GestaoDDD.Domain.Entities
{
    public class IndiqueProfissional : Entidade
    {
        public int Id { get; set; }
        public string Nome_Profissional { get; set; }
        public string Telefone { get; set; }
        public string Email_Empresa { get; set; }
        public EnumClass.EnumEstados Estado { get; set; }
        public string Cidade { get; set; }
        public string  Nome { get; set; }
        public string Email_Solicitante { get; set; }
        public virtual Servico Servico { get; set; }
    }

   
}


