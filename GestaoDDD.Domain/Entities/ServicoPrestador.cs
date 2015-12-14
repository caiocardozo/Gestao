
namespace GestaoDDD.Domain.Entities
{
    public class ServicoPrestador : Entidade
    {
        public int serv_Pres_Id { get; set; }

        public Prestador prestador_Id { get; set; }

        public Servico servico_Id { get; set; }
    }
}
