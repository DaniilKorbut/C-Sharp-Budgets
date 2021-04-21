using Budgets.BusinessLayer.Wallets;
using System;
using System.Collections.Generic;
using DataStorage;

namespace Budgets.BusinessLayer
{
    public class Transaction : IStorable
    {
        public Guid Guid { get; private set; }
        public decimal Sum { get; set; }
        public string Currency { get; set; }
        public Category Category { get; set; }
        public string Description { get; set; }
        public DateTimeOffset Date { get; set; }
        public List<string> Files { get; set; }

        // private Wallet wallet;

        public Transaction(Guid guid, decimal sum, string currency, Category category, DateTimeOffset date, string description = null, List<string> files = null)
        {
            // this.wallet = wallet;
            this.Guid = guid;
            this.Sum = sum;
            this.Currency = currency;
            this.Category = category;
            this.Date = date;
            this.Description = description;
            this.Files = files;
            if (files == null)
            {
                files = new List<string>();
            }
        }


        /*public bool Validate()
        {
            bool isValid = wallet != null;
            if (string.IsNullOrWhiteSpace(Currency))
            {
                isValid = false;
            }
            return isValid;
        }*/
    }
}