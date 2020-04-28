using System.Collections.Generic;
using System.Threading.Tasks;
using InsuranceInc.Core.Models;

namespace InsuranceInc.Business.Services
{
    public interface IClientService
    {
        Task<Client> GetClientById(string id);
        Task<IEnumerable<Client>> GetClientsByName(string name);
        Task<string> GetClientIdByName(string name);
    }
}
