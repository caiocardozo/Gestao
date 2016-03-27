using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDDD.Domain.Entities
{
   public class Contato : Entidade
    {
        public int contato_Id { get; set; }
        public string ctt_nome { get; set; }
        public string ctt_email { get; set; }
        public string ctt_telefone { get; set; }
        public string  ctt_msg { get; set; }
    }
}

