using MauiProjectBNews.Services;

namespace MauiProjectBNews
{
    public partial class MainPage : ContentPage
    {
        private NewsService _service;



        public MainPage()
        {
            InitializeComponent();
            Title = "Project B: News";
            _service = new NewsService();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                var news = await _service.GetNewsAsync(MauiProjectBNews.Models.NewsCategory.business);
                ServiceLabel.Text = $"{news.Articles.Count} news articles read.";
            }
            catch (Exception ex)
            {
                ServiceLabel.Text = $"Error reading news: {ex.Message}";
            }
        }

    }
}
