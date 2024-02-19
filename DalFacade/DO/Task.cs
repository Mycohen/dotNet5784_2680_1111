namespace DO
{
    /// <summary>
    /// Represents a namespace for data objects related to the application.
    /// </summary>
    public record Task(
        /// <summary>
        /// Gets or sets the unique identifier for the task.
        /// </summary>
        int Id,

        /// <summary>
        /// Gets or sets an alias or alternate name for the task (optional).
        /// </summary>
        string? Alias = null,

        /// <summary>
        /// Gets or sets a description of the task (optional).
        /// </summary>
        string? Description = null,

        /// <summary>
        /// Gets or sets the date and time when the task was created (optional).
        /// </summary>
        DateTime? CreatedAtDate = null,

        /// <summary>
        /// Gets or sets the estimated effort required for the task (optional).
        /// </summary>
        TimeSpan? RequiredEffortTime = null,

        /// <summary>
        /// Gets or sets whether the task is a milestone or not.
        /// </summary>
        bool IsMilestone = false,

        /// <summary>
        /// Gets or sets the complexity level of the task.
        /// Default is set to Intermediate.
        /// </summary>
        DO.EngineerExperience Complexity = EngineerExperience.Intermediate,

        /// <summary>
        /// Gets or sets the planned start date for the task (optional).
        /// </summary>
        DateTime? StartDate = null,

        /// <summary>
        /// Gets or sets the 
        /// (optional).
        /// </summary>
        DateTime? ScheduledDate = null,

        /// <summary>
        /// Gets or sets the deadline date for completing the task (optional).
        /// </summary>
        DateTime? DeadlineDate = null,

        /// <summary>
        /// Gets or sets the date when the task was completed.
        /// </summary>
        DateTime? CompleteDate = null,

        /// <summary>
        /// Gets or sets deliverables associated with the task (optional).
        /// </summary>
        string? Deliverables = null,

        /// <summary>
        /// Gets or sets additional remarks or notes about the task (optional).
        /// </summary>
        string? Remarks = null,

        /// <summary>
        /// Gets or sets the unique identifier of the engineer assigned to the task.
        /// Default is set to 0.
        /// </summary>
        int EngineerId = 0
    )
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Task"/> class with default values.
        /// </summary>
        public Task() : this(0) { }
    }
}
