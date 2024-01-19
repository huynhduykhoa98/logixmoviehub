using System.ComponentModel.DataAnnotations;

namespace LogixMovieApplication.WebApi.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Please enter Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
