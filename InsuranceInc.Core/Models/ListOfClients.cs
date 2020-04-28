using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace InsuranceInc.Core.Models
{
    //
    // Defines the data structure for the incoming Web service Clients data.
    //
    public class ListOfClients
    {
        [JsonPropertyName("clients")]
        public List<Client> Clients { get; set; }

        public override string ToString() => JsonSerializer.Serialize<ListOfClients>(this);
    }
}
