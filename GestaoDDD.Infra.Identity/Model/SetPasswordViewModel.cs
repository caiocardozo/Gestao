using System.ComponentModel.DataAnnotations;

namespace GestaoDDD.Infra.Identity.Model
{
    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "A {0} deve conter no mínimo {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nova senha")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirma nova senha")]
        [Compare("NewPassword", ErrorMessage = "A nova senha e confirmção não coincidem.")]
        public string ConfirmPassword { get; set; }
    }
}
