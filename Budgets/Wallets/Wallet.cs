using Budgets.BusinessLayer.Users;
using DataStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgets.BusinessLayer.Wallets
{
    public class Wallet : IStorable
    {
        public User owner { get; set; }

        public string Name { get; set; }
        public decimal Balance { get; set; }

        public Guid Guid { get; private set; }

        public decimal StartBalance { get; set; }

        public string Description { get; set; }

        public string Currency { get; set; }

        private List<Transaction> transactions = new List<Transaction>();
        private List<Category> categories = new List<Category>();

        public Wallet(User owner, Guid guid, string name, decimal startBalance, string currency, string description = null)
        {
            Guid = guid;
            Name = name;
            StartBalance = startBalance;
            Balance = startBalance;
            Currency = currency;
            Description = description;
            this.owner = owner;
        }

        public void AddTransaction(decimal sum, string currency, Category category, DateTime date, string description = null, List<string> files = null)
        {
            transactions.Add(new Transaction(this, sum, currency, category, date, description, files));
            Balance = Balance + sum;
        }

        public bool DeleteTransaction(int id)
        {
            foreach (Transaction t in transactions)
            {
                if (t.Id == id)
                {
                    transactions.Remove(t);
                    Balance -= t.Sum;
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
