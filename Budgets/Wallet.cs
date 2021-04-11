using System;
using System.Collections.Generic;

namespace Budgets
{
    public class Wallet
    {
        private static int InstanceCount = 1;

        private User owner = null;

        private List<Transaction> transactions = new List<Transaction>();
        private List<Category> categories = new List<Category>();

        public Wallet(User owner, string name, decimal startBalance, string currency, string description = null)
        {
            this.Name = name;
            this.StartBalance = startBalance;
            this.CurrentBalance = startBalance;
            this.Currency = currency;
            this.Description = description;
            this.Id = InstanceCount++;
            this.owner = owner;
            this.owner.AddWallet(this);
        }

        public int Id { get; private set; }

        public string Name { get; set; }

        public decimal StartBalance { get; set; }

        public decimal CurrentBalance { get; private set; }

        public string Description { get; set; }

        public string Currency { get; set; }

        public void AddTransaction(decimal sum, string currency, Category category, DateTime date, string description = null, List<string> files = null)
        {
            transactions.Add(new Transaction(this, sum, currency, category, date, description, files));
            CurrentBalance = CurrentBalance + sum;
        }

        public bool DeleteTransaction(int id)
        {
            foreach (Transaction t in transactions)
            {
                if (t.Id == id)
                {
                    transactions.Remove(t);
                    CurrentBalance -= t.Sum;
                    return true;
                }
            }
            return false;
        }

        public bool AddCategory(Category category)
        {
            if (categories.Contains(category))
            {
                return false;
            }
            if (owner.Categories.Contains(category))
            {
                categories.Add(category);
                return true;
            }
            return false;
        }

        public bool DeleteCategory(int id)
        {
            foreach (Category c in categories)
            {
                if (c.Id == id)
                {
                    categories.Remove(c);
                    return true;
                }
            }
            return false;
        }

        private decimal getTransactionsSumForCurrentMonth(bool income)
        {
            DateTime now = DateTime.Today;
            DateTime currentMonth = new DateTime(now.Year, now.Month, 1);
            decimal sum = 0;
            foreach (Transaction t in transactions)
            {
                if (t.Date >= currentMonth && t.Date <= now)
                {

                    if (income)
                    {
                        if (t.Sum > 0)
                        {
                            sum += t.Sum;
                        }
                    }
                    else
                    {
                        if (t.Sum < 0)
                        {
                            sum += t.Sum;
                        }
                    }

                }
            }
            return Math.Abs(sum);
        }

        public decimal getIncomeForCurrentMonth()
        {
            return getTransactionsSumForCurrentMonth(true);
        }

        public decimal getExpensesForCurrentMonth()
        {
            return getTransactionsSumForCurrentMonth(false);
        }

        public List<Transaction> GetTransactions(int from = 0)
        {
            if (from < 0)
            {
                return null;
            }
            int to = from + 9;
            if (from > transactions.Count - 1)
            {
                return new List<Transaction>();
            }
            if (from + 10 > transactions.Count)
            {
                to = transactions.Count - 1;
            }
            return transactions.GetRange(from, to); ;
        }

        public bool Validate()
        {
            bool isValid = owner != null;
            if (string.IsNullOrWhiteSpace(Name))
            {
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(Currency))
            {
                isValid = false;
            }
            
            return isValid;
        }
    }
}