using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using Budgets.GUI.WPF.Navigation;
using Budgets.GUI.WPF.Navigation.AV.ProgrammingWithCSharp.Budgets.GUI.WPF.Navigation;
using Prism.Mvvm;

namespace Budgets.GUI.WPF.Authentication
{
    public class AuthViewModel : NavigationBase<AuthNavigatableTypes>, INavigatable<MainNavigatableTypes>
    {
        private Action signInSuccess;

        public AuthViewModel(Action signInSuccess)
        {
            this.signInSuccess = signInSuccess;
            Navigate(AuthNavigatableTypes.SignIn);
        }

        protected override INavigatable<AuthNavigatableTypes> CreateViewModel(AuthNavigatableTypes type)
        {
            if (type == AuthNavigatableTypes.SignIn)
            {
                return new SignInViewModel(() => { Navigate(AuthNavigatableTypes.SignUp); }, signInSuccess);
            }
            else
            {
                return new SignUpViewModel(() => { Navigate(AuthNavigatableTypes.SignIn); });
            }
        }

        public MainNavigatableTypes Type
        {
            get
            {
                return MainNavigatableTypes.Auth;
            }
        }

        public void ClearSensitiveData()
        {
           CurrentViewModel.ClearSensitiveData();
        }
    }
}
