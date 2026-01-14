using System.Xml.Serialization;

namespace MauiProjectBNews.Models
{
    public enum NewsCategory
    {
        general, business, entertainment, health, science, sports, technology,
    }

    [XmlRoot("News", Namespace = "http://mynamespace/test/")] //ths to be able to deserialize the sample data
    public class News
    {
        private static readonly object _locker = new object();

        public NewsCategory Category { get; set; }
        public List<NewsItem> Articles { get; set; }
    }
}
