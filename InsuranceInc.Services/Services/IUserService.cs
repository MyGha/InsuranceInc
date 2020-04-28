using System.Threading.Tasks;
using InsuranceInc.Core.Entities;

namespace InsuranceInc.Business.Services
{
    public interface IUserService
    {
        Task<User> Authenticate(string userName, string password);
    }
}
