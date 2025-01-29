//#define UseNewsApiSample  // Remove or undefine to use your own code to read live data

using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json; 
using System.Threading.Tasks;

using MauiProjectBNews.Models;

namespace MauiProjectBNews.Services
{
    public class NewsService
    {
        readonly string _subscriptionKey = "256970bad92b4d5398613d17fcba4a7f";
        readonly string _endpoint = "https://api.bing.microsoft.com/v7.0/news";
        readonly HttpClient _httpClient = new HttpClient();
        
        public NewsService()
        {
            _httpClient = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate });
            _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _subscriptionKey);
        }
        public async Task<NewsResponse> GetNewsAsync(NewsCategory category)
        {
            // make the http request and ensure success
            string uri = $"{_endpoint}?mkt=en-us&category={Uri.EscapeDataString(category.ToString())}";
            HttpResponseMessage response = await _httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            //To ensure not too many requests per second for BingNewsApi free plan
            await Task.Delay(50);

            var newsResponse = await response.Content.ReadFromJsonAsync<NewsResponse>();
            newsResponse.Category = category;
            
            return newsResponse;
        }
    }
}
