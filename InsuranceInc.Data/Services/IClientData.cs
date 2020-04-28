using System.Collections.Generic;
using System.Threading.Tasks;
using InsuranceInc.Core.Models;

namespace InsuranceInc.Data.Services
{
    public interface IClientData
    {
        Task<IEnumerable<Client>> GetAllClients();
    }
}
