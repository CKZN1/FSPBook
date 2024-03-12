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
        private const string APITokenReplace = "**APITOKEN**";
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
            var token = Configuration.GetSection("NewsAPIToken").Value;
            if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(token)) 
                return new List<Datum>();

            var urltoken = url.Replace(APITokenReplace, token);

            var httpclient = _httpClientFactory.CreateClient();
            var response = await httpclient.GetAsync(urltoken);

            var newsResponse = await response.Content.ReadAsStringAsync();
            var json = JsonSerializer.Deserialize<NewsAPIResponse>(newsResponse);

            if (json.data == null)
                return new List<Datum>();

            return json.data.ToList();
        }
    }
}
