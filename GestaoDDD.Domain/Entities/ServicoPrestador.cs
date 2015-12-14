using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDDD.Domain.Entities
{
    public class ServicoPrestador : Entidade
    {
        public int serv_pres_Id { get; set; }
        public Prestador prestador_Id { get; set; }
        public Servico servico_Id { get; set; }
    }
}
