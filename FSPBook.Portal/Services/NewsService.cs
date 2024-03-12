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

            var httpclient = _httpClientFactory.CreateClient();
            var response = await httpclient.GetAsync(url);

            var newsResponse = await response.Content.ReadAsStringAsync();
            //var json = JsonSerializer.Deserialize<NewsAPIResponse>("{\"meta\":{\"found\":1662026,\"returned\":3,\"limit\":3,\"page\":1},\"data\":[{\"uuid\":\"6c5cef04-3e5f-4043-847f-9f50221bb490\",\"title\":\"onEco + Analytics - Capture all visits: GDPR-safe, Carbon conscious analytics\",\"description\":\"Privacy-focused, carbon conscious web analytics and insights platform which is also beautiful. No cookies or consent banners necessary. Certificate badge and pr...\",\"keywords\":\"\",\"snippet\":\"Support is great. Feedback is even better.\\n\\n\\\" Your thoughts mean the world to us! 🌍 How do you feel about onEco +Analytics? Any insights on our pricing or br...\",\"url\":\"https:\\/\\/www.producthunt.com\\/posts\\/oneco-analytics\",\"image_url\":\"https:\\/\\/ph-files.imgix.net\\/f90f0a3c-3844-4df2-9c17-2c8692cb0df2.jpeg?auto=format&fit=crop&frame=1&h=512&w=1024\",\"language\":\"en\",\"published_at\":\"2024-03-11T08:46:50.000000Z\",\"source\":\"producthunt.com\",\"categories\":[\"tech\",\"business\"],\"relevance_score\":null},{\"uuid\":\"6a67d17d-5018-4ccf-9383-89394fcea078\",\"title\":\"Bot in Bio powered by GPT - Create your Bot in Bio and get opportunity in your business!\",\"description\":\"A powerful Bot in Bio platform that combines GPT Chatbot and Bio creation with your data. Create Bio and get opportunity in your business. Cutting-edge LLMs lik...\",\"keywords\":\"\",\"snippet\":\"Free Options Discuss Collect Embed Share Stats\\n\\nA powerful Bot in Bio platform that combines GPT Chatbot and Bio creation with your data. Create Bio and get opp...\",\"url\":\"https:\\/\\/www.producthunt.com\\/posts\\/bot-in-bio-powered-by-gpt\",\"image_url\":\"https:\\/\\/ph-files.imgix.net\\/589d3b0f-e034-4ce7-8602-5ab88f53a113.png?auto=format&fit=crop&frame=1&h=512&w=1024\",\"language\":\"en\",\"published_at\":\"2024-03-11T08:46:50.000000Z\",\"source\":\"producthunt.com\",\"categories\":[\"tech\",\"business\"],\"relevance_score\":null},{\"uuid\":\"f9eb2939-342c-4ee0-bd51-7106cb625fab\",\"title\":\"Window Fusion - Merge multiple app windows into one window\",\"description\":\"This application allows users to merge multiple windows from different applications into a single window, enabling unified movement, scaling, activation, minimi...\",\"keywords\":\"\",\"snippet\":\"Support is great. Feedback is even better.\\n\\n\\\" Your feedback is very important to me; I welcome any comments about the product, price, or features. \\\"\",\"url\":\"https:\\/\\/www.producthunt.com\\/posts\\/window-fusion\",\"image_url\":\"https:\\/\\/ph-files.imgix.net\\/eaa09a1a-fd1e-429f-8594-effe2fe89d16.jpeg?auto=format&fit=crop&frame=1&h=512&w=1024\",\"language\":\"en\",\"published_at\":\"2024-03-11T08:46:50.000000Z\",\"source\":\"producthunt.com\",\"categories\":[\"tech\",\"business\"],\"relevance_score\":null}]}");
            var json = JsonSerializer.Deserialize<NewsAPIResponse>(newsResponse);
            return json.data.ToList();
        }
    }
}
