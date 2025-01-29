using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace MauiProjectBNews.Models;

public enum NewsCategory { Business, Entertainment, Technology, Sports, World };

public class NewsResponse
{
    static readonly object _locker = new object();
    public NewsCategory Category {get; set;}

    [JsonPropertyName("value")]
    public List<NewsArticle> Articles { get; set; }

    //XML cache serialization
    public static void Serialize(NewsResponse news, string fname)
    {
        lock (_locker)
        { 
            var xs = new XmlSerializer(typeof(NewsResponse));
            using (Stream s = File.Create(fname))
                xs.Serialize(s, news);
        }
    }
    public static NewsResponse Deserialize(string fname)
    {
        lock (_locker)
        {
            NewsResponse news;

            var xs = new XmlSerializer(typeof(NewsResponse));
            using (Stream s = File.OpenRead(fname))
                news = (NewsResponse)xs.Deserialize(s);

            return news;
        }
    }
}
public class NewsArticle
{
    [JsonPropertyName("name")]
    public string Title { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("datePublished")]
    public DateTime DatePublished {get; set; }

    [JsonPropertyName("provider")]
    public List<NewsProvider> Providers { get; set; }

    [JsonPropertyName("image")]
    public NewsImage Image { get; set; }
}

public class NewsProvider
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
}

public class NewsImage
{
    [JsonPropertyName("thumbnail")]
    public NewsThumbnail Thumbnail { get; set; }
}

public class NewsThumbnail
{
    [JsonPropertyName("contentUrl")]
    public string ContentUrl { get; set; }

    [JsonPropertyName("width")]
    public int Width { get; set; }

    [JsonPropertyName("height")]
    public int Height { get; set; }
}
