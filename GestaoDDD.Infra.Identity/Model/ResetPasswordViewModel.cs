using System.ComponentModel.DataAnnotations;

namespace GestaoDDD.Infra.Identity.Model
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A {0} deve conter no mínimo {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirma senha")]
        [Compare("Password", ErrorMessage = "A nova senha e confirmção não coincidem.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}
