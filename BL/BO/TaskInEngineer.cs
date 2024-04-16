using System;

namespace BO
{
    /// <summary>
    /// Represents a task assigned to an engineer, including an identifier and alias.
    /// </summary>
    public class TaskInEngineer
    {
        /// <summary>
        /// Gets the unique identifier of the task assigned to the engineer.
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// Gets or sets the alias or nickname of the task.
        /// </summary>
        public string? Alias { get; set; }
    }
}
