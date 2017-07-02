using System.ComponentModel.DataAnnotations;

namespace FoxMoney.Server.ViewModels.AccountViewModels {
    public class ExternalLoginConfirmationViewModel {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}