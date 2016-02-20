using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDDD.Application.ViewModels
{
    public class ComoFuncionaViewModel
    {
        [Key]
        public int cf_Id { get; set; }
       
        [DisplayName("Ordem")]
        public int cf_Ordem { get; set; }

        [Required(ErrorMessage = "Preencha o campo informação")]
        [MaxLength(200, ErrorMessage = "Tamanho máximo de {0} caracteres")]
        [MinLength(2, ErrorMessage = "Mínimo {0} caracteres")]
        [DisplayName("Informação")]
        public string cf_Informacao { get; set; }
    }
    }
}
