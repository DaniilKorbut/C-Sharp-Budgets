using Prism.Commands;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Budgets.Models.Users;
using Budgets.Services;

namespace Budgets.GUI.WPF.Authentication
{
    public class SignUpViewModel : INotifyPropertyChanged, IAuthNavigatable
    {
        private RegistrationUser regUser = new RegistrationUser();

        private Action goToSignIn;

        public AuthNavigatableTypes Type
        {
            get => AuthNavigatableTypes.SignUp;
        }

        public void ClearSensitiveData()
        {
            regUser = new RegistrationUser();
        }

        public string FirstName
        {
            get => regUser.FirstName;
            set
            {
                if (regUser.FirstName != value)
                {
                    regUser.FirstName = value;
                    OnPropertyChanged();
                    SignUpCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public string LastName
        {
            get => regUser.LastName;
            set
            {
                if (regUser.LastName != value)
                {
                    regUser.LastName = value;
                    OnPropertyChanged();
                    SignUpCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public string Email
        {
            get => regUser.Email;
            set
            {
                if (regUser.Email != value)
                {
                    regUser.Email = value;
                    OnPropertyChanged();
                    SignUpCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public string Login
        {
            get => regUser.Login;
            set
            {
                if (regUser.Login != value)
                {
                    regUser.Login = value;
                    OnPropertyChanged();
                    SignUpCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public string Password
        {
            get => regUser.Password;
            set {
                if (regUser.Password != value)
                {
                    regUser.Password = value;
                    OnPropertyChanged();
                    SignUpCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public DelegateCommand SignUpCommand { get; }
        public DelegateCommand SignInCommand { get; }

        public SignUpViewModel(Action goToSignIn)
        {
            SignUpCommand = new DelegateCommand(SignUp, IsSignUpEnabled);
            this.goToSignIn = goToSignIn;
            SignInCommand = new DelegateCommand(this.goToSignIn);
        }

        private void SignUp()
        {

            
               /* var authUser = new AuthenticationUser()
                {
                    Login = Tb_Login.Text,
                    Password = Tb_Password.Text
                };*/

                var authService = new AuthenticationService();
                try
                {
                    authService.RegisterUser(regUser);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Sign Up failed. Reason: " + ex.Message);
                    return;
                }

                MessageBox.Show("User successfully registered. Sign In to continue.");
                goToSignIn.Invoke();
        }

        private bool IsSignUpEnabled()
        {
            return !String.IsNullOrWhiteSpace(FirstName) && !String.IsNullOrWhiteSpace(LastName) && !String.IsNullOrWhiteSpace(Login) && !String.IsNullOrWhiteSpace(Password);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
