using System.ComponentModel.DataAnnotations;

namespace GestaoDDD.Infra.Identity.Model
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
