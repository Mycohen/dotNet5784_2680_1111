using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// Represents a task in a list, with properties such as ID, description, alias, and status.
    /// </summary>
    public class TaskInList
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
        /// Gets or sets the current status of the task.
        /// </summary>
        public Enums.Status Status { get; set; }
    }
}
