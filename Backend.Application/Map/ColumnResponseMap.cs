namespace Backend.Application.Map;

public static class ColumnResponseMap
{
    public static ColumnResponse Map(this Column column)
    {
        return new ColumnResponse
        {
            Id = column.Id,
            Name = column.Name,
            Position = column.Position,
            Issues = column.Issues.Select(i => i.Map()).OrderBy(i => i.NumberInProject)
        };
    }
}
