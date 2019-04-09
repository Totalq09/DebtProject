using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DebtProjectMVC.Models.ViewModels
{
    public class NewDebtEntryViewModel
    {
        public decimal Value { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Pesel { get; set; }
    }
}
