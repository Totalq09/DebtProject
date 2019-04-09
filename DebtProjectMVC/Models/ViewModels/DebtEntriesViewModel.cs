using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DebtProjectMVC.Models.ViewModels
{
    public class DebtEntriesViewModel
    {
        public int Current { get; set; }
        public int RowCount { get; set; }
        public DebtEntryViewModel[] Rows { get; set; }
        public int Total { get; set; }
    }
}
