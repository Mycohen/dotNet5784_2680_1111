namespace DO;

public record Dependency(
    int Id,
    int? DependentTask =null,
    int? DependsOnTask =null

    )
{
    public Dependency(): this(0) { }
}

