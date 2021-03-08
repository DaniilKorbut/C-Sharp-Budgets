using System;
using System.Collections.Generic;

namespace Budgets
{
    public class Wallet
    {
        private static int InstanceCount = 1;
        private int id;
        private string name;
        private decimal startBalance;
        private decimal currentBalance;
        private string description;
        private string currency;

        private User owner;

        private List<Transaction> transactions = new List<Transaction>();
        private List<Category> categories = new List<Category>();

        public Wallet(User owner, string name, decimal startBalance, string currency, string description = null)
        {
            this.name = name;
            this.startBalance = startBalance;
            this.currentBalance = startBalance;
            this.currency = currency;
            this.description = description;
            this.Id = InstanceCount++;
            this.owner = owner;
            this.owner.AddWallet(this);
        }

        public int Id { get => id; private set => id = value; }
        public string Name { get => name; set => name = value; }
        public decimal StartBalance { get => startBalance; set => startBalance = value; }
        public decimal CurrentBalance { get => currentBalance; private set => currentBalance = value; }
        public string Description { get => description; set => description = value; }
        public string Currency { get => currency; set => currency = value; }

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
            bool isValid = true;
            if(owner == null)
            {
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(currency))
            {
                isValid = false;
            }
            
            return isValid;
        }
    }
}