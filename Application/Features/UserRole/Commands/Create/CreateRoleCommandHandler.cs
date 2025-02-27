using API.Application.Interface;
using API.Domain.Entity;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace API.Application.Features.UserRole.Commands.Create
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, CreateRoleCommandResponse>
    {
        private readonly IRepository<Role> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateRoleCommandHandler> _logger;

        public CreateRoleCommandHandler(
            IRepository<Role> repository,
            IMapper mapper,
            ILogger<CreateRoleCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<CreateRoleCommandResponse> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var createRoleCommandResponse = new CreateRoleCommandResponse();
            var validator = new CreateRoleCommandValidator();

            try
            {
                var validationResult = await validator.ValidateAsync(request, new CancellationToken());
                if (validationResult.Errors.Count > 0)
                {
                    createRoleCommandResponse.Success = false;
                    createRoleCommandResponse.ValidationErrors = new List<string>();
                    foreach (var error in validationResult.Errors.Select(x => x.ErrorMessage))
                    {
                        createRoleCommandResponse.ValidationErrors.Add(error);
                        _logger.LogError($"validation failed due to error- {error}");
                    }
                }
                else if (createRoleCommandResponse.Success)
                {
                    var entity = _mapper.Map<Role>(request.Role);
                    var result = await _repository.AddAsync(entity);
                    createRoleCommandResponse.Id = result.Id;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"error while due to error- {ex.Message}");
                createRoleCommandResponse.Success = false;
                createRoleCommandResponse.Message = ex.Message;
            }

            return createRoleCommandResponse;
        }
    }
}
