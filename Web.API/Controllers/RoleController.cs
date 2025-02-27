using API.Application.Features.UserRole.Queries.GetRoles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ApiControllerBase
    {
        [HttpGet("get-roles")]
        public async Task<ActionResult<GetRolesQueryResponse>> GetRoles()
        {
            var response = await Mediator.Send(new GetRolesQuery());

            return response;
        }
    }
}
