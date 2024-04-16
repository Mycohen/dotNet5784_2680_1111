using System;

namespace BO
{
    /// <summary>
    /// Represents a milestone in a list, including an identifier, description, alias, status, and completion percentage.
    /// </summary>
    public class MilestoneInList
    {
        /// <summary>
        /// Gets the unique identifier of the milestone.
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// Gets or sets the description of the milestone.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the alias or name of the milestone.
        /// </summary>
        public string? Alias { get; set; }

        /// <summary>
        /// Gets or sets the current status of the milestone.
        /// </summary>
        public Enums.Status Status { get; set; }

        /// <summary>
        /// Gets or sets the completion percentage of the milestone.
        /// </summary>
        public double CompletionPercentage { get; set; }
    }
}
