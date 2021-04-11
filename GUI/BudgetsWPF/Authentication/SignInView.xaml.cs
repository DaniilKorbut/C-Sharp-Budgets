using System;
using System.Windows;
using System.Windows.Controls;

namespace Budgets.GUI.WPF.Authentication
{
    /// <summary>
    /// Логика взаимодействия для SignInView.xaml
    /// </summary>
    public partial class SignInView : UserControl
    {

        public SignInView()
        {
            InitializeComponent();
            
        }

        
        private void Tb_Password_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            ((SignInViewModel)DataContext).Password = Tb_Password.Password;
        }
    }
}
