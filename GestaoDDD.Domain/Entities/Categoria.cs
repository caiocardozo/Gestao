using System.Collections.Generic;

namespace GestaoDDD.Domain.Entities
{
    public class Categoria : Entidade
    {
        public int cat_Id { get; set; }

        public string cat_Nome { get; set; }

        public ICollection<Servico> Servico { get; set; }

        //public virtual Orcamento Orcamento { get; set; }
    }
}
 