
using System.Collections.Generic;
namespace GestaoDDD.Domain.Entities
{
    public class Servico: Entidade
    {
        public int serv_Id { get; set; }
        public string serv_nome { get; set; }
        public Categoria categoria_id { get; set; }
    }
}
