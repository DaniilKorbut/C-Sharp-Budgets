using System;
using DataStorage;

namespace Budgets.BusinessLayer.Users
{
    public class DBUser : IStorable
    {
        public Guid Guid { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }

        public string Login { get; }
        public string Password { get; }

        public DBUser(Guid guid, string firstName, string lastName, string email, string login, string password)
        {
            Guid = guid;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Login = login;
            Password = password;
        }
    }
}
