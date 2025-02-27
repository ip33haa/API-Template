using API.Application.Features.UserRole;
using API.Application.Features.UserRole.Commands.Create;
using API.Application.Features.UserRole.Queries.GetRoles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class RoleController : ApiControllerBase
    {
        [HttpGet("get-roles")]
        public async Task<ActionResult<GetRolesQueryResponse>> GetRoles()
        {
            var response = await Mediator.Send(new GetRolesQuery());

            return response;
        }

        [HttpPost("create-role")]
        public async Task<ActionResult<CreateRoleCommandResponse>> CreateRole(RoleDto command)
        {
            var result = await Mediator.Send(new CreateRoleCommand() { Role = command });

            return result;
        }
    }
}
