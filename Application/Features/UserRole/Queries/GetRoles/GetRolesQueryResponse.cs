using API.Application.Response;

namespace API.Application.Features.UserRole.Queries.GetRoles
{
    public class GetRolesQueryResponse : BaseResponse
    {
        public List<RoleDto> Roles { get; set; }
    }
}
