using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DebtProjectMVC.Entities
{
    public class Creditor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PESEL { get; set; }

        public string UserId { get; set; }
        public virtual IdentityUser User { get; set; }
    }
}
