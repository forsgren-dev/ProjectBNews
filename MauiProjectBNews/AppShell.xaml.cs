namespace MauiProjectBNews
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            foreach (var category in Enum.GetValues(typeof(Models.NewsCategory)))   
            {
                var route = $"{category}";
                Routing.RegisterRoute(route, typeof(Views.NewsPage));

                var flyOutItem = new FlyoutItem
                {
                    Title = route.ToUpper(),
                    Route = route
                };

                flyOutItem.Items.Add(new ShellContent
                {
                    Title = route.ToUpper(),
                    Route = route,
                    ContentTemplate = new DataTemplate(typeof(Views.NewsPage))

                });

                Items.Add(flyOutItem);
            }


        }
    }
}
