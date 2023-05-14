using System.ComponentModel.DataAnnotations;

namespace EduHome.ViewModels
{
    public class UpdateVM
    {
        [Required(ErrorMessage = "this line must not be empty")]
        public string Name { get; set; }


        [Required(ErrorMessage = "this line must not be empty")]
        public string Surname { get; set; }


        [Required(ErrorMessage = "this line must not be empty")]
        public string Username { get; set; }


        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Role { get; set; }


    }
}
