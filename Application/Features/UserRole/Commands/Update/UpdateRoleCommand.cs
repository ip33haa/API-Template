using MediatR;

namespace API.Application.Features.UserRole.Commands.Update
{
    public class UpdateRoleCommand : BaseCommandQuery, IRequest<UpdateRoleCommandResponse>
    {
        public RoleDto RoleUpdate {  get; set; }
    }
}
