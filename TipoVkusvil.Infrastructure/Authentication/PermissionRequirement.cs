using Microsoft.AspNetCore.Authorization;
using TipoVkusvil.Core.Enums;

namespace TipoVkusvil.Infrastructure;

public class PermissionRequirement(Permission[] permissions) : IAuthorizationRequirement
{
    
    public Permission[] Permissions { get; set; } = permissions;
}