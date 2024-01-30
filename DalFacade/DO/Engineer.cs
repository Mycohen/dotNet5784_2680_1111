namespace DO;

/// <summary>
/// Represents an Engineer entity with relevant characteristics.
/// </summary>
public record Engineer(
    /// <summary>
    /// Gets or sets the unique identifier for the Engineer.
    /// </summary>
    int Id,

    /// <summary>
    /// Gets or sets the email address of the Engineer.
    /// </summary>
    string? Email = null,

    /// <summary>
    /// Gets or sets the cost associated with the Engineer.
    /// </summary>
    double? Cost = null,

    /// <summary>
    /// Gets or sets the name of the Engineer.
    /// </summary>
    string? Name = null,

    /// <summary>
    /// Gets or sets the experience level of the Engineer.
    /// Default is set to Beginner.
    /// </summary>
    DO.EngineerExperience? Level = EngineerExperience.Beginner
)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Engineer"/> class with default values.
    /// </summary>
    public Engineer() : this(0) { }
}
