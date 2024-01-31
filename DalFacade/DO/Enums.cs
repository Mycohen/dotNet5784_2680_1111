namespace DO
{
    /// <summary>
    /// Represents a namespace for data objects related to the application.
    /// </summary>
    public enum EngineerExperience
    {
        /// <summary>
        /// Represents a beginner level of experience for an engineer.
        /// </summary>
        Beginner = 0,

        /// <summary>
        /// Represents an advanced beginner level of experience for an engineer.
        /// </summary>
        AdvancedBeginner,

        /// <summary>
        /// Represents an intermediate level of experience for an engineer.
        /// </summary>
        Intermediate,

        /// <summary>
        /// Represents an advanced level of experience for an engineer.
        /// </summary>
        Advanced,

        /// <summary>
        /// Represents an expert level of experience for an engineer.
        /// </summary>
        Expert
    }

    /// <summary>
    /// Represents the main menu options for the application.
    /// </summary>
    public enum MainMenuOption
    {
        MainExit,      // Option to exit the main menu
        TaskMenu,      // Option to navigate to the task menu
        EngineerMenu,  // Option to navigate to the engineer menu
        DependencyMenu // Option to navigate to the dependency menu
    };

    /// <summary>
    /// Represents the CRUD (Create, Read, Update, Delete) menu options for data entities.
    /// </summary>
    public enum CrudMenuOption
    {
        SubExit,       // Option to exit the CRUD sub-menu
        CreateOp,      // Option to create a new entity
        UpdateOp,      // Option to update an existing entity
        PrintSingleOp, // Option to print details of a single entity
        PrintAllOp,    // Option to print details of all entities
        DeleteOp,      // Option to delete a single entity
        DeleteAllOp    // Option to delete all entities
    };
}
