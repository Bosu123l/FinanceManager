using System.ComponentModel.DataAnnotations;

namespace FinanceManager.Models
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}