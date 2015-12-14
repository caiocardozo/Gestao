
using System.Runtime.Serialization;
namespace GestaoDDD.Domain.Entities
{
    public class Orcamento : Entidade
    {
        public int orc_Id { get; set; }

        public string orc_Endereco_Servico { get; set; }

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
