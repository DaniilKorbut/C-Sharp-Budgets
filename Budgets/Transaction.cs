using Budgets.BusinessLayer.Wallets;
using System;
using System.Collections.Generic;

namespace Budgets.BusinessLayer
{
    public class Transaction
    {
        private static int InstanceCount = 1;
        private int id;
        private decimal sum;
        private string currency;
        private Category category;
        private string description;
        private DateTime date;
        private List<string> files;

        private Wallet wallet;

        public Transaction(Wallet wallet, decimal sum, string currency, Category category, DateTime date, string description = null, List<string> files = null)
        {
            this.wallet = wallet;
            Sum = sum;
            this.currency = currency;
            this.category = category;
            Date = date;
            this.description = description;
            this.files = files;
            if (files == null)
            {
                files = new List<string>();
            }
        }

        public int Id { get => id; }
        public decimal Sum { get => sum; set => sum = value; }
        public DateTime Date { get => date; set => date = value; }

        public bool Validate()
        {
            bool isValid = wallet != null;
            if (string.IsNullOrWhiteSpace(currency))
            {
                isValid = false;
            }
            return isValid;
        }
    }
}