using System.Text.Json;
using System.Text.Json.Serialization;

namespace InsuranceInc.Core.Models
{
    //
    // The Policy model defines the data structure for a policy that will be returned to the Policy api controller methods.
    //
    public class Policy
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        
        [JsonPropertyName("amountInsured")]
        public float AmountInsured { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("inceptionDate")]
        public string InceptionDate { get; set; }

        [JsonPropertyName("installmentPayment")]
        public bool InstallmentPayment { get; set; }

        [JsonPropertyName("clientId")]
        public string ClientId { get; set; }

        public override string ToString() => JsonSerializer.Serialize<Policy>(this);
    }
}
