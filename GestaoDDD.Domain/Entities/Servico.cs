namespace GestaoDDD.Domain.Entities
{
    public class Servico : Entidade
    {
        public int serv_Id { get; set; }

        public string serv_Nome { get; set; }

        public int cat_Id { get; set; }
        
        public virtual Categoria Categoria { get; set; }
        public virtual Orcamento Orcamento { get; set; }
    }
}
