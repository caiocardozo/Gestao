using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDDD.Domain.Entities
{
    public class Servico: Entidade
    {
        public int serv_Id { get; set; }
        public string serv_nome { get; set; }
    }
}
