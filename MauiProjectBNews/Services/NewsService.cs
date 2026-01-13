#define UseNewsApiSample  // Remove or undefine to use your own code to read live data

using System.Collections.Concurrent;

using MauiProjectBNews.Models;
using MauiProjectBNews.Models.SampleData;
using System.Net;
using System.Net.Http.Json;

namespace MauiProjectBNews.Services
{
    public class NewsService
    {
        readonly string apiKey = "2a9f14287e7c426bb78777ed077748bd";
        public DateTime Fetched { get; set; } = new();

        HttpClient httpClient = new HttpClient();
        public NewsService()
        {
            httpClient = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate });
            httpClient.DefaultRequestHeaders.Add("user-agent", "News-API-csharp/0.1");
            httpClient.DefaultRequestHeaders.Add("x-api-key", apiKey);
        }
        public async Task<News> GetNewsAsync(NewsCategory category)
        {

#if !UseNewsApiSample      
            NewsApiData nd = await NewsApiSampleData.GetNewsApiSampleAsync(category);
#else
            //https://newsapi.org/docs/endpoints/top-headlines
            var uri = $"https://newsapi.org/v2/top-headlines?country=us&category={category}";

            // make the http request
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await httpClient.SendAsync(httpRequest);
            response.EnsureSuccessStatusCode();
            Fetched = DateTime.Now;

            //Convert Json to Object
            NewsApiData nd = await response.Content.ReadFromJsonAsync<NewsApiData>();
#endif
            var news = new News()
            {
                Category = category,
                Articles = nd.Articles.Select(ndi => new NewsItem()
                {
                    DateTime = ndi.PublishedAt,
                    Title = ndi.Title,
                    Url = ndi.Url,
                    UrlToImage = ndi.UrlToImage
                }).ToList()
            };
            return news;
        }
    }
}
