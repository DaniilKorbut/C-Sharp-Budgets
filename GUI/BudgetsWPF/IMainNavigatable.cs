namespace Budgets.GUI.WPF
{
    public enum MainNavigatableTypes
    {
        Auth, Wallets
    }
    public interface IMainNavigatable
    {
        public MainNavigatableTypes Type { get; }

        public void ClearSensitiveData();
    }
}
