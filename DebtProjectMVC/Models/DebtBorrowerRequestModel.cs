using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DebtProjectMVC.Models
{
    public class DebtBorrowerRequestModel
    {
        public int Current { get; set; }
        public int RowCount { get; set; }
        public string Search { get; set; }
        public string SortBy { get; set; }
        public string SortDirection { get; set; }
        public string PESEL { get; set; }
    }
}
