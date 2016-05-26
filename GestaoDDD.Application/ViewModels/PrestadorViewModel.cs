using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace GestaoDDD.Application.ViewModels
{
    public class PrestadorViewModel
    {
        [Key]
        [ScaffoldColumn(false)]
        public int pres_Id { get; set; }

        [Required(ErrorMessage = "Preencha o campo nome")]

        [DisplayName("Nome")]
        public string pres_nome { get; set; }

        [Required(ErrorMessage = "Preencha o campo.")]
        [DisplayName("Cpf/Cnpj")]
        public string pres_cpf_cnpj { get; set; }


        [Required(ErrorMessage = "Preencha o campo endereço.")]
        [DisplayName("Endereço")]
        public string pres_endereco { get; set; }

        [Required(ErrorMessage = "Informe o raio distância que deseja receber os orçamentos")]
        [DisplayName("Raio")]
        public string pres_raio_recebimento { get; set; }

        [Required(ErrorMessage = "Preencha o e-mail")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Insira um email válido.")]
        [DisplayName("Email")]
        public string pres_email { get; set; }

        [DisplayName("Telefone residencial.")]
        [DataType(DataType.PhoneNumber)]
        public string pres_telefone_residencial { get; set; }


        [DisplayName("Telefone celular.")]
        [DataType(DataType.PhoneNumber)]
        public string pres_telefone_celular { get; set; }

        [DisplayName("Status")]
        public EnumStatus status { get; set; }

        public string UsuarioId { get; set; }

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
