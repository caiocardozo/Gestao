using GestaoDDD.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace GestaoDDD.Application.ViewModels
{
    public class ServicoPrestadorViewModel 
    {
        [Key]
        public Prestador prestador_Id { get; set; }
        [Key]
        public Servico servico_Id { get; set; }
    }
}
