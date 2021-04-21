using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Budgets.BusinessLayer;
using Budgets.BusinessLayer.Users;
using Budgets.BusinessLayer.Wallets;
using DataStorage;

namespace Budgets.Services
{
    public class TransactionsService
    {
        private FileDataStorage<Transaction> storage;

        public TransactionsService(Guid walletGuid)
        {
            storage = new(walletGuid.ToString("N"));
        }

        public async Task<List<Transaction>> GetTransactions()
        {
            return await Task.Run(async () => { return await storage.GetAllAsync(); });
        }

        public async Task AddOrUpdateTransactions(Transaction transaction)
        {
            await storage.AddOrUpdateAsync(transaction);
        }

        public void DeleteTransaction(Guid guid)
        {
            storage.Delete(guid);
        }
    }
}
