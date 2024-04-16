using System;
using System.Collections.Generic;

namespace BO
{
    /// <summary>
    /// Represents a milestone, including its identifier, description, alias, status, and associated dependencies.
    /// </summary>
    public class Milestone
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
        /// Gets the date and time when the milestone was created.
        /// </summary>
        public DateTime? CreatedAtDate { get; init; }

        /// <summary>
        /// Gets or sets the current status of the milestone.
        /// </summary>
        public Enums.Status Status { get; set; }

        /// <summary>
        /// Gets or sets the forecast date of the milestone.
        /// </summary>
        public DateTime? ForecastDate { get; set; }

        /// <summary>
        /// Gets or sets the deadline date of the milestone.
        /// </summary>
        public DateTime? DeadlineDate { get; set; }

        /// <summary>
        /// Gets or sets the completion date of the milestone, if completed.
        /// </summary>
        public DateTime? CompleteDate { get; set; }

        /// <summary>
        /// Gets or sets any remarks or comments related to the milestone.
        /// </summary>
        public string? Remarks { get; set; }

        /// <summary>
        /// Gets or sets the list of tasks that this milestone depends on.
        /// </summary>
        public List<TaskInList>? Dependencies { get; set; }
    }
}
