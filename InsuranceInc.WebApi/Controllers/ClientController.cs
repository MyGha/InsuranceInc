using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InsuranceInc.Core.Entities;
using InsuranceInc.Business.Services;
using InsuranceInc.Core.Models;

namespace InsuranceInc.WebApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        #region Public Methods

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        // GET: api/Client/a0ece5db-cd14-4f21-812f-966633e7be86
        [Authorize(Roles = RoleType.AdminAndUsers)]
        [HttpGet("id/{id}", Name = "GetClientById")]
        public async Task<ActionResult<Client>> GetClientById(string id)
        {
            Client client = await _clientService.GetClientById(id);

            if (client == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(client);
            }
        }

        // GET: api/Client/Britney
        [Authorize(Roles = RoleType.AdminAndUsers)]
        [HttpGet("name/{name}", Name = "GetClientByName")]
        public async Task<ActionResult<Client>> GetClientByName(string name)
        {
            IEnumerable<Client> clients = await _clientService.GetClientsByName(name);

            if (clients == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(clients);
            }
        }

        #endregion
    }
}
