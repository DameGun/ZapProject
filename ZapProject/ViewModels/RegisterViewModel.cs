using System.ComponentModel.DataAnnotations;

namespace ZapProject.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Введите ваш email")]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Подтвердите ваш пароль")]
        [Required(ErrorMessage = "Это поле обязательно")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

    }
}
