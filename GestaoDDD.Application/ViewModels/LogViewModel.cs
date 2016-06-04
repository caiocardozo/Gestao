using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDDD.Application.ViewModels
{
    public class LogViewModel
    {
        public int Id { get; set; }
        public string Mensagem { get; set; }
        public string Controller { get; set; }
        public string View { get; set; }
    }
}
