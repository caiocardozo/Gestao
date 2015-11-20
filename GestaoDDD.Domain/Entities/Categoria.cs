using System;
using System.Collections.Generic;

namespace GestaoDDD.Domain.Entities
{
    public class Categoria : Entidade
    {
        public int cat_Id { get; set; }
        public string cat_Nome { get; set; }
        public List<Servico> cat_servico_id { get; set; }
    }
}
