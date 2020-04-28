using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using InsuranceInc.Business.Helpers;
using InsuranceInc.Core.Entities;
using InsuranceInc.Core.Models;
using InsuranceInc.Data.Services;

namespace InsuranceInc.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IClientData _clientData;
        private readonly ILogger<UserService> _logger;

        #region Public Methods

        public UserService(IClientData clientData, ILogger<UserService> logger)
        {
            _clientData = clientData;
            _logger = logger;
        }

        // User data is taken from web service. For simplicity, passwords will be taken from the field Id (guid).
        // For security reasons store this data in a database with hashed passwords in production environment.
        public async Task<User> Authenticate(string userName, string password)
        {
            try
            {
                IEnumerable<Client> users = await _clientData.GetAllClients();

                Client client = await Task.Run(() => users.SingleOrDefault(x => x.Name == userName && x.Id == password));

                // Return null if user not found.
                if (client == null)
                {
                    return null;
                }
                else
                {
                    // Map Client to User.
                    User user = new User { Id = client.Id, UserName = client.Name, Password = client.Id, Role = client.Role };

                    // Authentication successful so return user details without password.
                    return user.WithoutPassword();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred in method Authenticate. {0}", ex);
                throw;
            }
        }

        #endregion
    }
}
