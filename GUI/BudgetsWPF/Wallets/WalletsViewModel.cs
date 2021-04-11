using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budgets.GUI.WPF.Navigation;
using Budgets.GUI.WPF.Navigation.AV.ProgrammingWithCSharp.Budgets.GUI.WPF.Navigation;
using Budgets.Models.Wallets;
using Budgets.Services;
using Prism.Mvvm;

namespace Budgets.GUI.WPF.Wallets
{
    public class WalletsViewModel : BindableBase, INavigatable<MainNavigatableTypes>
    {
        private WalletService _service;
        private WalletsDetailsViewModel _currentWallet;
        public ObservableCollection<WalletsDetailsViewModel> Wallets { get; set; }

        public WalletsDetailsViewModel CurrentWallet
        {
            get
            {
                return _currentWallet;
            }
            set
            {
                _currentWallet = value;
                RaisePropertyChanged();
            }
        }

        public WalletsViewModel()
        {
            _service = new WalletService();
            Wallets = new ObservableCollection<WalletsDetailsViewModel>();
            foreach (var wallet in _service.GetWallets())
            {
                Wallets.Add(new WalletsDetailsViewModel(wallet));
            }
        }


        public MainNavigatableTypes Type
        {
            get
            {
                return MainNavigatableTypes.Wallets;
            }
        }
        public void ClearSensitiveData()
        {

        }
    }
}
