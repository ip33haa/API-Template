using API.Application.Interface;
using API.Domain.Entity;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace API.Application.Features.UserRole.Commands.Update
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, UpdateRoleCommandResponse>
    {
        private readonly IRepository<Role> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public UpdateRoleCommandHandler(
            IRepository<Role> repository,
            IMapper mapper,
            ILogger<UpdateRoleCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<UpdateRoleCommandResponse> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var updateRoleCommandResponse = new UpdateRoleCommandResponse();
            var validator = new UpdateRoleCommandValidator(_repository);

            try
            {
                var validationResult = await validator.ValidateAsync(request, new CancellationToken());
                if (validationResult.Errors.Count > 0)
                {
                    updateRoleCommandResponse.Success = false;
                    updateRoleCommandResponse.ValidationErrors = new List<string>();
                    foreach (var error in validationResult.Errors.Select(x => x.ErrorMessage))
                    {
                        updateRoleCommandResponse.ValidationErrors.Add(error);
                        _logger.LogError($"validation failed due to error- {error}");
                    }
                }
                else if (updateRoleCommandResponse.Success)
                {
                    var entity = await _repository.GetByIdAsync(request.Id);
                    _mapper.Map(request.RoleUpdate, entity);
                    await _repository.UpdateAsync(entity);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"error while due to error- {ex.Message}");
                updateRoleCommandResponse.Success = false;
                updateRoleCommandResponse.Message = ex.Message;
            }

            return updateRoleCommandResponse;
        }
    }
}
