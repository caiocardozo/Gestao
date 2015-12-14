namespace GestaoDDD.Domain.Entities
{
    public class Servico : Entidade
    {
        public int serv_Id { get; set; }

        public string serv_Nome { get; set; }

        public Categoria categoria_Id { get; set; }
    }
}
