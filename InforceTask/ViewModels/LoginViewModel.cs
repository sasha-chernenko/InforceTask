using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InforceTask.ViewModels
{
    [Description]
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Login is required.")]
        public string? Login { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
