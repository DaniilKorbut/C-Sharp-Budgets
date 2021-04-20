using Budgets.BusinessLayer.Wallets;
using System;
using System.Collections.Generic;

namespace Budgets.BusinessLayer.Users
{
    public class User
    {

        private List<Wallet> wallets = new List<Wallet>();
        private List<SharedWallet> sharedWallets = new List<SharedWallet>();
        private List<Category> categories = new List<Category>();

        public User(Guid guid, string firstName, string lastName, string email, string login)
        {
            Guid = guid;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Login = login;
        }

        public Guid Guid { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public string Login { get; }

        public List<Wallet> Wallets { get => wallets; private set => wallets = value; }
        public List<Category> Categories { get => categories; private set => categories = value; }

        public void AddWallet(Wallet wallet)
        {
            Wallets.Add(wallet);
        }

        public bool DeleteWallet(Guid guid)
        {
            foreach (Wallet w in wallets)
            {
                if (w.Guid == guid)
                {
                    wallets.Remove(w);
                    return true;
                }
            }
            return false;
        }

        public bool Validate()
        {
            bool isValid = true;
            if (string.IsNullOrWhiteSpace(FirstName))
            {
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(LastName))
            {
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(Email))
            {
                isValid = false;
            }
            return isValid;
        }
        /* public bool Share(User user, int wallet_Id)
         {
             if (!user.Validate() && user.Id == Id)
             {
                 return false;
             }
             SharedWallet sharedWallet = null;
             foreach (var w in wallets)
             {
                 if (w.Id == wallet_Id)
                 {
                     sharedWallet = new SharedWallet(w);
                     break;
                 }
             }
             if (sharedWallet != null)
             {
                 user.sharedWallets.Add(sharedWallet);
                 return true;
             }
             return false;
         }*/
    }
}
