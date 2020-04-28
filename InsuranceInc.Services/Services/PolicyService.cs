using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using InsuranceInc.Core.Models;
using InsuranceInc.Data.Services;

namespace InsuranceInc.Business.Services
{
    public class PolicyService : IPolicyService
    {
        private readonly IPolicyData _policyData;
        private readonly IClientService _clientService;
        private readonly ILogger<PolicyService> _logger;

        #region Public Methods

        public PolicyService(IPolicyData policyData, IClientService clientService, ILogger<PolicyService> logger)
        {
            _policyData = policyData;
            _clientService = clientService;
            _logger = logger;
        }

        public async Task<Client> GetClientForPolicy(string id)
        {
            try
            {
                IEnumerable<Policy> policies = await _policyData.GetAllPolicies();

                // 1 - Find the ClientId related to the given Policy.
                Policy policy = policies.FirstOrDefault(p => p.Id == id);

                if (policy != null)
                {
                    // 2 - Retrieve the related Client data.
                    Client client = await _clientService.GetClientById(policy.ClientId);

                    if (client != null)
                    {
                        return client;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred in method GetClientForPolicy. {0}", ex);
                throw;
            }
        }

        public async Task<IEnumerable<Policy>> GetListPolicies(string name)
        {
            try
            {
                // 1 - Find the ClientId for a given Username.
                string clientId = await _clientService.GetClientIdByName(name);

                if (clientId == null)
                {
                    return null;
                }
                else
                {
                    // 2 - Retrieve all related Policies for this ClientId.
                    IEnumerable<Policy> policies = await _policyData.GetAllPolicies();

                    IEnumerable<Policy> policiesForClientId = policies.Where(p => p.ClientId == clientId);

                    if (policiesForClientId.Any())
                    {
                        return policiesForClientId;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred in method GetListPolicies. {0}", ex);
                throw;
            }
        }

        #endregion
    }
}
