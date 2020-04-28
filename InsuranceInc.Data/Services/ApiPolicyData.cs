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
    public class ApiPolicyData : IPolicyData
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;

        #region Public Methods

        public ApiPolicyData(HttpClient httpClient, IMemoryCache cache)
        {
            _httpClient = httpClient;
            _cache = cache;
        }

        public async Task<IEnumerable<Policy>> GetAllPolicies()
        {
            IEnumerable<Policy> policyData;
            string key = "CachingKey";

            HttpResponseMessage Response = await _httpClient.GetAsync("v2/580891a4100000e8242b75c5");

            if (Response.IsSuccessStatusCode)
            {
                using Stream responseStream = await Response.Content.ReadAsStreamAsync();
                ListOfPolicies listOfPolicies = await JsonSerializer.DeserializeAsync<ListOfPolicies>(responseStream);

                policyData = listOfPolicies.Policies;

                // Store data in the cache.
                _cache.Set<IEnumerable<Policy>>(key, policyData);

                return policyData;
            }
            else
            {
                // In case webservice is not reachable/does not respond return persisted
                // data from cache (from last succesfull call to Webservice).
                if (!_cache.TryGetValue(key, out policyData))
                {
                    throw new Exception("Policies data cannot be retrieved from backend webservice and there is no policies data available in the cache.");
                }
                else
                {
                    // Return persistent data from last succesfull call to Webservice.
                    return policyData;
                }
            }
        }

        #endregion
    }
}
