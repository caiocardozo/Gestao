using GestaoDDD.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace GestaoDDD.Application.ViewModels
{
    public class ServicoPrestadorViewModel 
    {
        [Key]
        public int serv_Pres_Id { get; set; }
        [Key]
        public virtual Prestador prestador_Id { get; set; }
        [Key]
        public virtual Servico servico_Id { get; set; }
    }
}
