using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using GestaoDDD.Domain.Entities;

namespace GestaoDDD.Application.ViewModels
{
    public class PrestadorEditarViewModel
    {
        [Key]
        [ScaffoldColumn(false)]
        public string pres_Id { get; set; }

        [Required(ErrorMessage = "Preencha o campo nome")]
        [DisplayName("Nome")]
        public string pres_nome { get; set; }

        [DisplayName("CPF / CNPJ:")]
        [Required(ErrorMessage = "Preencha o campo CPF")]
        public string pres_cpf_cnpj { get; set; }

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

        [DisplayName("Nome empresa")]
        public string nome_Empresa { get; set; }

        [StringLength(500, ErrorMessage = "A {0} deve conter {2} caracteres ao menos.", MinimumLength = 0)]
        [Display(Name = "Breve apresentação pessoal")]
        public string apresentacao_Pesssoal { get; set; }

        [StringLength(500, ErrorMessage = "A {0} deve conter {2} caracteres ao menos.", MinimumLength = 0)]
        [Display(Name = "Breve apresentação da empresa")]
        public string apresentacao_Empresa { get; set; }

        public string caminho_foto { get; set; }
        public string foto { get; set; }
        
        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<Orcamento> OrcamentoFk { get; set; }
        public Domain.Entities.NoSql.EnumStatus status { get; set; }
    }
}
