using DO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    /// <summary>
    /// Represents a task with various properties such as description, alias, status, and dependencies.
    /// </summary>
    public class Task
    {
        /// <summary>
        /// Gets the unique identifier of the task.
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// Gets or sets the description of the task.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the alias or nickname of the task.
        /// </summary>
        public string? Alias { get; set; }

        /// <summary>
        /// Gets the date and time when the task was created.
        /// </summary>
        public DateTime? CreatedAtDate { get; init; }

        /// <summary>
        /// Gets or sets the current status of the task.
        /// </summary>
        public Enums.Status Status { get; set; }

        /// <summary>
        /// Gets or sets the list of tasks that this task depends on.
        /// </summary>
        public List<TaskInList>? Dependencies { get; set; }

        /// <summary>
        /// Gets or sets the milestone associated with the task, if any.
        /// </summary>
        public MilestoneInTask? Milestone { get; set; }

        /// <summary>
        /// Gets or sets the required effort time for the task.
        /// </summary>
        public TimeSpan? RequiredEffortTime { get; set; }

        /// <summary>
        /// Gets or sets the start date of the task.
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the scheduled date of the task.
        /// </summary>
        public DateTime? ScheduledDate { get; set; }

        /// <summary>
        /// Gets or sets the forecast date of the task.
        /// </summary>
        public DateTime? ForecastDate { get; set; }

        /// <summary>
        /// Gets or sets the deadline date of the task.
        /// </summary>
        public DateTime? DeadlineDate { get; set; }

        /// <summary>
        /// Gets or sets the completion date of the task, if completed.
        /// </summary>
        public DateTime? CompleteDate { get; set; }

        /// <summary>
        /// Gets or sets the deliverables associated with the task.
        /// </summary>
        public string? Deliverables { get; set; }

        /// <summary>
        /// Gets or sets any remarks or comments related to the task.
        /// </summary>
        public string? Remarks { get; set; }

        /// <summary>
        /// Gets or sets the engineer assigned to the task.
        /// </summary>
        public EngineerInTask? Engineer { get; set; }

        /// <summary>
        /// Gets or sets the level of complexity of the task.
        /// </summary>
        public EngineerExperience Complexity { get; set; }

        /// <summary>
        /// Returns a string representation of the task, including its properties such as ID, description, and other details.
        /// </summary>
        /// <returns>A formatted string representing the task details.</returns>
        public override string ToString()
        {
            // Create a StringBuilder to build the task details string
            StringBuilder taskDetails = new StringBuilder();

            // Add task information to the string
            taskDetails.AppendLine($"Task ID: {Id}");
            taskDetails.AppendLine($"Description: {Description}");
            taskDetails.AppendLine($"Alias: {Alias}");
            taskDetails.AppendLine($"Created At: {CreatedAtDate}");
            taskDetails.AppendLine($"Status: {Status}");
            taskDetails.AppendLine($"Required Effort Time: {RequiredEffortTime}");
            taskDetails.AppendLine($"Start Date: {StartDate}");
            taskDetails.AppendLine($"Scheduled Date: {ScheduledDate}");
            taskDetails.AppendLine($"Forecast Date: {ForecastDate}");
            taskDetails.AppendLine($"Deadline Date: {DeadlineDate}");
            taskDetails.AppendLine($"Complete Date: {CompleteDate}");
            taskDetails.AppendLine($"Deliverables: {Deliverables}");
            taskDetails.AppendLine($"Remarks: {Remarks}");
            taskDetails.AppendLine($"Complexity: {Complexity}");

            // Add engineer information if available
            if (Engineer != null)
            {
                taskDetails.AppendLine($"Engineer ID: {Engineer.Id}");
                taskDetails.AppendLine($"Engineer Name: {Engineer.Name}");
            }

            // Add milestone information if available
            if (Milestone != null)
            {
                taskDetails.AppendLine($"Milestone ID: {Milestone.Id}");
                taskDetails.AppendLine($"Milestone Alias: {Milestone.Alias}");
            }

            // Add dependencies information if available
            if (Dependencies != null && Dependencies.Count > 0)
            {
                taskDetails.AppendLine("Dependencies:");
                foreach (var dependency in Dependencies)
                {
                    taskDetails.AppendLine($"- Task ID: {dependency.Id}");
                    taskDetails.AppendLine($"  Description: {dependency.Description}");
                    taskDetails.AppendLine($"  Alias: {dependency.Alias}");
                    taskDetails.AppendLine($"  Status: {dependency.Status}");
                }
            }

            return taskDetails.ToString();
        }
    }
}
