using System;

namespace BO
{
    /// <summary>
    /// Represents an engineer assigned to a task, including an identifier and name.
    /// </summary>
    public class EngineerInTask
    {
        /// <summary>
        /// Gets the unique identifier of the engineer.
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// Gets or sets the name of the engineer.
        /// </summary>
        public string? Name { get; set; }
    }
}
