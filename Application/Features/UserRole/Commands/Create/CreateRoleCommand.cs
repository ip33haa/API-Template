using MediatR;

namespace API.Application.Features.UserRole.Commands.Create
{
    public class CreateRoleCommand : IRequest<CreateRoleCommandResponse>
    {
        public RoleDto Role { get; set; }
    }
}
