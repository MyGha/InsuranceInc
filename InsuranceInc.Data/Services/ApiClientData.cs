using System;
using System.IO;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using InsuranceInc.Core.Models;

namespace InsuranceInc.Data.Services
{
    public class ApiClientData : IClientData
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;

        #region Public Methods

        public ApiClientData(HttpClient httpClient, IMemoryCache cache)
        {
            _httpClient = httpClient;
            _cache = cache;
        }

        public async Task<IEnumerable<Client>> GetAllClients()
        {
            IEnumerable<Client> clientsData;
            string key = "CachingKey";

            HttpResponseMessage Response = await _httpClient.GetAsync("v2/5808862710000087232b75ac");

            if (Response.IsSuccessStatusCode)
            {
                using Stream responseStream = await Response.Content.ReadAsStreamAsync();
                ListOfClients listOfClients = await JsonSerializer.DeserializeAsync<ListOfClients>(responseStream);

                clientsData = listOfClients.Clients;

                // Store data in the cache.
                _cache.Set<IEnumerable<Client>>(key, clientsData);

                return clientsData;
            }
            else
            {
                // In case webservice is not reachable/does not respond return persisted
                // data from cache (from last succesfull call to Webservice).
                if (!_cache.TryGetValue(key, out clientsData))
                {
                    throw new Exception("Clients data cannot be retrieved from backend webservice and there is no clients data available in the cache.");
                }
                else
                {
                    // Return persistent data from last succesfull call to Webservice.
                    return clientsData;
                }
            }
        }

        #endregion
    }
}
