using API.Application.Interface;
using API.Domain.Entity;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Dapper;
using System.Data;

namespace API.Application.Features.UserRole.Queries.GetRoles
{
    public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, GetRolesQueryResponse>
    {
        private readonly IDbConnection _dbConnection;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        // Constructor takes an IDbConnection (which will be configured in dependency injection)
        public GetRolesQueryHandler(IDbConnection dbConnection, IMapper mapper, ILogger<GetRolesQueryHandler> logger)
        {
            _dbConnection = dbConnection;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetRolesQueryResponse> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var getRoleResponse = new GetRolesQueryResponse();
            var validator = new GetRolesQueryValidator();

            try
            {
                var validationResult = await validator.ValidateAsync(request, cancellationToken);
                if (validationResult.Errors.Count > 0)
                {
                    getRoleResponse.Success = false;
                    getRoleResponse.ValidationErrors = new List<string>();
                    foreach (var error in validationResult.Errors.Select(e => e.ErrorMessage))
                    {
                        getRoleResponse.ValidationErrors.Add(error);
                        _logger.LogError($"Validation failed due to error - {error}");
                    }
                }
                else if (getRoleResponse.Success)
                {
                    // Using Dapper to fetch roles from the database
                    var query = "SELECT Id, RoleName FROM Roles"; // Assuming your Roles table has these columns
                    var result = await _dbConnection.QueryAsync<Role>(query);

                    getRoleResponse.Roles = _mapper.Map<List<RoleDto>>(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred: {ex.Message}");
                getRoleResponse.Success = false;
                getRoleResponse.Message = ex.Message;
            }

            return getRoleResponse;
        }
    }
}
