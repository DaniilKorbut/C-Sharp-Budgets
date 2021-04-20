using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Budgets.BusinessLayer.Users;
using DataStorage;

namespace Budgets.Services
{
    public class AuthenticationService
    {
        private FileDataStorage<DBUser> storage = new();

        public async Task<User> Authenticate(AuthenticationUser authenticationUser)
        {
            return await Task.Run(async () =>
            {
                if (String.IsNullOrWhiteSpace(authenticationUser.Login) ||
                    String.IsNullOrWhiteSpace(authenticationUser.Password))
                {
                    throw new ArgumentException("Login or Password is empty");
                }

                var users = await storage.GetAllAsync();
                var dbUser = users.FirstOrDefault(user =>
                    user.Login == authenticationUser.Login && user.Password ==
                    ComputeHash(authenticationUser.Password, new SHA256CryptoServiceProvider()));
                if (dbUser == null)
                {
                    throw new Exception("Wrong Login or Password");
                }

                return new User(dbUser.Guid, dbUser.FirstName, dbUser.LastName, dbUser.Email, dbUser.Login);
            });
        }

        public async Task<bool> RegisterUser(RegistrationUser regUser)
        {
            return await Task.Run(async () =>
            {
                var users = await storage.GetAllAsync();
                var dbUser = users.FirstOrDefault(user => user.Login == regUser.Login);
                if (dbUser != null)
                {
                    throw new Exception("Login is already taken");
                }

                if (String.IsNullOrWhiteSpace(regUser.Login)
                    || String.IsNullOrWhiteSpace(regUser.Password) || String.IsNullOrWhiteSpace(regUser.FirstName)
                    || String.IsNullOrWhiteSpace(regUser.LastName) || String.IsNullOrWhiteSpace(regUser.Email))
                {
                    throw new ArgumentException("All fields are required");
                }

                ValidationService vs = new ValidationService();
                vs.ValidateEmail(regUser.Email);
                vs.ValidateLogin(regUser.Login);
                vs.ValidatePassword(regUser.Password);

                dbUser = new DBUser(Guid.NewGuid(), regUser.FirstName, regUser.LastName, regUser.Email, regUser.Login,
                    ComputeHash(regUser.Password, new SHA256CryptoServiceProvider()));
                await storage.AddOrUpdateAsync(dbUser);
                return true;
            });
        }



        public string ComputeHash(string input, HashAlgorithm algorithm)
            {
                Byte[] inputBytes = Encoding.UTF8.GetBytes(input);

                Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

                return BitConverter.ToString(hashedBytes);
            }


        
    }
}
