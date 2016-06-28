using GestaoDDD.Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GestaoDDD.Application.ViewModels
{
    public class PrestadorUsuarioViewModel
    {
        [Key]
        [ScaffoldColumn(false)]
        public string pres_Id { get; set; }

        [Required(ErrorMessage = "Preencha o campo nome")]
        [DisplayName("Nome")]
        public string pres_nome { get; set; }

        [Required(ErrorMessage = "Preencha o campo CPF/CNPJ.")]
        [DisplayName("CPF / CNPJ:")]
        public string pres_cpf_cnpj { get; set; }


        [Required(ErrorMessage = "Preencha o endereço.")]
        [MaxLength(200, ErrorMessage = "Tamanho máximo de {0} caracteres.")]
        [MinLength(10, ErrorMessage = "Tamanho minímo de {0} caracteres.")]
        public string pres_Endereco { get; set; }

        [Required(ErrorMessage = "Preencha o e-mail")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Insira um email válido.")]
        [DisplayName("Email")]
        public string pres_email { get; set; }

        [DisplayName("Telefone residencial.")]
        [StringLength(14, ErrorMessage = "A {0} deve conter {2} caracteres ao menos.", MinimumLength = 13)]
        [DataType(DataType.PhoneNumber)]
        public string pres_telefone_residencial { get; set; }


        [DisplayName("Telefone celular.")]
        [StringLength(14, ErrorMessage = "A {0} deve conter {2} caracteres ao menos.", MinimumLength = 14)]
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

        public string nome_Empresa { get; set; }

        public string caminho_foto { get; set; }
        public string foto { get; set; }

        public string apresentacao_Pesssoal { get; set; }
        
        public string apresentacao_Empresa { get; set; }
        [Required]
        public string pres_Latitude { get; set; }
        [Required]
        public string pres_Longitude { get; set; }

        [DisplayName("Li e aceito as condições.")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "Aceite os termos e condições")]
        public bool TermosECondicoes { get; set; }

        public string pres_Raio_Recebimento { get; set; }

        public virtual Usuario Usuario { get; set; }
        public ICollection<ServicoPrestador> ServicoPrestador { get; set; }
        public virtual ICollection<Orcamento> OrcamentoFk { get; set; }
    }
}

