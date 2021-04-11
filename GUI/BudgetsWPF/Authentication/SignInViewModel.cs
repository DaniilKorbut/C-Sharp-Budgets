using Prism.Commands;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using Budgets.Models.Users;
using Budgets.Services;

namespace Budgets.GUI.WPF.Authentication
{
    public class SignInViewModel : INotifyPropertyChanged, IAuthNavigatable
    {
        private AuthenticationUser authenticationUser = new AuthenticationUser();

        private Action goToSignUp; 
        private Action goToWallets;
        private bool isEnabled = true;
        public bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                isEnabled = value;
                OnPropertyChanged();
            }
        }

        public AuthNavigatableTypes Type
        {
            get => AuthNavigatableTypes.SignIn;
        }

        public void ClearSensitiveData()
        {
            Password = "";
        }


        public string Login
        {
            get => authenticationUser.Login;
            set
            {
                if (authenticationUser.Login != value)
                {
                    authenticationUser.Login = value;
                    OnPropertyChanged();
                    SignInCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public string Password
        {
            get => authenticationUser.Password;
            set {
                if (authenticationUser.Password != value)
                {
                    authenticationUser.Password = value;
                    OnPropertyChanged();
                    SignInCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public DelegateCommand SignInCommand { get; }
        public DelegateCommand SignUpCommand { get; }

        public SignInViewModel(Action goToSignUp, Action goToWallets)
        {
            SignInCommand = new DelegateCommand(SignIn, IsSignInEnabled);
            this.goToSignUp = goToSignUp;
            this.goToWallets = goToWallets;
            SignUpCommand = new DelegateCommand(this.goToSignUp);
        }

        private async void SignIn()
        {
            if (String.IsNullOrWhiteSpace(Login) || String.IsNullOrWhiteSpace(Password))
            {
                MessageBox.Show("Login or Password is empty");
            }
            else
            {


                var authService = new AuthenticationService();
                User user = null;
                try
                {
                    IsEnabled = false;
                    user = await Task.Run(() => authService.Authenticate(authenticationUser));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Sign in failed. Reason: " + ex.Message);
                    return;
                }
                finally
                {
                    IsEnabled = true;
                }

                MessageBox.Show($"Sign In successful for {user.FirstName} {user.LastName}");
                goToWallets.Invoke();
            }
        }

        private bool IsSignInEnabled()
        {
            return !String.IsNullOrWhiteSpace(Login) && !String.IsNullOrWhiteSpace(Password);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
