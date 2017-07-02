using System.ComponentModel.DataAnnotations;

namespace FoxMoney.Server.ViewModels.AccountViewModels {
    public class ForgotPasswordViewModel {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}