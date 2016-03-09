using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace GestaoDDD.Application.ViewModels
{
    public class OrcamentoViewModel
    {
        [Key]
        [ScaffoldColumn(true)]
        public int orc_Id { get; set; }

        [DisplayName("Endereço")]
        [Required(ErrorMessage = "Preencha o endereço do serviço.")]
        [MaxLength(200, ErrorMessage = "Tamanho máximo de {0} caracteres.")]
        [MinLength(10, ErrorMessage = "Tamanho minímo de {0} caracteres.")]
        public string orc_Endereco { get; set; }

        [DisplayName("Número")]
        [Required(ErrorMessage = "Preencha o número.")]
        public int orc_numero { get; set; }

        [DisplayName("Bairro")]
        [Required(ErrorMessage = "Preencha o bairro.")]
        [MaxLength(100, ErrorMessage = "Tamanho máximo de {0} caracteres.")]
        [MinLength(3, ErrorMessage = "Tamanho minímo de {0} caracteres.")]
        public string orc_bairro { get; set; }

        [DisplayName("Cidade")]
        [Required(ErrorMessage = "Preencha a cidade.")]
        [MaxLength(100, ErrorMessage = "Tamanho máximo de {0} caracteres.")]
        [MinLength(3, ErrorMessage = "Tamanho minímo de {0} caracteres.")]
        public string orc_cidade { get; set; }

        [DisplayName("CEP")]
        [Required(ErrorMessage = "Preencha o CEP.")]
        [MaxLength(9, ErrorMessage = "Tamanho máximo de {0} caracteres.")]
        [MinLength(9, ErrorMessage = "Tamanho minímo de {0} caracteres.")]
        public string orc_cep { get; set; }

        [DisplayName("Referência")]
        [MaxLength(150, ErrorMessage = "Tamanho máximo de {0} caracteres.")]
        [MinLength(3, ErrorMessage = "Tamanho minímo de {0} caracteres.")]
        public string orc_referencia { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "Preencha uma breve descrição")]
        [MaxLength(250, ErrorMessage = "Tamanho máximo de {0} caracteres.")]
        [MinLength(10, ErrorMessage = "Tamanho minímo de {0} caracteres.")]
        public string orc_descricao { get; set; }

        [DisplayName("Prazo")]
        [Required(ErrorMessage = "Preencha os dias de prazo")]
        public int orc_dias_prazo { get; set; }

        [DisplayName("Frequência")]
        [Required(ErrorMessage = "Preencha a frequência do prazo")]

        //[DisplayName("Latitude")]
        //[Required(ErrorMessage = "Aponte seu endereço no mapa para que gere o valor da latitude.")]
        [Display(AutoGenerateField= false)]
        public string  orc_latitude { get; set; }

        //[DisplayName("Longitude")]
        //[Required(ErrorMessage = "Aponte seu endereço no mapa para que gere o valor da longitude.")]
        [Display(AutoGenerateField = false)]
        public string orc_longitude { get; set; }

        public EnumFrequencia orc_frequencia_prazo { get; set; }

    }

    [DataContract]
    public enum EnumFrequencia
    {
        Dias,
        Mês,
        Ano
    };
}