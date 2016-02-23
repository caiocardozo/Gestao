using System.ComponentModel.DataAnnotations;

namespace GestaoDDD.Infra.Identity.Model
{
    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Número de telefone")]
        public string Number { get; set; }
    }
}
