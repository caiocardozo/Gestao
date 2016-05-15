using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GestaoDDD.Application.ViewModels
{
    public class PrestadorUsuarioViewModel
    {
        [Key]
        [ScaffoldColumn(false)]
        public int pres_Id { get; set; }

        [Required(ErrorMessage = "Preencha o campo nome")]
        [DisplayName("Nome")]
        public string pres_nome { get; set; }

        [Required(ErrorMessage = "Preencha o campo CPF/CNPJ.")]
        [DisplayName("Cpf/Cnpj")]
        public string pres_cpf_cnpj { get; set; }


        [Required(ErrorMessage = "Preencha o campo endereço.")]
        [DisplayName("Endereço")]
        public string pres_endereco { get; set; }

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

        [Required]
        [StringLength(100, ErrorMessage = "A {0} deve conter {2} caracteres ao menos.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Nova senha")]
        public string Senha { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A {0} deve conter {2} caracteres ao menos.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmação nova senha")]
        [Compare("Senha", ErrorMessage = "A Nova Senha e Confirmação não conferem.")]
        public string ConfirmaSenha { get; set; }

        [DisplayName("Li e aceito as condições.")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "Aceite os termos e condições")]
        public bool TermosECondicoes { get; set; }
    }
}
