using System.Runtime.Serialization;

namespace GestaoDDD.Domain.Entities
{
    public class Orcamento : Entidade
    {
        public int orc_Id { get; set; }

        public string orc_Endereco { get; set; }

        public int orc_numero { get; set; }

        public string orc_bairro { get; set; }

        public string orc_cidade { get; set; }

        public string orc_cep { get; set; }

        public string orc_referencia { get; set; }

        public string orc_Descricao { get; set; }

        public int orc_Dias_Prazo { get; set; }

        public EnumFrequencia orc_Frequencia_Prazo { get; set; }

    }

    [DataContract]
    public enum EnumFrequencia
    {
        Dias,
        Mês,
        Ano
    };

}
