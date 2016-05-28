
namespace GestaoDDD.Domain.Entities
{
    public class ServicoPrestador : Entidade
    {
        public int serv_Pres_Id { get; set; }

        public string pres_Id { get; set; }

        public int serv_Id { get; set; }

        public virtual Prestador Prestador { get; set; }

        public virtual Servico Servico { get; set; }
    }
}
