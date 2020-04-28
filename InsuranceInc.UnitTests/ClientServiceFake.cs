using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InsuranceInc.Core.Models;

namespace InsuranceInc.Business.Services
{
    public class ClientServiceFake : IClientService
    {
        private readonly List<Client> _clientData;
        //private readonly ILogger<ClientService> _logger;

        #region Public Methods

        public ClientServiceFake()
        {
            _clientData = new List<Client>()
                {
                    new Client { Id = "a0ece5db-cd14-4f21-812f-966633e7be86",
                                Name = "Britney",
                                Email= "britneyblankenship@quotezart.com",
                                Role = "admin"},
                    new Client { Id = "a3b8d425-2b60-4ad7-becc-bedf2ef860bd",
                                 Name = "Barnett",
                                 Email = "barnettblankenship@quotezart.com",
                                 Role = "user"},
                    new Client { Id = "1470c601-6833-48a4-85b4-ddab9c045916",
                                 Name ="Jerry",
                                 Email = "jerryblankenship@quotezart.com",
                                 Role = "user"}
                };
        }

        // Returns the Client data for a given ClientId.
        // Assumption: each Client has a unique ClientId.
        public async Task<Client> GetClientById(string id)
        {
            try
            {
                await Task.Delay(100);
                IEnumerable<Client> clients = _clientData;
                Client client = clients.FirstOrDefault(x => x.Id == id);

                return client;
            }
            catch (Exception ex)
            {
                //_logger.LogError("An error occurred in method GetClientById. {0}", ex);
                throw;
            }
        }

        // Returns all Clients for a given Client name.
        // Possibly more than 1 Client can be found if Client name is not unique.
        public async Task<IEnumerable<Client>> GetClientsByName(string name)
        {
            try
            {
                await Task.Delay(100);
                IEnumerable<Client> clients = _clientData;
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
                //_logger.LogError("An error occurred in method GetClientsByName. {0}", ex);
                throw;
            }
        }

        // Returns the ClientId for a client with the given name.
        // In case more than 1 Clients are found, it only return the ClientId for the first Client found.
        public async Task<string> GetClientIdByName(string name)
        {
            try
            {
                await Task.Delay(100);
                IEnumerable<Client> clients = _clientData;
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
                //_logger.LogError("An error occurred in method GetClientIdByName. {0}", ex);
                throw;
            }
        }

        #endregion
    }
}
