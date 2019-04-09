using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DebtProjectMVC.Models.ViewModels
{
    public class UpdateDebtEntryViewModel: NewDebtEntryViewModel
    {
        public int Id { get; set; }
        public decimal ReturnedValue { get; set; }
    }
}
