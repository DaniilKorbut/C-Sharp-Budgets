using System.Windows.Controls;
using Budgets.GUI.WPF.Authentication;
using Budgets.GUI.WPF.Wallets;

namespace Budgets.GUI.WPF
{
    /// <summary>
    /// Логика взаимодействия для MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}
