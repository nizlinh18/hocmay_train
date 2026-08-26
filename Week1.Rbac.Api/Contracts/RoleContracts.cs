using System.ComponentModel.DataAnnotations;

namespace Week1.Rbac.Api.Contracts;

public sealed record CreateRoleRequest(
    [property: Required, MaxLength(100)]
    string Name,

    [property: MaxLength(500)]
    string Description
);

public sealed record UpdateRoleRequest(
    [property: Required, MaxLength(100)]
    string Name,

    [property: MaxLength(500)]
    string Description
);

public sealed record RoleResponse(
    Guid Id,
    string Name,
    string Description,
    IReadOnlyCollection<string> Permissions
);