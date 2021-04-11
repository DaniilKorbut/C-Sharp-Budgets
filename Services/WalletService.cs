using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budgets.Models.Wallets;

namespace Budgets.Services
{
    public class WalletService
    {
        private static List<Wallet> Users = new List<Wallet>()
        {
            new Wallet() {Name = "My Main Wallet", Balance = 57.06m},
            new Wallet() {Name = "My Savings", Balance = 3030.43m},
            new Wallet() {Name = "Salary", Balance = 1236.58m},
        };

        public List<Wallet> GetWallets()
        {
            return Users.ToList();
        }
    }
}
