using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Orlik.Web.ViewModels.Users
{
    public class UsersRegisterViewModel
    {
        [DisplayName("Nazwa użytkownika")]
        [Required(ErrorMessage = "Nazwa użytkownika jest wymagana.")]
        [StringLength(20, ErrorMessage = "Nazwa użytkownika musi posiadać od 6 do 20 znaków", MinimumLength = 6)]
        public string Username { get; set; }

        [DisplayName("Hasło")]
        [Required(ErrorMessage = "Hasło jest wymagane.")]
        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessage = "Hasło musi posiadać od 6 do 20 znaków", MinimumLength = 6)]
        public string Password { get; set; }

        [DisplayName("Imię")]
        [Required(ErrorMessage = "Imię jest wymagane.")]
        [StringLength(20, ErrorMessage = "Imię musi posiadać od 6 do 20 znaków", MinimumLength = 6)]
        public string FirstName { get; set; }

        [DisplayName("Nazwisko")]
        [Required(ErrorMessage = "Nazwisko jest wymagane.")]
        [StringLength(20, ErrorMessage = "Nazwisko musi posiadać od 6 do 20 znaków", MinimumLength = 6)]
        public string SecondName { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Email jest wymagany.")]
        [EmailAddress(ErrorMessage = "Nieprawidłowy format adresu email.")]
        public string Email { get; set; }
    }
}