using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GestaoDDD.MVC.ViewModels
{
    public class CategoriaViewModel
    {
        [Key]
        public int Id_pk { get; set; }

        [Required(ErrorMessage = "Preencha o campo nome")]
        [MaxLength(150, ErrorMessage = "Tamanho máximo de {0} caracteres")]
        [MinLength(2, ErrorMessage = "Mínimo {0} caracteres")]
        [DisplayName("Nome")]
        public string Nome { get; set; }

    }
}