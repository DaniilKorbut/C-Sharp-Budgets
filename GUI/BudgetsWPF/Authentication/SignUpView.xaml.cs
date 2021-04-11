using System;
using System.Windows;
using System.Windows.Controls;

namespace Budgets.GUI.WPF.Authentication
{
    /// <summary>
    /// Логика взаимодействия для SignInView.xaml
    /// </summary>
    public partial class SignUpView : UserControl
    {
        public SignUpView()
        {
            InitializeComponent();
        }


        private void Tb_Password_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            ((SignUpViewModel)DataContext).Password = Tb_Password.Password;
        }
    }
}
