using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Budgets.Models.Users;

namespace Budgets.Services
{
    public class AuthenticationService
    {
        private static List<DBUser> Users = new List<DBUser>();

        public User Authenticate(AuthenticationUser authenticationUser)
        {
            Thread.Sleep(3000);
            if (String.IsNullOrWhiteSpace(authenticationUser.Login) ||
                String.IsNullOrWhiteSpace(authenticationUser.Password))
            {
                throw new ArgumentException("Login or Password is empty");
            }

            var dbUser = Users.FirstOrDefault(user =>
                user.Login == authenticationUser.Login && user.Password == authenticationUser.Password);
            if (dbUser == null)
            {
                throw new Exception("Wrong Login or Password");
            }
            return new User(dbUser.Guid, dbUser.FirstName, dbUser.LastName, dbUser.Email, dbUser.Login);
        }

        public bool RegisterUser(RegistrationUser regUser)
        {
            var dbUser = Users.FirstOrDefault(user => user.Login == regUser.Login);
            if(dbUser != null)
            {
                throw new Exception("User already exists");
            }

            if (String.IsNullOrWhiteSpace(regUser.Login)
                || String.IsNullOrWhiteSpace(regUser.Password) || String.IsNullOrWhiteSpace(regUser.FirstName)
                || String.IsNullOrWhiteSpace(regUser.LastName) || String.IsNullOrWhiteSpace(regUser.Email))
            {
                throw new ArgumentException("All fields are required");
            }

            dbUser = new DBUser(regUser.FirstName, regUser.LastName, regUser.Email, regUser.Login, regUser.Password);
            Users.Add(dbUser);
            return true;
        }
    }
}
