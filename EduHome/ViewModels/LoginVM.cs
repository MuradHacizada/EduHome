using System.ComponentModel.DataAnnotations;

namespace EduHome.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "this line must not be empty")]
        public string Username { get; set; }

        [Required(ErrorMessage = "this line must not be empty")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsRemember { get; set; }

    }
}
