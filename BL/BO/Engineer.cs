using DO;
using System.Text;

namespace BO
{
    /// <summary>
    /// Represents an engineer, including properties such as ID, name, email, cost, level, and assigned task.
    /// </summary>
    public class Engineer
    {
        /// <summary>
        /// Gets or sets the unique identifier of the engineer.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the engineer.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the email of the engineer.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the cost associated with the engineer.
        /// </summary>
        public double? Cost { get; set; }

        /// <summary>
        /// Gets or sets the experience level of the engineer.
        /// </summary>
        public EngineerExperience? Level { get; set; }

        /// <summary>
        /// Gets or sets the task assigned to the engineer.
        /// </summary>
        public BO.TaskInEngineer? Task { get; set; }

        /// <summary>
        /// Returns a string representation of the engineer, including ID, name, level, email, cost, and task details if available.
        /// </summary>
        /// <returns>A string representation of the engineer.</returns>
        public override string ToString()
        {
            // Create a StringBuilder to build the engineer details string
            StringBuilder engineerDetails = new StringBuilder();

            // Add engineer information to the string
            engineerDetails.AppendLine($"Engineer ID: {Id}");
            engineerDetails.AppendLine($"Engineer Name: {Name}");
            engineerDetails.AppendLine($"Engineer Level: {Level}");
            engineerDetails.AppendLine($"Engineer Email: {Email}");
            engineerDetails.AppendLine($"Engineer Cost: {Cost}");

            // Include Task details if it exists
            if (Task != null)
            {
                engineerDetails.AppendLine($"Task ID: {Task.Id}");
                engineerDetails.AppendLine($"Task Alias: {Task.Alias}");
            }

            return engineerDetails.ToString();
        }
    }
}
