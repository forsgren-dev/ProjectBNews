using MauiProjectBNews.Services;
using Microsoft.Maui.Controls;
using System.Linq;

namespace MauiProjectBNews
{
    public partial class MainPage : ContentPage
    {
        private NewsService _service;

        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            _service = new NewsService();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                var news = await _service.GetNewsAsync(MauiProjectBNews.Models.NewsCategory.business);
                ServiceLabel.Text = $"{news.Articles.Count} news article read.";   
            }
            catch (Exception ex)
            {
                ServiceLabel.Text = $"Error reading news: {ex.Message}";
            }
        }
        
    }
}
