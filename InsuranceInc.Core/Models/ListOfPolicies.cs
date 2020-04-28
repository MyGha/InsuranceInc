using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace InsuranceInc.Core.Models
{
    //
    // Defines the data structure for the incoming Web service Policies data.
    //
    public class ListOfPolicies
    {
        [JsonPropertyName("policies")]
        public List<Policy> Policies { get; set; }

        public override string ToString() => JsonSerializer.Serialize<ListOfPolicies>(this);
    }
}
