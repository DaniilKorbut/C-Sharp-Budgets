using System;
using System.Collections.Generic;
using System.Text;

namespace Budgets
{
    public class User
    {
        private static int instanceCount = 1;

        private int id;
        private string firstName;
        private string lastName;
        private string email;

        private List<Wallet> wallets = new List<Wallet>();
        private List<SharedWallet> sharedWallets = new List<SharedWallet>();
        private List<Category> categories = new List<Category>();

        public User(string firstName, string lastName, string email)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.Id = instanceCount++;
        }

        public int Id { get => id; private set => id = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        
        public string LastName { get => lastName; set => lastName = value; }
        public string Email { get => email; set => email = value; }
        public List<Wallet> Wallets { get => wallets; private set => wallets = value; }
        public List<Category> Categories { get => categories; private set => categories = value; }

        public void AddWallet(Wallet wallet)
        {
            this.Wallets.Add(wallet);
        }

        public bool DeleteWallet(int id)
        {
            foreach(Wallet w in this.wallets)
            {
                if (w.Id == id)
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
            if (string.IsNullOrWhiteSpace(this.firstName))
            {
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(this.lastName))
            {
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(this.email))
            {
                isValid = false;
            }
            return isValid;
        }
        public bool Share(User user, int wallet_Id)
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
        }
    }

    
}
