using MauiProjectBNews.Models;
using MauiProjectBNews.Services;

namespace MauiProjectBNews.Views
{

    public partial class NewsPage : ContentPage
    {
        private readonly NewsService _newsService = new();
        private readonly NewsCategory _category;
        private News _cached;

        public NewsPage(NewsCategory category)
        {
            InitializeComponent();

            _category = category;
            Title = $"{category.ToString().Substring(0, 1).ToUpper()}{category.ToString().Substring(1)}";
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (_cached == null)
            {
                LoadNewsAsync();
            }
        }

        private async void OnRefreshClicked(object sender, EventArgs e)
        {
            LoadNewsAsync();
        }

        private async void NewsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (NewsItem)e.CurrentSelection?.FirstOrDefault();

            if (item.Url == null) return;

            await Navigation.PushAsync(new ArticleView(item.Url));

        }

        private async Task LoadNewsAsync()
        {
            try
            {
                this.IsBusy = true;
                var news = await _newsService.GetNewsAsync(_category);
                _cached = news;
                NewsList.ItemsSource = news.Articles;
                LastUpdated.Text = $"Last updated: {_newsService.Fetched:g}";
            }
            catch (Exception ex)
            {
                await DisplayAlert("Fel", ex.Message, "OK");
            }
            finally
            {
                this.IsBusy = false;
            }
        }
    }
}
