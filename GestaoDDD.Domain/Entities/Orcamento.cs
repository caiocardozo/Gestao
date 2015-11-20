
namespace GestaoDDD.Domain.Entities
{
    public class Orcamento : Entidade
    {
        public int orc_Id { get; set; }
        public string orc_endereco_servico { get; set; }
        public string orc_descricao { get; set; }
        public int orc_dias_prazo { get; set; }
        //public EnumFrequencia orc_frequencia_prazo { get; set; }

    }

     enum EnumFrequencia
    {
        Dias,
        Mês,
        Ano
    };

}
