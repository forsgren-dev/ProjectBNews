namespace MauiProjectBNews
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();


            foreach (Models.NewsCategory category in Enum.GetValues(typeof(Models.NewsCategory)))
            {

                var flyOutItem = new FlyoutItem
                {
                    Title = $"🔵 {category.ToString().Substring(0, 1).ToUpper()}{category.ToString().Substring(1).ToLower()}",
                    FlyoutDisplayOptions = FlyoutDisplayOptions.AsSingleItem
                };

                flyOutItem.Items.Add(new ShellContent
                {
                    Title = $"{category.ToString().Substring(0, 1).ToUpper()}{category.ToString().Substring(1).ToLower()}",
                    ContentTemplate = new DataTemplate(() => new Views.NewsPage(category))

                });

                Items.Add(flyOutItem);
            }


        }
    }
}
