using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GestaoDDD.Application.ViewModels
{
    public class UsuarioViewModel 
    {
        public int usu_Id { get; set; }

        [Required(ErrorMessage = "Preencha o campo de endereço")]
        [MaxLength(200, ErrorMessage = "Tamanho máximo de {0} caracteres")]
        [MinLength(2, ErrorMessage = "Mínimo {0} caracteres")]
        [DisplayName("Endereço")]
        public string usu_endereco { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage="Utilize um e-mail válido.")]
        [Required(ErrorMessage="Preencha o campo de e-mail.")] 
        public string usu_email { get; set; }
    }
}
