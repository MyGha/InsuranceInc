using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using InsuranceInc.Core.Models;
using InsuranceInc.Data.Services;

namespace InsuranceInc.Business.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientData _clientData;
        private readonly ILogger<ClientService> _logger;

        #region Public Methods

        public ClientService(IClientData clientData, ILogger<ClientService> logger)
        {
            _clientData = clientData;
            _logger = logger;
        }

        // Returns the Client data for a given ClientId.
        // Assumption: each Client has a unique ClientId.
        public async Task<Client> GetClientById(string id)
        {
            try
            {
                IEnumerable<Client> clients = await _clientData.GetAllClients();
                Client client = clients.FirstOrDefault(x => x.Id == id);

                return client;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred in method GetClientById. {0}", ex);
                throw;
            }
         }
        
        // Returns all Clients for a given Client name.
        // Possibly more than 1 Client can be found if Client name is not unique.
        public async Task<IEnumerable<Client>> GetClientsByName(string name)
        {
            try
            {
                IEnumerable<Client> clients = await _clientData.GetAllClients();
                IEnumerable<Client> clientsData = clients.Where(x => x.Name == name);

                if (clientsData.Any())
                {
                    return clientsData;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred in method GetClientsByName. {0}", ex);
                throw;
            }
         }

        // Returns the ClientId for a client with the given name.
        // In case more than 1 Clients are found, it only return the ClientId for the first Client found.
        public async Task<string> GetClientIdByName(string name)
        {
            try
            {
                IEnumerable<Client> clients = await _clientData.GetAllClients();
                Client client = clients.FirstOrDefault(x => x.Name == name);

                if (client != null)
                {
                    return client.Id;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred in method GetClientIdByName. {0}", ex);
                throw;
            }
        }

        #endregion
    }
}
