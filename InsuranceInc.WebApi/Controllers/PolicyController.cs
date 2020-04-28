using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InsuranceInc.Core.Entities;
using InsuranceInc.Core.Models;
using InsuranceInc.Business.Services;

namespace InsuranceInc.WebApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class PolicyController : ControllerBase
    {
        private readonly IPolicyService _policyService;

        #region Public Methods

        public PolicyController(IPolicyService policyService)
        {
            _policyService = policyService;
        }

        // GET: /Policy/"64cceef9-3a01-49ae-a23b-3761b604800b"
        [Authorize(Roles = RoleType.Admin)]
        [HttpGet("id/{id}", Name = "GetClientForPolicy")]
        public async Task<IActionResult> GetClientForPolicy(string id)
        {
            Client client = await _policyService.GetClientForPolicy(id);

            if (client == null)
            {
                return NotFound(client);
            }
            else
            {
                return Ok(client);
            }
        }

        // GET: /Policy/"Britney"
        [Authorize(Roles = RoleType.Admin)]
        [HttpGet("name/{name}", Name = "GetListPolicies")]
        public async Task<IActionResult> GetListPolicies(string name)
        {
            IEnumerable<Policy> policies = await _policyService.GetListPolicies(name);

            if (policies == null)
            {
                return NotFound(policies);
            }
            else
            {
                return Ok(policies);
            }
        }

        #endregion
    }
}
