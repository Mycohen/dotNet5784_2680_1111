namespace BO;

public class Enums
{
    public static DateTime projectStartDate = DateTime.MinValue;
    public enum Status
    {
        Unscheduled = 1,
        Scheduled,
        OnTrack,
       /* InJeopardy,*/
        Done
    }
    /// <summary>
    /// Represents the main menu options for the application.
    /// </summary>
    public enum MainTaskMenu
    {
        MainExit,      // Option to exit the main menu
        TaskMenu,      // Option to navigate to the task menu
        CloseTaskMenu, // Option to close the task menu
        InitData      // Option to initialize the data
    };
    public enum MainEngineerMenu
    {
        MainExit,      // Option to exit the main menu
        EngineerMenu,      // Option to navigate to the task menu
        CloseEngineerMenu, // Option to close the task menu
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

