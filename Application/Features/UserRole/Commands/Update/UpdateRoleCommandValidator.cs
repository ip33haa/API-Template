using API.Application.Interface;
using API.Domain.Entity;
using FluentValidation;

namespace API.Application.Features.UserRole.Commands.Update
{
    public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
    {
        private IRepository<Role> _repository;

        public UpdateRoleCommandValidator(IRepository<Role> repository)
        {
            _repository = repository;

            RuleFor(p => p.RoleUpdate.RoleName)
                .NotEmpty()
                .NotNull()
                .WithMessage("{PropertyName} should have value.");
        }
    }
}