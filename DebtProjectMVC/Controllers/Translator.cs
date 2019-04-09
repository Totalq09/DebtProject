using DebtProjectMVC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DebtProjectMVC.Controllers
{
    public static class Translator
    {
        public static string Translate(DebtEntryStatus status)
        {
            switch(status)
            {
                case DebtEntryStatus.Open:
                    return "Otwarty";
                case DebtEntryStatus.Closed:
                    return "Zamknięty";
                case DebtEntryStatus.PartiallyReturned:
                    return "Częściowo uregulowany";
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
