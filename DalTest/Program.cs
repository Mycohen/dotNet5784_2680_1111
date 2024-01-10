namespace DalTest;
using DalApi;
using Dal;
using System;
using DO;
using System.Transactions;

internal static class Program
{
    public static IDependency? s_dalDependency = new DependencyImplementation();
    public static IEngineer? s_dalEngineer = new EngineerImplementation();
    public static ITask? s_dalTask = new TaskImplementation();
    

    public static void Main()
    {
        try
        {
            Initialization.Do(s_dalDependency, s_dalEngineer, s_dalTask);
        }
        catch (global::System.Exception errorMasseege)
        {
            Console.WriteLine(errorMasseege);
        }
    }

    private static void ptintMainManu()
    {
        Console.WriteLine(@"Enter 0 for quiting the program:
                            Enter 1 for Tasks menu:
                            Enter 2 for Engineers nenu:
                            Enter 3 for Dependencies menu:");
    }

    private static void printSubMenu(string entity)
    {
        Console.WriteLine("Enter 0 for returninig to the main menu:\n");
        Console.WriteLine($"Enter 1 to Create a {entity}:");
        Console.WriteLine($"Enter 2 to Update the {entity}:");
        Console.WriteLine($"Enter 3 to Read a {entity} by entering its ID:");
        Console.WriteLine($"Enter 4 to Read all of the {entity} elements:");
        Console.WriteLine($"Enter 5 to Remove all of the {entity} elements:");
    }
    private static void createTask()
    {
        
        Console.WriteLine("Enter the task alias: ");
        string? _Alias= Console.ReadLine();
        Console.WriteLine("Enter the task description");
        string? _Description= Console.ReadLine();
        DateTime _CreatedAtDate = DateTime.Now;
        Console.WriteLine("Enter the required effort time for the task, enter in the format [d.]hh:mm:ss[.fffffff]");
        string? userInput = Console.ReadLine();
        TimeSpan _requiredEffortTimeI=TimeSpan.Parse(userInput);
        Console.WriteLine("Does the task have a milestone ? (Y/N):");
         userInput = Console.ReadLine()?.Trim().ToUpper(); // Read input and convert to uppercase
        bool _IsMilestone = userInput == "Y";
        Console.WriteLine("Enter the complexity of the task? (0-5)");
        userInput = Console.ReadLine();
        int complexity=getInt(userInput);
        EngineerExperience ?_complexity = (EngineerExperience)complexity;
        Console.WriteLine("Enter the planned start date for the task (e.g., 2024-01-10): ");
        userInput = Console.ReadLine();
        DateTime? _startDate= DateTime.Parse(userInput);
        Console.WriteLine("Enter the schedule date for the task (e.g., 2024-01-10):");
        userInput= Console.ReadLine();
        DateTime?_scheduleDate= DateTime.Parse(userInput);
        Console.WriteLine("Enter the task dead line date (e.g., 2024-01-10):");
        userInput= Console.ReadLine();
        DateTime _deadLineDate= DateTime.Parse(userInput);
        Console.WriteLine("If the task was completed, enter the completed date (e.g., 2024-01-10):");
        DateTime _completeDatte= DateTime.Parse(userInput);
        Console.WriteLine("Enter the deliverables associated with the task: ");
        string? _deliverables= Console.ReadLine();
        Console.WriteLine("Enter the remarks associated with the task: ");
        string? _remarks = Console.ReadLine();
        Console.WriteLine("Enter the Engineer ID for the task");
        userInput=Console.ReadLine();
        int _engineerid=getInt(userInput);
        Task inputTak = new Task(0, _Alias, _Description, _CreatedAtDate, _requiredEffortTimeI,
            _IsMilestone, (EngineerExperience)complexity, _startDate, _scheduleDate, _deadLineDate,
            _completeDatte, _deliverables, _remarks, _engineerid);
    }
    private static void readTask()
    {

        Console.WriteLine("Enter the task ID which you want to print:");
        string? userInput=Console.ReadLine();
       int id =getInt(userInput);
        ITask t = new TaskImplementation();
        Task i= t.Read(id)!;
        Console.WriteLine($"ID={i.Id} ");
        Console.WriteLine($"Alias={i.Alias} ");
        Console.WriteLine($"Description={i.Description} ");
        Console.WriteLine($"Created At Date={i.CreatedAtDate} ");
        







    }

    private static bool yesOrNo()
    {
       string? message = Console.ReadLine()?.Trim().ToUpper(); // Read input and convert to uppercase
        bool _answer =message == "Y";
        return _answer;
    }
    private static int getInt(string userInput)
    {

        return int.Parse(userInput);
    }
    private static void checkTimeSpanFormat(string userInput)
    {
        if (!(TimeSpan.TryParse(userInput, out TimeSpan _RequiredEffortTime)))
            throw new Exception("ERROR: the duration is incorrect");
    }
    private static void checkDateTimeFormat(string ?userInput)
    {
        if (!(DateTime.TryParse(userInput, out DateTime plannedStartDate)))
            throw new Exception("");
    }


}