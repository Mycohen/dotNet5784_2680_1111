namespace DalTest;
using DalApi;
using Dal;
using System.Runtime.InteropServices;
using System.Net.Mail;
using DO;
using System.Reflection.Metadata;
using System.ComponentModel.DataAnnotations;
using System.Transactions;
using System.Reflection.Emit;
using System.Xml.Linq;

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

               //.... I'm  too tierd to write the main... :<
            }
            catch (global::System.Exception errorMasseege)
            {
                Console.WriteLine(errorMasseege);
            }
        }

    private static void ptintMainManu()
    {
        Console.WriteLine(@"Enter 0 for quiting the program.
                            Enter 1 for Tasks menu.
                            Enter 2 for Engineers nenu
                            Enter 3 for Dependencies menu");
    }

    private static void printSubMenu(string entity)
    {
        Console.WriteLine("Enter 0 for returning to the main menu.");
        Console.WriteLine($"Enter 1 to Create a list of  {entity}s");
        Console.WriteLine($"Enter 2 to Update the {entity}");
        Console.WriteLine($"Enter 3 to Print a {entity} by entering its ID");
        Console.WriteLine($"Enter 4 to Read all of the {entity} elements");
        Console.WriteLine($"Enter 5 to Remove all of the {entity} elements");
    }
    private static void createEngineer()
    {
        Console.WriteLine("Enter Engineer's ID: the range for the the id is 2*10^8 to 4*10^8");
        int id = isValidIntInput(); 
        if (id < 2e8 || id > 4e8)
        { throw new Exception("Invalid ID number. please enter in the range"); }

        string email = emailCheck_update_createEngineer();

        Console.WriteLine("Enter the cost for Engineer\n");
        double cost = double.TryParse(Console.ReadLine(), out cost) ? cost : throw new Exception("Error casting the input to cast ");

        Console.WriteLine("Enter Engineer's name\n");
        string name = Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not a null)");

        EngineerExperience level = (EngineerExperience)isValidIntInput();

        Engineer engineerInstence = new Engineer(Id: id, Email: email, Cost: cost, Name: name, Level: level);
        s_dalEngineer!.Create(engineerInstence);
    }
    private static void updateEngineer()
    {
        Console.WriteLine("what Engineer do you want to update: (enter an ID)\n");
        int id = isValidIntInput();
        Engineer correntEngineerData = s_dalEngineer!.Read(id) ?? throw new Exception("Engineer with such ID does not exist");

        updateEngineer_PrintText("Email");
        string email = (yesOrNo()) ? emailCheck_update_createEngineer() : correntEngineerData!.Email!;

        updateEngineer_PrintText("Cost");
        double cost = yesOrNo() ?  
            (Double.TryParse(Console.ReadLine(), out cost) ? cost : throw new Exception("Error casting the input to cast ")) :
            (double)correntEngineerData!.Cost!;

        updateEngineer_PrintText("Name");
        string name = (yesOrNo()) ? (Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not a null)")) :
            correntEngineerData!.Name!;

        updateEngineer_PrintText("Level");
       EngineerExperience level = (yesOrNo()) ? (EngineerExperience)isValidIntInput() : correntEngineerData!.Level!;

        Engineer updatedEngineerData = new Engineer(Id:  id, Email: email, Name: name, Cost: cost, Level: level);
        s_dalEngineer!.Update(updatedEngineerData);
    }


    private static void updateEngineer_PrintText(string type)
    {
        Console.WriteLine($"Do you wish to update the {type} property? enter yes/no \n");
    }
    private static string emailCheck_update_createEngineer()
    {
        string email = Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not a null)");
        if (!IsValidEmail(email))
        {
            throw new Exception("Email addres is not valid");
        }
        return email;
    }
    private static bool IsValidEmail(string email)
    {
        try
        {
            // Attempt to create a MailAddress instance
            MailAddress mailAddress = new MailAddress(email);
            return true;
        }
        catch (FormatException)
        {
            // If FormatException is thrown, the email address is not valid
            return false;
        }
    }

    private static void readEngineer()
    {
        Console.WriteLine("what Engineer do you want to Print: (enter an ID)\n");
        int id = isValidIntInput();
        Engineer correntEngineerData = s_dalEngineer!.Read(id) ?? throw new Exception("Engineer with such ID does not exist");
        printEngineer(correntEngineerData);
    }

    private static void readAllEngineers()
    {
        List<Engineer> engineers = s_dalEngineer!.ReadAll();
       
        foreach (Engineer engineer in engineers)
        {
            printEngineer(engineer!);
        }
    }
   
    private static void removeEngineer()
    {
        Console.WriteLine("Enter the ID of Engineer you want to remove\n ");
        int id = isValidIntInput();
        Engineer correntEngineerData = s_dalEngineer!.Read(id) ?? throw new Exception("Engineer with such ID does not exist");

        s_dalEngineer.Delete(id);
    }


    //Dependency 
    private static void createDependency() 
    {
        Console.WriteLine("Enter depented task ID");
        int dependentTask = isValidIntInput();
        Console.WriteLine("Provide the task necessary for the current task");
        int dependsOnTask = isValidIntInput();
        Dependency dependencyInstance = new Dependency(dependentTask, dependsOnTask);
        s_dalDependency!.Create(dependencyInstance);
    }
    
    private static void updateDependency() 
    {
        
        Console.WriteLine("Enter the ID of the task you want to update");
        int id = isValidIntInput();

        Dependency currentDependencyData = s_dalDependency!.Read(id) ?? throw new Exception("Dependency with such ID does not exist");

        Console.WriteLine("Do you want to update the current task? (y/n)");
        int dependentTask = (yesOrNo()) ?  isValidIntInput() : (int)currentDependencyData!.DependentTask!;

        Console.WriteLine("Do you want to update the dependency task? (y/n)");
        int dependentOnTask = (yesOrNo()) ? isValidIntInput() : (int)currentDependencyData!.DependsOnTask!;

        Dependency updatedDapendancyData = new Dependency(dependentTask, dependentOnTask);

    }
    private static void readDependency() 
    {
        Console.WriteLine("Enter the ID of dependency you want to read");
        int id = isValidIntInput();
        Dependency currentEngineerData = s_dalDependency!.Read(id) ?? throw new Exception("Dependency with such ID does not exist");
        printDependency(currentEngineerData);

    }

    private static void readAllDependency()
    {
        List<Dependency> dependencies = s_dalDependency!.ReadAll();
        foreach (Dependency dependency in dependencies)
        {
            printDependency(dependency);
        }
    }

    private static void removeDependency()
    {
        Console.WriteLine("Enter the ID of the Dependency you wish to delete");
        int id = isValidIntInput();
        
        s_dalDependency!.Delete(id);
    }
    private static void printDependency(Dependency dependency)
    {
        Console.WriteLine($"The ID of the dependency is : {dependency.Id}");
        Console.WriteLine($"The dependent task is : {dependency.DependentTask}");
        Console.WriteLine($"The task that is nessery for current task {dependency.DependsOnTask}");
    }


    // help functions
   
    

    private static int isValidIntInput()
    {
        //check if the input can be transformed from string to input
        bool isValid = int.TryParse(Console.ReadLine(), out int value);
        if (isValid)
        {
            return value;
        }
        else { throw new Exception("Error: the input can not be converted to int type"); }
    }

   

    private static void printEngineer(Engineer engineer)
    {
        Console.WriteLine("Engineer ID: " + engineer.Id);
        Console.WriteLine("Engineer Name: " + engineer.Name);
        Console.WriteLine("Engineer Level: " + engineer.Level);
        Console.WriteLine("Engineer Email: " + engineer.Email);
        Console.WriteLine("Engineer Cost: " + engineer.Cost);
    }
    private static void createTask()
    {

        Console.WriteLine("Enter the task alias: ");
        string? _Alias = Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not a null)");
        Console.WriteLine("Enter the task description");
        string? _Description = Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not a null)");
        DateTime _CreatedAtDate = DateTime.Now;
        Console.WriteLine("Enter the required effort time for the task, enter in the format [d.]hh:mm:ss[.fffffff]");
        string? userInput = Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not a null)");
        TimeSpan _requiredEffortTimeI = checkTimeSpanFormat(userInput);
        Console.WriteLine("Does the task have a milestone ? (Y/N):");
        userInput = Console.ReadLine()?.Trim().ToUpper(); // Read input and convert to uppercase
        bool _IsMilestone = userInput == "Y";
        Console.WriteLine("Enter the complexity of the task? (0-5)");
        userInput = Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not a null)");
        int complexity = getInt(userInput);
        EngineerExperience? _complexity = (EngineerExperience)complexity;
        Console.WriteLine("Enter the planned start date for the task (e.g., 2024-01-10): ");
        userInput = Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not a null)");
        DateTime? _startDate = CheckDateTimeFormat(userInput);
        Console.WriteLine("Enter the schedule date for the task (e.g., 2024-01-10):");
        userInput = Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not a null)");
        DateTime? _scheduleDate =CheckDateTimeFormat(userInput);
        Console.WriteLine("Enter the task dead line date (e.g., 2024-01-10):");
        userInput = Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not a null)");
        DateTime _deadLineDate = CheckDateTimeFormat(userInput);
        Console.WriteLine("If the task was completed, enter the completed date (e.g., 2024-01-10):");
        userInput = Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not a null)");
        DateTime _completeDatte = CheckDateTimeFormat(userInput);
        Console.WriteLine("Enter the deliverables associated with the task: ");
        string? _deliverables = Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not a null)");
        Console.WriteLine("Enter the remarks associated with the task: ");
        string? _remarks = Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not a null)");
        Console.WriteLine("Enter the Engineer ID for the task");
        userInput = Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not a null)");
        int _engineerid = getInt(userInput);
        Task inputTak = new Task(0, _Alias, _Description, _CreatedAtDate, _requiredEffortTimeI,
            _IsMilestone, (EngineerExperience)complexity, _startDate, _scheduleDate, _deadLineDate,
            _completeDatte, _deliverables, _remarks, _engineerid);
    }
    private static void updateTask()
    {
        Console.WriteLine("what Task do you want to update: (enter an ID)\n");
        int id = isValidIntInput();
        Task taskT = s_dalTask!.Read(id) ?? throw new Exception("Engineer with such ID does not exist");

        updateEngineer_PrintText("Alias");
        string alias = (yesOrNo()) ? (Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not a null)")) :
             taskT!.Alias!;

        updateEngineer_PrintText("Description");
        string description = (yesOrNo()) ? (Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not a null)")) :
             taskT!.Description!;
        DateTime? createdAtDate =taskT!.CreatedAtDate;

        updateEngineer_PrintText("Required effort time");
        TimeSpan? requiredEffortTime;
        bool ans = yesOrNo();
        if (ans)
        {
            string? requiredEffo = Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not a null)");
            requiredEffortTime = checkTimeSpanFormat(requiredEffo);
        }
        else
        {
            requiredEffortTime = taskT.RequiredEffortTime;
        }
        updateEngineer_PrintText("Is Milestone");
        bool isMilstone = yesOrNo();
        updateEngineer_PrintText("Complexity");
        EngineerExperience? complexity;
        ans = yesOrNo();
        if (ans)
        {
            int comp = getInt(Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not a null)"));
            complexity = (EngineerExperience)comp;
        }
        else
        {
            complexity = taskT.Complexity;
        }

        updateEngineer_PrintText("Start Date");
        DateTime? startDate;
        ans = yesOrNo();
        if (ans)
        {
            string? startD = Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not a null)");
            startDate = CheckDateTimeFormat(startD);
        }
        else
        {
            startDate = taskT.StartDate;
        }
        updateEngineer_PrintText("Schedule Date");
        DateTime? scheduleDate;
        ans = yesOrNo();
        if (ans)
        {
            string? schedule = Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not a null)");
            scheduleDate = CheckDateTimeFormat(schedule);
        }
        else
        {
            scheduleDate = taskT.ScheduledDate;

        }

        updateEngineer_PrintText("Dead Line Date");
        DateTime? deadLineDate;
        ans = yesOrNo();
        if (ans)
        {
            string? dead = Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not a null)");
            deadLineDate = CheckDateTimeFormat(dead);
        }
        else
        {
            deadLineDate = taskT.DeadlineDate;

        }
        updateEngineer_PrintText("Complete Date");
        DateTime? completeDate;
        ans = yesOrNo();
        if (ans)
        {
            string? comp = Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not a null)");
            completeDate = CheckDateTimeFormat(comp);
        }
        else
        {
           completeDate = taskT.CompleteDate;

        }
        updateEngineer_PrintText("Deliverables");
        string deliverables = (yesOrNo()) ? (Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not a null)")) :
             taskT!.Deliverables!;
        updateEngineer_PrintText("Remarks");
        string remarks = (yesOrNo()) ? (Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not a null)")) :
        taskT!.Remarks!;
        int engineerId = taskT!.EngineerId;
        Task taskToUpdate = new Task(Id: id, Alias: alias, Description: description, CreatedAtDate: createdAtDate, RequiredEffortTime: requiredEffortTime,
            IsMilestone:isMilstone,Complexity:(EngineerExperience)complexity,StartDate:startDate, ScheduledDate:scheduleDate,
            DeadlineDate:deadLineDate, CompleteDate:completeDate, Deliverables:deliverables,
            Remarks:remarks,EngineerId:engineerId);

    }
    private static void ReadTask()
    {

        Console.WriteLine("Enter the task ID which you want to print:");
        string? userInput = Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not a null)");
        int id = getInt(userInput);
        Task correntTaskData = s_dalTask!.Read(id) ?? throw new Exception("Engineer with such ID does not exist");
    }
    private static void PrintTask(Task taskToPrint)
    {
        Console.WriteLine("Task ID:" + taskToPrint.Id);
        Console.WriteLine("Tsak Alias:" + taskToPrint.Alias);
        Console.WriteLine("Tsak Descrription:" + taskToPrint.Description);
        Console.WriteLine("Tsak Created at :" + taskToPrint.CreatedAtDate);
        Console.WriteLine("Required time for the task:" + taskToPrint.RequiredEffortTime);
        Console.WriteLine("Does the task have a milestone?:" + taskToPrint.IsMilestone);
        Console.WriteLine("Complexity's task:" + taskToPrint.Complexity);
        Console.WriteLine("The task stated at:" + taskToPrint.StartDate);
        Console.WriteLine("Task schedule date:" + taskToPrint.ScheduledDate);
        Console.WriteLine("Dead line  task:" + taskToPrint.DeadlineDate);
        Console.WriteLine("Task completed at:" + taskToPrint.CompleteDate);
        Console.WriteLine("Task deliverables:" + taskToPrint.Deliverables);
        Console.WriteLine("Task remarks:" + taskToPrint.Remarks);
        Console.WriteLine("Task Engineer ID:" + taskToPrint.EngineerId);

    }
    private static void readAllTask()
    {
        List<Task> tasks = s_dalTask!.ReadAll();

        foreach (Task task in tasks)
        {
            PrintTask(task!);
        }

    }
    private static void removeTask()
    {
        Console.WriteLine("Enter the ID of the Task you wish to delete");
        int id = isValidIntInput();

        s_dalTask!.Delete(id);
    }

    private static int getInt(string userInput)
    {

        return int.Parse(userInput);
    }
    private static TimeSpan checkTimeSpanFormat(string? userInput)
    {
        if ((TimeSpan.TryParse(userInput, out TimeSpan _RequiredEffortTime)))
        {
            return TimeSpan.Parse(userInput);
        }
        else
        {
            throw new Exception("ERROR: the duration is incorrect");
        }

    }
    private static DateTime CheckDateTimeFormat(string? userInput)
    {
        if (DateTime.TryParse(userInput, out DateTime parsedDateTime))
        {
            return parsedDateTime;
        }
        else
        {
            throw new Exception("Invalid DateTime format");
        }
    }

    private static bool yesOrNo()
    {
        // Read input and convert to uppercase
        string message = Console.ReadLine()?.Trim().ToUpper() ?? throw new Exception("can't enter a null");
        bool _answer = message!.StartsWith('Y');
        return _answer;
    }

}




