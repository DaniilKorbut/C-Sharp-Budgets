namespace Budgets.BusinessLayer
{
    public class Category
    {
        private static int InstanceCount = 1;
        private int id;
        private string name;
        private string description;
        private string color;
        private string icon;

        public Category(string name, string color, string description = null, string icon = null)
        {
            Name = name;
            Color = color;
            Description = description;
            Icon = icon;
            Id = InstanceCount++;
        }

        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public string Color { get => color; set => color = value; }
        public string Icon { get => icon; set => icon = value; }
        public int Id { get => id; private set => id = value; }
    }
}