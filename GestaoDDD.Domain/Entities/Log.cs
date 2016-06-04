using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDDD.Domain.Entities
{
    public class Log : Entidade
    {
        public int Id { get; set; }
        public string Mensagem { get; set; }
        public string Controller { get; set; }
        public string View { get; set; }

    }
}
