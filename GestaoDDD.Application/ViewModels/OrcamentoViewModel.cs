using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace GestaoDDD.Application.ViewModels
{
    public class OrcamentoViewModel
    {
        public int orc_Id { get; set; }

        [Required(ErrorMessage = "Preencha o endereço do serviço.")]

        public string orc_endereco_servico { get; set; }

        [Required(ErrorMessage = "Preencha uma breve descrição")]
        [MaxLength(250, ErrorMessage = "Tamanho máximo de {0} caracteres.")]
        [MinLength(10, ErrorMessage = "Tamanho minímo de {0} caracteres.")]
        public string orc_descricao { get; set; }
       
        public int orc_dias_prazo { get; set; }
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