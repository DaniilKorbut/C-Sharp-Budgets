using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Budgets.BusinessLayer.Users;
using Budgets.BusinessLayer.Wallets;
using Budgets.Services;
using Prism.Commands;
using Prism.Mvvm;

namespace Budgets.GUI.WPF.Wallets
{
    public class WalletsDetailsViewModel : BindableBase
    {
        private Wallet _wallet;

        private decimal incomesThisMonth;
        private decimal expensesThisMonth;

        public DelegateCommand SaveWalletCommand { get; }

        public Guid Guid
        {
            get
            {
                return _wallet.Guid;
            }
        }

        public string Name
        {
            get
            {
                return _wallet.Name;
            }
            set
            {
                _wallet.Name = value;
                RaisePropertyChanged(nameof(DisplayName));
            }
        }

        public decimal Balance
        {
            get
            {
                return _wallet.Balance;
            }
            set
            {
                _wallet.Balance = value;
                RaisePropertyChanged(nameof(DisplayName));
            }
        }

        public decimal StartBalance
        {
            get
            {
                return _wallet.StartBalance;
            }
            set
            {
                _wallet.StartBalance = value;
                RaisePropertyChanged(nameof(DisplayName));
            }
        }

        public decimal IncomeThisMonth
        {
            get
            {
                return incomesThisMonth;
            }
            set
            {
                incomesThisMonth = value;
                RaisePropertyChanged(nameof(DisplayName));
            }
        }

        public decimal ExpensesThisMonth
        {
            get
            {
                return expensesThisMonth;
            }
            set
            {
                expensesThisMonth = value;
                RaisePropertyChanged(nameof(DisplayName));
            }
        }

        public string Description
        {
            get
            {
                return _wallet.Description;
            }
            set
            {
                _wallet.Description = value;
                RaisePropertyChanged(nameof(Description));
            }
        }

        public string Currency
        {
            get
            {
                return _wallet.Currency;
            }
            set
            {
                _wallet.Currency = value;
                RaisePropertyChanged(nameof(Currency));
            }
        }

        public string DisplayName
        {
            get
            {
                return $"{_wallet.Name}";
            }
        }

        public WalletsDetailsViewModel(Wallet wallet)
        {
            _wallet = wallet;
            SaveWalletCommand = new DelegateCommand(saveCurrentWallet);
        }

        public async void saveCurrentWallet()
        {
            var service = new WalletService();
            await service.AddOrUpdateWallet(_wallet);
            MessageBox.Show("Wallet successfully saved", "Operation succeed");
        }
    }
}
