using System.ComponentModel.DataAnnotations;

namespace GestaoDDD.Infra.Identity.Model
{
    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "Código")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Número de telefone")]
        public string PhoneNumber { get; set; }
    }
}
