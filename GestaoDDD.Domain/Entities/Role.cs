using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDDD.Domain.Entities
{
    public class Role : Entidade
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
