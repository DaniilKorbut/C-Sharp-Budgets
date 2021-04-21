using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Budgets.GUI.WPF.Navigation;
using Budgets.GUI.WPF.Navigation.AV.ProgrammingWithCSharp.Budgets.GUI.WPF.Navigation;
using Budgets.BusinessLayer.Wallets;
using Budgets.Services;
using Prism.Mvvm;
using Prism.Commands;
using Budgets.BusinessLayer.Users;
using System.Windows;
using Budgets.GUI.WPF.Transactions;

namespace Budgets.GUI.WPF.Wallets
{
    public class WalletsViewModel : BindableBase, INavigatable<MainNavigatableTypes>
    {
        private WalletService _service;
        private WalletsDetailsViewModel _currentWallet;
        private TransactionsViewModel _transactions;
        public ObservableCollection<WalletsDetailsViewModel> Wallets { get; set; }

        public DelegateCommand AddWalletCommand { get; }
        public DelegateCommand DeleteWalletCommand { get; }
        public DelegateCommand SignOutCommand { get; }


        public WalletsDetailsViewModel CurrentWallet
        {
            get
            {
                return _currentWallet;
            }
            set
            {
                _currentWallet = value;
                if (value != null)
                {
                    Transactions = new TransactionsViewModel(value);
                }
                else
                {
                    Transactions = null;
                }

                RaisePropertyChanged();
                DeleteWalletCommand.RaiseCanExecuteChanged();
            }
        }

        public TransactionsViewModel Transactions
        {
            get
            {
                return _transactions;
            }
            set
            {
                _transactions = value;
                RaisePropertyChanged();
            }
        }

        public WalletsViewModel(Action goToSignIn)
        {
            _service = new WalletService();
            Wallets = new ObservableCollection<WalletsDetailsViewModel>();
            AddWalletCommand = new DelegateCommand(addWallet);
            DeleteWalletCommand = new DelegateCommand(deleteWallet, ()=> CurrentWallet != null );
            SignOutCommand = new DelegateCommand(goToSignIn);

            var w = Task.Run(_service.GetWallets).Result;
            foreach (var wallet in w)
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
            CurrentWallet = null;
            _service = new WalletService();
            Wallets = new ObservableCollection<WalletsDetailsViewModel>();
            var w = Task.Run(_service.GetWallets).Result;
            foreach (var wallet in w)
            {
                Wallets.Add(new WalletsDetailsViewModel(wallet));
            }
        }

        private async void addWallet()
        {
            var wallet = new Wallet(CurrentUser.User, Guid.NewGuid(), "New Wallet", 0, "UAH");
            await _service.AddOrUpdateWallet(wallet);
            Wallets.Add(new WalletsDetailsViewModel(wallet));
        }
        private void deleteWallet()
        {
            var Result = MessageBox.Show("Are you sure you want to delete this wallet?", "Deleting current wallet", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (Result == MessageBoxResult.Yes)
            {
                _service.DeleteWallet(CurrentWallet.Guid);
                Wallets.Remove(CurrentWallet);
            }
        }

       
    }
}
