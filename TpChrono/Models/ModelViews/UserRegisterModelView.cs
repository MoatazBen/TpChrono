using System.ComponentModel.DataAnnotations;

namespace TpChrono.Models.ModelViews
{
    public class UserRegisterModelView
    {

        [Required(ErrorMessage = "Le nom est obligatoire")]

        public string? Nom { get; set; }

        public string? Prenom { get; set; }
        [Required(ErrorMessage = "Le login est obligatoire")]

        public string Login { get; set; }


        [MinLength(6, ErrorMessage = "Le password > 6 caractères")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Compare("Password", ErrorMessage = "Les 2 password doivent être equi")]
        [DataType(DataType.Password)]

        public string? PasswordConfirmation { get; set; }
        public string Role { get; set; }


    }
}
