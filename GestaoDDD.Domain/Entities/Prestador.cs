using System.Runtime.Serialization;

namespace GestaoDDD.Domain.Entities
{
    public class Prestador : Entidade
    {
        public int pres_Id { get; set; }
        public string pres_nome { get; set; }
        public string pres_cpf_cnpj { get; set; }
        public string pres_endereco { get; set; }
        public string pres_raio_recebimento { get; set; }
        public string pres_email { get; set; }
        public string pres_telefone_residencial { get; set; }
        public string pres_telefone_celular { get; set; }
        public EnumStatus status { get; set; }

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
