using System.Collections.Generic;
using System.Threading.Tasks;
using InsuranceInc.Core.Models;

namespace InsuranceInc.Business.Services
{
    public interface IPolicyService
    {
        Task<IEnumerable<Policy>> GetListPolicies(string name);
        Task<Client> GetClientForPolicy(string id);
    }
}
