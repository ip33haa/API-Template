using API.Application.Response;

namespace API.Application.Features.UserRole.Commands.Create
{
    public class CreateRoleCommandResponse : BaseResponse
    {
        public Guid Id { get; set; }
    }
}