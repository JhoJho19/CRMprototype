using System.Text.Json.Serialization;

namespace ClientManagementApp.Models
{
    public class Client
    {
        [JsonPropertyName("id")]
        public int ID { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("addedAt")]
        public DateTime AddedAt { get; set; }
    }
}
