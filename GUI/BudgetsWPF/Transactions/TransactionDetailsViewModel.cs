using System;
using System.Globalization;
using Budgets.BusinessLayer;
using Prism.Commands;
using Prism.Mvvm;

namespace Budgets.GUI.WPF.Transactions
{
    public class TransactionDetailsViewModel : BindableBase
    {
        private TransactionsViewModel transactionsViewModel;
        private Transaction transaction;

        public Guid Guid => transaction.Guid;

        public string DisplayName
        {
            get
            {
                return $"{transaction.Sum} {transaction.Currency} - {transaction.Date.ToString()}";
            }
        }

        public decimal Sum
        {
            get => transaction.Sum;
            set
            {
                transaction.Sum = value;
                RaisePropertyChanged();
                SaveTransactionCommand.RaiseCanExecuteChanged();
            }
        }

        public string Currency
        {
            get => transaction.Currency;
            set
            {
                transaction.Currency = value;
                RaisePropertyChanged();
                SaveTransactionCommand.RaiseCanExecuteChanged();
            }
        }

        public string Description
        {
            get => transaction.Description;
            set
            {
                transaction.Description = value;
                RaisePropertyChanged();
                SaveTransactionCommand.RaiseCanExecuteChanged();
            }
        }

        public DateTimeOffset Date
        {
            get => transaction.Date;
            set
            {
                transaction.Date = value;
                RaisePropertyChanged();
                SaveTransactionCommand.RaiseCanExecuteChanged();
            }
        }

        public string DateString
        {
            get => transaction.Date.ToString("dd.MM.yyyy HH:mm:ss");
            set
            {
                if (transaction.Date.ToString("dd.MM.yyyy HH:mm:ss") != value)
                {
                    Date = DateTimeOffset.ParseExact(value, "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None);
                }
            }
        }

        public string TransactionDisplayName => $"{Sum}: {Description} ({Date.LocalDateTime})";

        public DelegateCommand BackToListCommand { get; }
        public DelegateCommand SaveTransactionCommand { get; }
        public DelegateCommand RemoveTransactionCommand { get; }

        public TransactionDetailsViewModel(TransactionsViewModel transactionsViewModel, Transaction transaction)
        {
            
            this.transactionsViewModel = transactionsViewModel;

            this.transaction = transaction;

            BackToListCommand = new DelegateCommand(BackToList);
            SaveTransactionCommand = new DelegateCommand(SaveTransaction);
            RemoveTransactionCommand = new DelegateCommand(RemoveTransaction);
        }

        private void BackToList()
        {
            transactionsViewModel.CurrentTransaction = null;
        }

        private void SaveTransaction()
        {
            transactionsViewModel.SaveTransaction();

            RaisePropertyChanged(nameof(TransactionDisplayName));
            SaveTransactionCommand.RaiseCanExecuteChanged();
        }

        private void RemoveTransaction()
        {
            transactionsViewModel.RemoveTransaction();
        }
    }
}
