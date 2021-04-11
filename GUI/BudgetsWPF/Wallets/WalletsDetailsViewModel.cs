using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budgets.Models.Wallets;
using Prism.Mvvm;

namespace Budgets.GUI.WPF.Wallets
{
    public class WalletsDetailsViewModel : BindableBase
    {
        private Wallet _wallet;

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

        public string DisplayName
        {
            get
            {
                return $"{_wallet.Name} (${_wallet.Balance})";
            }
        }

        public WalletsDetailsViewModel(Wallet wallet)
        {
            _wallet = wallet;
        }
    }
}
