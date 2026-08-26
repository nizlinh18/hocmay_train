using System.ComponentModel.DataAnnotations;

namespace Week1.Rbac.Api.Contracts;

public sealed record CreatePermissionRequest(
    [property: Required, MaxLength(150)]
    string Code,

    [property: MaxLength(500)]
    string Description
);

public sealed record UpdatePermissionRequest(
    [property: Required, MaxLength(150)]
    string Code,

    [property: MaxLength(500)]
    string Description
);

public sealed record PermissionResponse(
    Guid Id,
    string Code,
    string Description
);