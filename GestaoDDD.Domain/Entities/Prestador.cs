using System.Runtime.Serialization;

namespace GestaoDDD.Domain.Entities
{
    public class Prestador : Entidade
    {
        public int pres_Id { get; set; }

        public string pres_Nome { get; set; }

        public string pres_Cpf_Cnpj { get; set; }

        public string pres_Endereco { get; set; }

        public string pres_Raio_Recebimento { get; set; }

        public string pres_Email { get; set; }

        public string pres_Telefone_Residencial { get; set; }

        public string pres_Telefone_Celular { get; set; }

        public EnumStatus status { get; set; }

        public string UsuarioId { get; set; }
        //public virtual Usuario UsuarioId { get; set; }

    }

    [DataContract]
    public enum EnumStatus
    {
        Ativo,
        Inativo,
        Orcamento_bloqueado,
        Orcamento_liberado
    }
}
