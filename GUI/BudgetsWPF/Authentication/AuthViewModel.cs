using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using Budgets.GUI.WPF.Navigation;
using Prism.Mvvm;

namespace Budgets.GUI.WPF.Authentication
{
    public class AuthViewModel : BindableBase, INavigatable<MainNavigatableTypes>
    {
        private List<IAuthNavigatable> viewModels = new List<IAuthNavigatable>();
        private Action signInSuccess;


        public IAuthNavigatable CurrentViewModel
        {
            get;
            private set;
        }

        public AuthViewModel(Action signInSuccess)
        {
            this.signInSuccess = signInSuccess;
            Navigate(AuthNavigatableTypes.SignIn);
        }

        public void Navigate(AuthNavigatableTypes type)
        {
            if (CurrentViewModel !=null && CurrentViewModel.Type == type)
            {
                return;
            }
            IAuthNavigatable viewModel = viewModels.FirstOrDefault(authNavigatable => authNavigatable.Type == type);

            if (viewModel == null)
            {
                viewModel = CreateViewModel(type);
                viewModels.Add(viewModel);
            }
            viewModel.ClearSensitiveData();
            CurrentViewModel = viewModel;
            RaisePropertyChanged(nameof(CurrentViewModel));
        }

        private IAuthNavigatable CreateViewModel(AuthNavigatableTypes type)
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
