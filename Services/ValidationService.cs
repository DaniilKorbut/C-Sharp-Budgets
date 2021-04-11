using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Budgets.Services
{
    class ValidationService
    {
        public void ValidateLogin(string login)
        {
            if (login.Length < 5)
            {
                throw new ArgumentException("Login can't have less than 5 symbols");
            }
            if (login.Length > 30)
            {
                throw new ArgumentException("Login can't have more than 30 symbols");
            }
        }

        public void ValidatePassword(string password)
        {
            string passwordPolicy = "Password must meet the following requirements:\n" +
                                    "- More than 8 symbols\n" +
                                    "- Contains at least one lowercase letter\n" +
                                    "- Contains at least one uppercase letter\n" +
                                    "- Contains at least one digit";
            if (password.Length < 8)
            {
                throw new ArgumentException(passwordPolicy);
            }

            bool hasLowerCase = false;
            bool hasUpperCase = false;
            bool hasDigit = false;
            foreach (var c in password)
            {
                if (Char.IsLower(c))
                {
                    hasLowerCase = true;
                } else if (Char.IsUpper(c))
                {
                    hasUpperCase = true;
                } else if (Char.IsDigit(c))
                {
                    hasDigit = true;
                }
            }

            if (!hasLowerCase || !hasUpperCase || !hasDigit)
            {
                throw new ArgumentException(passwordPolicy);
            }
        }

        public void ValidateEmail(string email)
        {
            string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                             @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";
            if(!Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase))
            {
                throw new ArgumentException("Incorrect Email");
            }
        }
    }
}
