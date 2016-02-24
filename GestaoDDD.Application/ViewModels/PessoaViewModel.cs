
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GestaoDDD.Application.ViewModels
{
    public class PessoaViewModel
    {
        public string usu_id { get; set; }

        [Required(ErrorMessage = "Preencha o campo nome")]
        [MaxLength(150, ErrorMessage = "Tamanho máximo de {0} caracteres")]
        [MinLength(2, ErrorMessage = "Mínimo {0} caracteres")]
        [DisplayName("Nome")]
        public string pes_nome { get; set; }

        [Required(ErrorMessage = "Preencha o campo cpf")]
        [MaxLength(11, ErrorMessage = "Tamanho máximo de {0} caracteres")]
        [MinLength(11, ErrorMessage = "Mínimo {0} caracteres")]
        [DisplayName("CPF")]
        public string pes_cpf { get; set; }

        [MaxLength(9, ErrorMessage = "Tamanho máximo de {0} caracteres")]
        [MinLength(4, ErrorMessage = "Mínimo {0} caracteres")]
        [DisplayName("RG")]
        public string pes_rg { get; set; }

        [MaxLength(200, ErrorMessage = "Tamanho máximo de {0} caracteres")]
        [MinLength(2, ErrorMessage = "Mínimo {0} caracteres")]
        [DisplayName("Enredeço")]
        public string pes_endereco { get; set; }

        [DisplayName("Número")]
        public int pes_numero { get; set; }

        [MaxLength(100, ErrorMessage = "Tamanho máximo de {0} caracteres")]
        [MinLength(2, ErrorMessage = "Mínimo {0} caracteres")]
        [DisplayName("Bairro")]
        public string pes_bairro { get; set; }

        [MaxLength(100, ErrorMessage = "Tamanho máximo de {0} caracteres")]
        [MinLength(2, ErrorMessage = "Mínimo {0} caracteres")]
        [DisplayName("Cidade")]
        public string pes_cidade { get; set; }

        [MaxLength(9, ErrorMessage = "Tamanho máximo de {0} caracteres")]
        [MinLength(9, ErrorMessage = "Mínimo {0} caracteres")]
        [DisplayName("CEP")]
        public string pes_cep { get; set; }

    }
}
