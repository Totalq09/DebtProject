using DebtProjectMVC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DebtProjectMVC.Models.ViewModels
{
    public class DebtEntryViewModel
    {
        public int Id { get; set; }
        public decimal Value { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public decimal? ReturnedValue { get; set; }
        public string Status { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Pesel { get; set; }
    }
}
