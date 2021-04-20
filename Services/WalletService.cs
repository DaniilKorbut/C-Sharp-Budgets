using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budgets.BusinessLayer.Users;
using Budgets.BusinessLayer.Wallets;
using DataStorage;

namespace Budgets.Services
{
    public class WalletService
    {
        private FileDataStorage<Wallet> storage = new(CurrentUser.User.Guid.ToString("N"));

        /*private static List<Wallet> Users = new List<Wallet>()
        {
            new Wallet(null, Guid.NewGuid(), "My Main Wallet", 57.06m, "UAH"),
            new Wallet(null, Guid.NewGuid(), "My Savings", 3030.43m, "UAH"),
            new Wallet(null, Guid.NewGuid(), "Salary", 1236.58m, "UAH"),
        };*/

        public async Task<List<Wallet>> GetWallets()
        {
           return await Task.Run(async ()=> { return await storage.GetAllAsync(); });
        }

        public async Task AddOrUpdateWallet(Wallet wallet)
        {
            await storage.AddOrUpdateAsync(wallet);
        }

        public void DeleteWallet(Guid guid)
        {
           storage.Delete(guid);
        }
    }
}
