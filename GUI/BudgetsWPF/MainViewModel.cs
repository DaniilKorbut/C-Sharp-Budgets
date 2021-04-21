using System;
using System.Collections.Generic;
using System.Linq;
using Budgets.GUI.WPF.Authentication;
using Budgets.GUI.WPF.Navigation;
using Budgets.GUI.WPF.Navigation.AV.ProgrammingWithCSharp.Budgets.GUI.WPF.Navigation;
using Budgets.GUI.WPF.Wallets;
using Prism.Mvvm;

namespace Budgets.GUI.WPF
{
    public class MainViewModel : NavigationBase<MainNavigatableTypes>
    {
        public MainViewModel()
        {
            Navigate(MainNavigatableTypes.Auth);
        }

        protected override INavigatable<MainNavigatableTypes> CreateViewModel(MainNavigatableTypes type)
        {
            if (type == MainNavigatableTypes.Auth)
            {
                return new AuthViewModel(() => Navigate(MainNavigatableTypes.Wallets));
            }
            else
            {
                return new WalletsViewModel(() => Navigate(MainNavigatableTypes.Auth));
            }
        }
    }
}
