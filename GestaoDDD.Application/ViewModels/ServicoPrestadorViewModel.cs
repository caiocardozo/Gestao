using GestaoDDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDDD.Application.ViewModels
{
    public class ServicoPrestadorViewModel 
    {
        [Key]
        public int serv_prest_Id { get; set; }
        public Prestador prestador_Id { get; set; }
        public Servico servico_Id { get; set; }
    }
}
