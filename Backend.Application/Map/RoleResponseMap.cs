using Backend.Application.Queries.Response;

namespace Backend.Application.Map;

public static class RoleResponseMap
{
    public static RoleResponse Map(this Role role)
    {
        return new RoleResponse
        {
            Id = role.Id,
            Name = role.Name,
        };
    }
}
