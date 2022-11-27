using System.ComponentModel.DataAnnotations;

namespace ToDoProject.ViewModel
{
    public class RegisterAccountViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Must Match with Password")]
        public string ConfirmPassword { get; set; }
    }
}
