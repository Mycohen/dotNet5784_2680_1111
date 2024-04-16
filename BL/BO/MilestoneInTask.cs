using System;

namespace BO
{
    /// <summary>
    /// Represents a milestone within a task, including an identifier and alias.
    /// </summary>
    public class MilestoneInTask
    {
        /// <summary>
        /// Gets the unique identifier of the milestone.
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// Gets or sets the alias or name of the milestone.
        /// </summary>
        public string? Alias { get; set; }
    }
}
