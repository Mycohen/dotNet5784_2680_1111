namespace DO
{
    /// <summary>
    /// Represents a Dependency entity with relevant characteristics.
    /// </summary>
    public record Dependency(
        int Id,
        int? DependentTask = null,
        int? DependsOnTask = null
    )
    {
        /// <summary>
        /// Default constructor for the Dependency class.
        /// Initializes a new instance with default values.
        /// </summary>
        public Dependency() : this(0) { }
    }
}
