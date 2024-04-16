namespace BO
{
    /// <summary>
    /// This namespace contains various enums and shared resources for the business objects (BO).
    /// </summary>
    public class Enums
    {
        // The project start date, initialized to DateTime.MinValue (represents the minimum possible date value).
        public static DateTime projectStartDate = DateTime.MinValue;

        /// <summary>
        /// Enumeration to represent the status of a task or project.
        /// </summary>
        public enum Status
        {
            Unscheduled = 1, // Task has not yet been scheduled.
            Scheduled,       // Task has been scheduled but not yet started.
            OnTrack,         // Task is currently in progress and on track.
            /* InJeopardy, */ // Task is in jeopardy of not meeting its objectives (currently commented out).
            Done             // Task is completed.
        }

        /// <summary>
        /// Enumeration representing the main menu options for the application.
        /// </summary>
        public enum Phase1Menu
        {
            MainExit,      // Exit the main menu.
            AddMultiple,   // Add multiple tasks.
            TaskMenu,      // Navigate to the task menu.
            //InitData       // Initialize the data.
        }
        public enum Phase2Menu
        {
            MainExit,        // Exit the main menu.
            
        }
        /// <summary>
        /// Enumeration representing the main engineer menu options.
        /// </summary>
        public enum Phase3Menu
        {
            MainExit,        // Exit the main engineer menu.
            CRUDEngineer,    // Navigate to the CRUD menu for engineers.
            AddMultipleEngineers,     // Add multiple engineers.
            TaskMenuEngineer, // Navigate to the task menu for engineers.
            InitData  // Initialize the data.
        }

        /// <summary>
        /// Enumeration representing the CRUD menu options for data entities.
        /// </summary>
        public enum CrudMenuOption
        {
            SubExit,        // Exit the CRUD sub-menu.
            CreateOp,       // Create a new data entity.
            UpdateOp,       // Update an existing data entity.
            PrintSingleOp,  // Print details of a single data entity.
            PrintAllOp,     // Print details of all data entities.
            DeleteOp,       // Delete a single data entity.
            DeleteAllOp     // Delete all data entities.
        }
        public enum CrudMenuOptionTaskPhase3
        {
            SubExit,        // Exit the CRUD sub-menu.
            UpdateOp,       // Update an existing data entity.
            PrintSingleOp,  // Print details of a single data entity.
            PrintAllOp,     // Print details of all data entities.
            DeleteOp,       // Delete a single data entity.
            DeleteAllOp     // Delete all data entities.
        }

        /// <summary>
        /// Enumeration representing the different phases of a project.
        /// </summary>
        public enum projectPhase
        {
            TaskCreationPhase = 1, // The phase in which the director creates tasks.
            SchedualingPhase,      // The phase in which the director schedules the tasks.
            ExecutionPhase         // The phase in which tasks are assigned to engineers.
        }
    }
}
