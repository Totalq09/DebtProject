using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DebtProjectMVC.Entities
{
    public class DebtEntry
    {
        public int Id { get; set; }
        public decimal Value { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public decimal? ReturnedValue { get; set; }
        public DebtEntryStatus Status { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PESEL { get; set; }

        public int CreditorId { get; set; }
        //public int? BorrowerId { get; set; }

        public Creditor Creditor { get; set; }
        //public Borrower Borrower { get; set; }
    }
}
