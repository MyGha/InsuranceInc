using System.Text.Json.Serialization;

namespace InsuranceInc.Core.Models
{
    //
    // The Client model defines the data structure for a client that will be returned to the Client api controller methods.
    //
    public class Client
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonPropertyName("email")]
        public string Email { get; set; }
        
        [JsonPropertyName("role")]
        public string Role { get; set; }
    }
}
