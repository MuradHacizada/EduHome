using System.ComponentModel.DataAnnotations;

namespace EduHome.ViewModels
{
    public class ResetPasswordVM
    {
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]


        [Compare("Password")]
        public string CheckPassword { get; set; }
        public bool IsRemember { get; set; }
    }
}
