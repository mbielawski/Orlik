using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Orlik.Web.ViewModels.Account
{
    public class AccountResetPasswordConfirmedViewModel
    {
        public string Email { get; set; }

        [DisplayName("Hasło")]
        [Required(ErrorMessage = "Hasło jest wymagane.")]
        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessage = "Hasło musi posiadać od 6 do 20 znaków", MinimumLength = 6)]
        public string Password { get; set; }
    }
}