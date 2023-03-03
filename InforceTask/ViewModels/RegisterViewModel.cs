using System.ComponentModel.DataAnnotations;

namespace InforceTask.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please enter Login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Please enter Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "Please confirm Password")]
        [Compare(nameof(Password), ErrorMessage = "Passwords don`t match")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string PasswordConfirm { get; set; }
    }

}