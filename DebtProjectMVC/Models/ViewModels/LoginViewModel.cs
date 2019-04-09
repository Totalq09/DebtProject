using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DebtProjectMVC.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Musisz podać email")]
        [EmailAddress(ErrorMessage = "Musisz podać prawidłowy adres email")]
        [Display(Name = "Email")]
        public string LoginEmail { get; set; }

        [Required(ErrorMessage = "Musisz podać hasło")]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string LoginPassword { get; set; }

        [Display(Name = "Zapamiętaj mnie?")]
        public bool RememberMe { get; set; }
    }
}
