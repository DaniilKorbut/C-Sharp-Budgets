using Budgets.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budgets.BusinessLayer.Wallets;
using Budgets.GUI.WPF.Wallets;
using Budgets.Services;
using Prism.Commands;
using Prism.Mvvm;

namespace Budgets.GUI.WPF.Transactions
{
    public class TransactionsViewModel : BindableBase
    {
        private TransactionsService service;
        private TransactionDetailsViewModel currentTransaction;
        public ObservableCollection<TransactionDetailsViewModel> Transactions { get; set; }

        private WalletsDetailsViewModel wallet;

        private decimal initialTransactionSum;

        public TransactionDetailsViewModel CurrentTransaction
        {
            get => currentTransaction;
            set
            {
                if (currentTransaction != value)
                {
                    currentTransaction = value;
                    if (currentTransaction != null)
                    {
                        initialTransactionSum = currentTransaction.Sum;
                    }
                    RaisePropertyChanged();
                }
            }
        }

        public TransactionsViewModel(WalletsDetailsViewModel wallet)
        {
            service = new TransactionsService(wallet.Guid);
            var transactions = Task.Run(service.GetTransactions).Result;
            Transactions = new ObservableCollection<TransactionDetailsViewModel>();
            foreach (var transaction in transactions)
            {
                Transactions.Add(new TransactionDetailsViewModel(this, transaction));
            }
            this.wallet = wallet;

            AddTransactionCommand = new DelegateCommand(AddTransaction);
            RemoveAllTransactionsCommand = new DelegateCommand(RemoveAllTransactions);
            RemoveTransactionsCommand = new DelegateCommand(RemoveTransaction);

            decimal allSum = 0;
            decimal incomes = 0;
            decimal expenses = 0;
            foreach (Transaction transaction in transactions)
            {
                allSum += transaction.Sum;
                DateTimeOffset now = DateTimeOffset.Now;
                if (transaction.Date >= new DateTimeOffset(now.Year, now.Month, 1, 0, 0, 0, now.Offset))
                {
                    if (transaction.Sum > 0)
                    {
                        incomes += transaction.Sum;
                    }
                    else if (transaction.Sum < 0)
                    {
                        expenses += transaction.Sum;
                    }
                }
            }

            wallet.Balance = wallet.StartBalance + allSum;
            wallet.IncomeThisMonth = incomes;
            wallet.ExpensesThisMonth = expenses;
        }

        public void RemoveAllTransactions()
        {
            Transactions.Clear();
            service.DeleteTransaction(Guid.Empty);
        }

        public async void AddTransaction()
        {
            Transaction newTransaction = new Transaction(Guid.NewGuid(), 0.0m, "UAH", new Category("1"),  DateTimeOffset.Now, "");
            await service.AddOrUpdateTransactions(newTransaction);
            Transactions.Add(new TransactionDetailsViewModel(this, newTransaction));
        }

        public async void SaveTransaction()
        {
           await service.AddOrUpdateTransactions(new Transaction(CurrentTransaction.Guid, CurrentTransaction.Sum, CurrentTransaction.Currency, null, CurrentTransaction.Date, CurrentTransaction.Description));

            wallet.Balance += CurrentTransaction.Sum - initialTransactionSum;
            DateTimeOffset now = DateTimeOffset.Now;
            if (CurrentTransaction.Date >= new DateTimeOffset(now.Year, now.Month, 1, 0, 0, 0, now.Offset))
            {
                if (CurrentTransaction.Sum > 0)
                {
                    if (initialTransactionSum < 0)
                    {
                        wallet.IncomeThisMonth += CurrentTransaction.Sum;
                        wallet.ExpensesThisMonth -= initialTransactionSum;
                    }
                    else
                    {
                        wallet.IncomeThisMonth += CurrentTransaction.Sum - initialTransactionSum;
                    }
                }
                else if (CurrentTransaction.Sum < 0)
                {
                    if (initialTransactionSum > 0)
                    {
                        wallet.ExpensesThisMonth += CurrentTransaction.Sum;
                        wallet.IncomeThisMonth -= initialTransactionSum;
                    }
                    else
                    {
                        wallet.ExpensesThisMonth += CurrentTransaction.Sum - initialTransactionSum;
                    }
                }
            }
        }

        public void RemoveTransaction()
        {
            service.DeleteTransaction(CurrentTransaction.Guid);
            wallet.Balance -= CurrentTransaction.Sum;
                DateTimeOffset now = DateTimeOffset.Now;
                if (CurrentTransaction.Date >= new DateTimeOffset(now.Year, now.Month, 1, 0, 0, 0, now.Offset))
                {
                    if (CurrentTransaction.Sum > 0)
                    {
                        wallet.IncomeThisMonth -= initialTransactionSum;
                    }
                    else if (CurrentTransaction.Sum < 0)
                    {
                        wallet.ExpensesThisMonth -= initialTransactionSum;
                    }
                }

                Transactions.Remove(CurrentTransaction);
        }

        public DelegateCommand AddTransactionCommand { get; }
        public DelegateCommand RemoveAllTransactionsCommand { get; }
        public DelegateCommand RemoveTransactionsCommand { get; }
    }
}
