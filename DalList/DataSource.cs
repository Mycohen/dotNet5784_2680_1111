namespace Dal
{
    // Static class representing the data source for the application
    internal static class DataSource
    {
        // Nested static class containing configuration constants for the data source
        internal static class Config
        {//welcome Shmuel kaplan
            // Constant representing the starting value for dependency IDs
            internal const int StartDepID = 0;

            // Private static variable to track the next available dependency ID
            private static int _nextDepID = StartDepID;

            // Property to get the next dependency ID and increment the internal counter
            internal static int NextDepID { get => _nextDepID++; }

            // Constant representing the starting value for task IDs
            internal const int StartTaskID = 0;

            // Private static variable to track the next available task ID
            private static int _nextTaskID = StartTaskID;

            // Property to get the next task ID and increment the internal counter
            internal static int NextTaskID { get => _nextTaskID++; }
        }

        // Static list of Dependency objects representing dependencies in the application
        internal static List<DO.Dependency> Dependencies { get; } = new();

        // Static list of Engineer objects representing engineers in the application
        internal static List<DO.Engineer> Engineers { get; } = new();

        // Static list of Task objects representing tasks in the application
        internal static List<DO.Task> Tasks { get; } = new();
    }
}
