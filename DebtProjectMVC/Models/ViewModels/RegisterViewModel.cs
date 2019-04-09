using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DebtProjectMVC.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Musisz podać email")]
        [EmailAddress(ErrorMessage = "Musisz podać prawidłowy adres email")]
        [Display(Name = "Email")]
        public string RegisterEmail { get; set; }

        [Required(ErrorMessage = "Musisz podać hasło")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Nieprawidłowe hasło, powinno składać sie z 6-100 znaków", MinimumLength = 6)]
        [Display(Name = "Hasło")]
        public string RegisterPassword { get; set; }

        [Required(ErrorMessage = "Musisz powtórzyć hasło")]
        [DataType(DataType.Password)]
        [Display(Name = "Powtórz hasło")]
        public string ConfirmRegisterPassword { get; set; }

        [Required(ErrorMessage = "Podaj swoje imię")]
        [StringLength(100, ErrorMessage = "Imię nieprawidłowe", MinimumLength = 3)]
        [Display(Name = "Imię")]
        public string RegisterName { get; set; }

        [Required(ErrorMessage = "Podaj swoje nazwisko")]
        [StringLength(100, ErrorMessage = "Nazwisko nieprawidłowe", MinimumLength = 3)]
        [Display(Name = "Nazwisko")]
        public string RegisterSurname { get; set; }

        [Required(ErrorMessage = "Podaj swój PESEL")]
        [RegularExpression("([0-9]+)", ErrorMessage = "PESEL nieprawidłowy, PESEL powinien składać się z cyfr.")]
        [Display(Name = "PESEL")]
        public string RegisterPESEL { get; set; }
    }
}
