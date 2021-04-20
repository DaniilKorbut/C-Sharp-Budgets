using Budgets.BusinessLayer.Wallets;
using System;
using System.Collections.Generic;
using System.Text;

namespace Budgets.BusinessLayer
{
    class SharedWallet
    {
        private Wallet wallet;

        public SharedWallet(Wallet wallet)
        {
            this.wallet = wallet;
        }

        public Guid Guid
        {
            get => wallet.Guid;
        }

        public string Name
        {
            get => wallet.Name;
        }

        public decimal StartBalance
        {
            get => wallet.StartBalance;
        }

        public decimal Balance
        {
            get => wallet.Balance;
        }

        public string Description
        {
            get => wallet.Description;
        }

        public string Currency
        {
            get => wallet.Currency;
        }

        public void AddTransaction(decimal sum, string currency, Category category, DateTime date, string description = null, List<string> files = null)
        {
            wallet.AddTransaction(sum, currency, category, date, description, files);
        }

        public bool DeleteTransaction(int id)
        {
            return wallet.DeleteTransaction(id);
        }

        public bool AddCategory(Category category)
        {
            return wallet.AddCategory(category);
        }

        public bool DeleteCategory(int id)
        {
            return wallet.DeleteCategory(id);
        }

        public decimal getIncomeForCurrentMonth()
        {
            return wallet.getIncomeForCurrentMonth();
        }

        public decimal getExpensesForCurrentMonth()
        {
            return wallet.getExpensesForCurrentMonth();
        }

        public List<Transaction> GetTransactions(int from = 0)
        {
            return wallet.GetTransactions(from);
        }
    }
}
