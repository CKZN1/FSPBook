using System;

namespace FSPBook.Portal.Models
{
    public class NewsAPIResponse
    {
        public Meta meta { get; set; }
        public Datum[] data { get; set; }
    }

    public class Meta
    {
        public int found { get; set; }
        public int returned { get; set; }
        public int limit { get; set; }
        public int page { get; set; }
    }

    public class Datum
    {
        public string uuid { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string keywords { get; set; }
        public string snippet { get; set; }
        public string url { get; set; }
        public string image_url { get; set; }
        public string language { get; set; }
        public DateTime published_at { get; set; }
        public string source { get; set; }
        public string[] categories { get; set; }
        public object relevance_score { get; set; }
    }
}
