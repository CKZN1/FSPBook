using FSPBook.Portal.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace FSPBook.Portal.Services
{
    public class NewsService : INewsService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public IConfiguration Configuration { get; set; }

        public NewsService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            Configuration = configuration;
        }

        public async Task<List<Datum>> GetNews()
        { 
            var url = Configuration.GetSection("NewsAPIUrl").Value;
            if(string.IsNullOrEmpty(url)) 
                return new List<Datum>();

            var httpclient = _httpClientFactory.CreateClient();
            var response = await httpclient.GetAsync(url);

            var newsResponse = await response.Content.ReadAsStringAsync();
            var json = JsonSerializer.Deserialize<NewsAPIResponse>(newsResponse);

            if (json.data == null)
                return new List<Datum>();

            return json.data.ToList();
        }
    }
}
