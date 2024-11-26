using System;
using System.Text.Json.Serialization;

namespace FinalSite.Models
{
    public class BlogPost
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("datePosted")]
        public DateTime DatePosted { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }
    }
}
