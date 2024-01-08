using System.ComponentModel.DataAnnotations;

namespace TpChrono.Models.ModelViews
{
    public class UserLoginModelView
    {
        [Required(ErrorMessage = "Le login est obligatoire")]

        public string? Login { get; set; }


        [MinLength(6, ErrorMessage = "Le password > 6 caractères")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
