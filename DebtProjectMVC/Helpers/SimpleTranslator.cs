using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DebtProjectMVC.Helpers
{
    public class SimpleTranslator
    {
        public static string GetMessageBasedOnCode(string code)
        {
            switch (code)
            {
                case "PasswordRequiresNonAlphanumeric":
                    return "Hasło musi posiadać przynajmniej jeden znak specjalny";

                case "PasswordRequiresDigit":
                    return "Hasło musi posiadać przynajmniej jedną cyfrę";
                    
                case "PasswordRequiresUpper":
                    return "Hasło musi posiadać przynajmniej jedną dużą literę";

                case "DuplicateUserName":
                    return "Użytkownik o podanym mailu już istnieje";
            }

            return null;
        }
    }
}
