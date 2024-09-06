using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using TipoVkusvil.Core.Abstractions;

namespace TipoVkusvil.Infrastructure;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }
    
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        var userId = context.User.Claims.FirstOrDefault(c => c.Type == "userId");

        Guid id;
        
        try
        {
            id = Guid.Parse(userId.Value);
            if (userId is null || id == Guid.Empty)
            {
                return;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
        

        using var scope = _serviceScopeFactory.CreateScope();

        var permissionService = scope.ServiceProvider.GetRequiredService<IPermissionService>();

        var permissions = await permissionService.GetPermissionAsync(id);

        if (permissions.Intersect(requirement.Permissions).Any())
        {
            context.Succeed(requirement);
        }
    }
}