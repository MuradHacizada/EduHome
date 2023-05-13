using System.ComponentModel.DataAnnotations;

namespace EduHome.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "this line must not be empty")]
        public string Name { get; set; }


        [Required(ErrorMessage = "this line must not be empty")]
        public string Surname { get; set; }


        [Required(ErrorMessage = "this line must not be empty")]
        public string Username { get; set; }


        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]


        [Compare("Password")]
        public string CheckPassword { get; set; }
        public bool IsRemember { get; set; }
    }
}
