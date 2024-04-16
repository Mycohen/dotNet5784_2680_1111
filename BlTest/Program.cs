// This namespace is for testing classes related to Data Access Layer (DAL)
namespace BlTest;
using BO;
// Importing necessary namespaces
using BlApi;
using System.Net.Mail;
using System.Runtime.InteropServices.Marshalling;
using System.Reflection.Emit;
using System.Threading.Channels;

// A static class that serves as the entry point for the program
internal static class Program
{

    static readonly BlApi.IBl s_bl = BlApi.Factory.Get(); //stage 4


    // Main method, the starting point of the program
    public static void Main()
    {
        BO.ProjectPhaseData phaseData = new BO.ProjectPhaseData();
        phaseData.Phase = BO.Enums.projectPhase.TaskCreationPhase;
        phaseData.StartedDate = DateTime.MinValue;
       
        welcomeMessage();
        do
        {
            BO.Tools.SerializeToXml(phaseData, "./xml/projectPhase.xml");
           
            try
            {
                if (phaseData.Phase == Enums.projectPhase.TaskCreationPhase)
                {
                    printSchedulingPhase1();
                    if (Enum.TryParse(Console.ReadLine(), out BO.Enums.Phase1Menu userChoice))
                    {
                        switch (userChoice)
                        {
                            case Enums.Phase1Menu.MainExit:
                                phaseData.Phase = BO.Enums.projectPhase.SchedualingPhase;
                                break;
                            case Enums.Phase1Menu.AddMultiple:
                                phase1TasksInputInARow();
                                break;
                            case Enums.Phase1Menu.TaskMenu:
                                taskOptions();
                                break;
                            default:
                                throw new BO.BlInvalidInputException($"invalid choice input {userChoice}");
                        }

                    }
                    else
                    {
                        throw new BO.BlInvalidInputException($"invalid choice input {userChoice}");
                    }
                }
                else if (phaseData.Phase == Enums.projectPhase.SchedualingPhase)
                {
                    if (Enum.TryParse(Console.ReadLine(), out BO.Enums.Phase2Menu userChoice))
                    {
                        switch (userChoice)
                        { 
                            case Enums.Phase2Menu.MainExit:
                                Console.WriteLine("Exiting the program at stage 2");
                                break;
                            case Enums.Phase2Menu.EnterDates:
                                loopOverTasksForUpdatingPhase2();
                                break;
                        }
                    }
                }
            }
            catch (BO.BlDoesNotExistExeption ex)
            {
                Console.WriteLine(ex);
            }
            catch (BO.BlAlreadyExistsException ex)
            {
                Console.WriteLine(ex);
            }
            catch (BO.BlDeletionImpossible ex)
            {
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        } while (true);  // Infinite loop for continuous user interaction

    }

    //Sub menu for task options
    private static void taskOptions()
    {
        // Display sub-menu for Task operations
        printSubMenuWithCreate("Task");

        // Parse user's choice from the sub-menu
        if (Enum.TryParse(Console.ReadLine(), out BO.Enums.CrudMenuOption crudMenuChoice))
        {
            // Process the user's choice
            switch (crudMenuChoice)
            {
                case BO.Enums.CrudMenuOption.SubExit:
                    // Exit to the main menu
                    return;
                case BO.Enums.CrudMenuOption.CreateOp:
                    // Handle user choice to create a new task
                    createTaskPhase1();
                    break;
                case BO.Enums.CrudMenuOption.UpdateOp:
                    // Handle user choice to update an existing task
                    updateTaskPhase1();
                    break;
                case BO.Enums.CrudMenuOption.PrintSingleOp:
                    // Handle user choice to print a single task
                    readTask();
                    break;
                case BO.Enums.CrudMenuOption.PrintAllOp:
                    // Handle user choice to print all tasks
                    readAllTask();
                    break;
                case BO.Enums.CrudMenuOption.DeleteOp:
                    // Handle user choice to remove a task
                    removeTask();
                    break;
                case BO.Enums.CrudMenuOption.DeleteAllOp:
                    // Handle user choice to remove all tasks
                    removeAllTasks();
                    break;
                default:
                    // Invalid choice, throw an exception
                    throw new Exception("ERROR: Invalid choice input. Please try again");
            }
        }
        else
        {
            // Invalid input format, throw an exception
            throw new Exception("ERROR: Invalid choice input. Please try again");
        }
    }

    //Sub menu for engineer options
    private static void engineerOptions()
    {
        // Display sub-menu for Engineer operations
        printSubMenuWithCreate("Engineer");

        // Parse user's choice from the sub-menu
        if (Enum.TryParse(Console.ReadLine(), out BO.Enums.CrudMenuOption crudMenuChoice))
        {
            // Process the user's choice
            switch (crudMenuChoice)
            {
                case BO.Enums.CrudMenuOption.SubExit:
                    // Exit to the main menu
                    return;
                case BO.Enums.CrudMenuOption.CreateOp:
                    // Handle user choice to create a new engineer
                    createEngineer();
                    break;
                case BO.Enums.CrudMenuOption.UpdateOp:
                    // Handle user choice to update an existing engineer
                    updateEngineer();
                    break;
                case BO.Enums.CrudMenuOption.PrintSingleOp:
                    // Handle user choice to print a single engineer
                    readEngineer();
                    break;
                case BO.Enums.CrudMenuOption.PrintAllOp:
                    // Handle user choice to print all engineers
                    readAllEngineers();
                    break;
                case BO.Enums.CrudMenuOption.DeleteOp:
                    // Handle user choice to remove an engineer
                    removeEngineer();
                    break;
                case BO.Enums.CrudMenuOption.DeleteAllOp:
                    // Handle user choice to remove all engineers
                    removeAllEngineers();
                    break;
                default:
                    // Invalid choice, throw an exception
                    throw new Exception("ERROR: Invalid choice input. Please try again");
            }
        }
        else
        {
            // Invalid input format, throw an exception
            throw new Exception("ERROR: Invalid choice input. Please try again");
        }
    }

    private static void printSchedulingPhase1()
    {
        // Introduction to Phase 1: Task Creation Phase
        Console.WriteLine("Phase 1: Task Creation Phase");

        // User's responsibilities in Phase 1
        Console.WriteLine("In this phase, you need to add all tasks required for the project.");

        // Present available options to the user
        Console.WriteLine("Choose an option:");
        Console.WriteLine("1 - Add multiple tasks to the project");
        Console.WriteLine("2 - Perform CRUD operations on tasks");
        Console.WriteLine("0 - Finish stage 1");
    }

    private static void printSchedulingPhase2()
    {
        // Introduction to Phase 2: Scheduling Phase
        Console.WriteLine("Phase 2: Scheduling Phase");

        // User's responsibilities in Phase 2
        Console.WriteLine("In this phase, you will set the project start date and schedule dates for all tasks.");

        // Present the option to quit the program
        Console.WriteLine("Choose an option:");
        Console.WriteLine("1 - Enter the Project Start Date and the start dates of all the tasks");
        Console.WriteLine("0 - Quit the program");
    }

    private static void printExecutionPhase3()
    {
        // Introduction to Phase 3: Execution Phase
        Console.WriteLine("Phase 3: Execution Phase");

        // User's responsibilities in Phase 3
        Console.WriteLine("In this phase, you cannot add new tasks.");
        Console.WriteLine("However, you can add engineers and update tasks.");

        // Present available options to the user
        Console.WriteLine("Choose an option:");
        Console.WriteLine("1 - Perform CRUD operations on engineers");
        Console.WriteLine("2 - Add multiple engineers to the project");
        Console.WriteLine("3 - Navigate to the tasks menu (to read or update tasks)");
        Console.WriteLine("4 - Initialize options (if necessary)");
        Console.WriteLine("0 - Quit the program");
    }

    private static void welcomeMessage()
    {
        // Introduction to the project planning
        Console.WriteLine("Welcome to the project planning.");
        Console.WriteLine("The planning process is divided into three phases:");

        // Description of Phase 1
        Console.WriteLine("Phase 1 - Task Creation:");
        Console.WriteLine("In this stage, you will add all tasks required to complete the project.");

        // Description of Phase 2
        Console.WriteLine("Phase 2 - Planning Phase:");
        Console.WriteLine("In this stage, you will set the project start date and then set start dates for each task.");

        // Description of Phase 3
        Console.WriteLine("Phase 3 - Execution Phase:");
        Console.WriteLine("In this stage, the project manager can assign tasks to engineers." +
            " Engineers can choose available tasks to begin working on.");
    }

    //printing the sub menu for the switch
    private static void printSubMenuWithCreate(string entity)
    {
        // Display the sub-menu options for a given entity (Task, Engineer, Dependency)
        Console.WriteLine("Enter 0 for returning to the previous menu.");
        Console.WriteLine($"Enter 1 to Create {entity}");
        Console.WriteLine($"Enter 2 to Update the {entity}");
        Console.WriteLine($"Enter 3 to Print a {entity} by entering its ID");
        Console.WriteLine($"Enter 4 to Read all of the {entity} elements");
        Console.WriteLine($"Enter 5 to Remove an instance of the {entity} element");
        Console.WriteLine($"Enter 6 to Remove all of the {entity} elements");
    }
    //for phase 3
    private static void printSubMenuWithoutCreate(string entity)
    {
        // Display the sub-menu options for a given entity (Task, Engineer, Dependency)
        Console.WriteLine("Enter 0 for returning to the previous menu.");
        Console.WriteLine($"Enter 2 to Update the {entity}");
        Console.WriteLine($"Enter 3 to Print a {entity} by entering its ID");
        Console.WriteLine($"Enter 4 to Read all of the {entity} elements");
        Console.WriteLine($"Enter 5 to Remove an instance of the {entity} element");
        Console.WriteLine($"Enter 6 to Remove all of the {entity} elements");
    }


    private static void removeAllFromXml()
    {
        Console.WriteLine("Would you like to delete all the data from the XML file?");
        if (yesOrNo())
        {

            s_bl.Engineers.DeleteAll();
            s_bl.Tasks.DeleteAll();
            Console.WriteLine("The data was deleted");

        }
        else
            Console.WriteLine("The data was not deleted");


    }
    private static void initOp()
    {
        Console.WriteLine("Would you like to create Initial data? (Y/N)"); //stage 3

        if (yesOrNo())
            //Initialization.Do(s_dal); //stage 2
            DalTest.Initialization.Do(); //stage 4
    }
    //Engineer methods
    // Method to create a new Engineer instance
    private static void createEngineer()
    {
        // Prompt user to enter Engineer's ID within a specified range
        Console.WriteLine($"Enter Engineer's ID: the range for the id is from {2e8} to {4e8}");
        int id = isValidIntInput(); // Call a method to validate and retrieve an integer input

        // Check if the entered ID is within the specified range
        if (id < 2e8 || id > 4e8)
        {
            throw new Exception("Invalid ID number. Please enter in the range.");
        }

        // Prompt user to enter Engineer's name
        Console.WriteLine("Enter Engineer's name:");
        string name = Console.ReadLine() ?? throw new Exception("ERROR: Enter a valid input (not null)");

        // Prompt user to enter Engineer's email using a separate method for validation
        Console.WriteLine("Enter the email for Engineer:");
        string email = emailCheck_update_createEngineer();

        // Prompt user to enter Engineer's cost, and validate the input
        Console.WriteLine("Enter the cost for Engineer:");
        double cost = double.TryParse(Console.ReadLine(), out cost) ? cost : throw new Exception("Error casting the input to double");

        // Prompt user to enter Engineer's level, and validate the input
        Console.WriteLine("Enter Engineer's level:");
        DO.EngineerExperience level = (DO.EngineerExperience)isValidIntInput(); // Assuming EngineerExperience is an enum
        Console.WriteLine("Enter the task id for the engineer:");
        int taskId = isValidIntInput();
        Console.WriteLine("Enter the task alias:");
        string taskAlias = Console.ReadLine() ??
            throw new Exception("ERROR: Enter a valid input (not null)");
        BO.TaskInEngineer task = new BO.TaskInEngineer
        {
            Id = taskId,
            Alias = taskAlias
        };
        // Create a new Engineer instance with the collected information
        BO.Engineer engineerInstance = new BO.Engineer {
            Id = id, Email = email, Cost = cost, Name = name, Level = (DO.EngineerExperience)level, Task = task
        };

        // Call the Create method on the data access layer to store the Engineer instance
        //s_dalEngineer!.Create(engineerInstance); //(stage 1)
        s_bl!.Engineers!.Create(engineerInstance);

        // Inform the user that the data has been received successfully
        Console.WriteLine("The data received successfully. Here is the Data:");

        // Print the details of the created Engineer instance
        Console.WriteLine(engineerInstance);
    }

    // Method to update an existing Engineer's information
    private static void updateEngineer()
    {
        // Prompt user to enter the ID of the Engineer to be updated
        Console.WriteLine("What Engineer do you want to update: (enter an ID)\n");
        int id = isValidIntInput(); // Call a method to validate and retrieve an integer input

        // Retrieve the current data of the Engineer to be updated from the data access layer
        //Engineer currentEngineerData = s_dalEngineer!.Read(id) ?? throw new Exception("Engineer with such ID does not exist"); //(satge1)
        Engineer currentEngineerData = s_bl!.Engineers!.Read(id) ??
            throw new Exception("Engineer with such ID does not exist");



        // Update the Engineer's Email
        updateEngineer_PrintText("Email");

        string? email;
        if (yesOrNo())
        {
            Console.WriteLine("Enter the Engineer's email:");
            email = emailCheck_update_createEngineer();
        }
        else
        {
            email = currentEngineerData!.Email!;
        }

        // Update the Engineer's Name
        updateEngineer_PrintText("Name");

        string? name;
        if (yesOrNo())
        {
            Console.WriteLine("Enter the Engineer's name:");
            name = Console.ReadLine() ?? throw new Exception("ERROR: Enter a valid input (not null)");
        }
        else
        {
            name = currentEngineerData!.Name!;
        }

        // Update the Engineer's Cost
        updateEngineer_PrintText("Cost");

        double cost;
        if (yesOrNo())
        {
            Console.WriteLine("Enter the Engineer's cost:");
            cost = isValidIntInput();
        }
        else
        {
            cost = (double)currentEngineerData!.Cost!;
        }

        // Update the Engineer's Level
        updateEngineer_PrintText("Level");

        DO.EngineerExperience? level;
        if (yesOrNo())
        {
            Console.WriteLine("Enter the Engineer's level");
            level = (DO.EngineerExperience)isValidIntInput();
        }
        else
        {
            level = currentEngineerData.Level;
        }
        updateEngineer_PrintText("Task assined to the Engineer");
        BO.TaskInEngineer task;
        if (yesOrNo())
        {
            Console.WriteLine("Enter the task id for the engineer:");
            int taskId = isValidIntInput();
            Console.WriteLine("Enter the task alias:");
            string taskAlias = Console.ReadLine() ??
                throw new Exception("ERROR: Enter a valid input (not null)");
            task = new BO.TaskInEngineer
            {
                Id = taskId,
                Alias = taskAlias
            };
        }
        else
        {
            task = currentEngineerData.Task!;
        }
        // Create a new Engineer instance with the updated information
        Engineer updatedEngineerData = new Engineer
        {
            Id = id,
            Email = email,
            Name = name,
            Cost = cost,
            Level = level,
            Task = task
        };

        // Call the Update method on the data access layer to apply the changes
        s_bl!.Engineers!.Update(updatedEngineerData);

        // Inform the user that the data has been received successfully
        Console.WriteLine("The data received successfully. Here is the Data:");

        // Print the details of the updated Engineer instance
        Console.WriteLine(updatedEngineerData);
    }

    // Method to prompt the user whether they wish to update a specific property of an Engineer
    private static void updateEngineer_PrintText(string type)
    {
        // Display a message asking the user if they wish to update the specified property
        Console.WriteLine($"Do you wish to update the {type} property? Enter yes/no \n");
    }

    // Method to get a valid email address input for updating or creating an Engineer
    private static string emailCheck_update_createEngineer()
    {
        // Read user input for the email address, ensuring it is not null
        string email = Console.ReadLine() ?? throw new Exception("ERROR: Enter a valid input (Not null)");

        // Check if the entered email address is valid
        if (!IsValidEmail(email))
        {
            throw new Exception("Email address is not valid");
        }

        // Return the valid email address
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

    // Method to read details of a specific Engineer by ID
    private static void readEngineer()
    {
        // Prompt the user to enter the ID of the Engineer to be printed
        Console.WriteLine("What Engineer do you want to Print: (enter an ID)\n");

        // Validate and get the ID from user input
        int id = isValidIntInput();

        // Retrieve the current Engineer data using the data access layer
        BO.Engineer correntEngineerData = s_bl!.Engineers!.Read(id) ??
            throw new Exception("Engineer with such ID does not exist");

        // Call the printEngineer method to display details of the current Engineer
        Console.WriteLine(correntEngineerData);
    }

    // Method to print details of a specific Engineer
    //private static void printEngineer(Engineer engineer)
    //{
    //    // Display the details of the Engineer
    //    Console.WriteLine("Engineer ID: " + engineer.Id);
    //    Console.WriteLine("Engineer Name: " + engineer.Name);
    //    Console.WriteLine("Engineer Level: " + engineer.Level);
    //    Console.WriteLine("Engineer Email: " + engineer.Email);
    //    Console.WriteLine("Engineer Cost: " + engineer.Cost);
    //    Console.WriteLine("\n");
    //}

    // Method to read details of all Engineers
    private static void readAllEngineers()
    {
        // Retrieve a list of all Engineers from the data access layer
        IEnumerable<BO.Engineer> engineers = s_bl!.Engineers!.ReadAll(null)!;

        // Iterate through each Engineer and print their details
        if (engineers.Any())
            foreach (Engineer engineer in engineers)
            {
                // Call the printEngineer method to display details of the current Engineer
                Console.WriteLine(engineer!);
            }
    }

    // Method to remove a specific Engineer by ID
    private static void removeEngineer()
    {
        // Prompt the user to enter the ID of the Engineer to be removed
        Console.WriteLine("Enter the ID of Engineer you want to remove\n ");

        // Validate and get the ID from user input
        int id = isValidIntInput();

        // Retrieve the current Engineer data using the data access layer
        BO.Engineer correntEngineerData = s_bl!.Engineers!.Read(id) ??
            throw new Exception("Engineer with such ID does not exist");

        // Delete the Engineer using the data access layer
        s_bl!.Engineers.Delete(id);
    }

    // Method to remove all Engineers
    private static void removeAllEngineers()
    {
        // Delete all Engineers using the data access layer
        s_bl!.Engineers!.DeleteAll();
    }


    //Task 
    // Method to create a new task
    private static void createTaskPhase1()
    {
        // Prompt user for task alias
        Console.WriteLine("Enter the task alias: ");
        string? _Alias = Console.ReadLine() ??
            throw new Exception("ERROR: enter a valid input (Not a null)");

        //option to add description
        Console.WriteLine("Does the task has description? (yes or no)");
        string? _Description;
        if (yesOrNo())
        {
            Console.WriteLine("Enter the task description");
            _Description = Console.ReadLine() ??
               throw new Exception("ERROR: enter a valid input (Not a null)");
        }
        else
        {
            _Description = "No description";
        }


        // Set the creation date to the current date and time
        DateTime _CreatedAtDate = DateTime.Now;

        // Prompt user for required effort time in the specified format
        Console.WriteLine("Enter the required effort time for the task, enter in the format [d.]hh:mm:ss[.fffffff]");
        string? userInput = Console.ReadLine() ??
            throw new Exception("ERROR: enter a valid input (Not a null)");
        TimeSpan _requiredEffortTimeI = checkTimeSpanFormat(userInput);


        // Prompt user for the complexity of the task in the range 0-5
        Console.WriteLine("Enter the complexity of the task? (1-5)");
        userInput = Console.ReadLine() ??
            throw new Exception("ERROR: enter a valid input (Not a null)");
        DO.EngineerExperience complexity = (DO.EngineerExperience)getInt(userInput);
        // Create a new Task instance with the provided details
        BO.Task inputTask = new BO.Task
        {
            Id = 0,
            Alias = _Alias,
            Description = _Description,
            CreatedAtDate = _CreatedAtDate,
            RequiredEffortTime = _requiredEffortTimeI,
            Complexity = complexity,
        };

        // Call the Create method in the data access layer to store the new task
        s_bl!.Tasks!.Create(inputTask);

        // Display the received data
        Console.WriteLine("The data received successfully, here is the Data:");
        Console.WriteLine("At this stage, the task ID is not 0. To see the task ID, type 1 then 4. Don't worry, be happy!");
        Console.WriteLine(inputTask);
    }

    // Methods to update task for different phases
    private static void updateTaskPhase1()
    {
        // Prompt user to enter the ID of the task to be updated
        Console.WriteLine("Which task do you want to update? (enter an ID)\n");
        int id = isValidIntInput();

        // Read the existing task data from the data access layer
        BO.Task taskT = s_bl!.Tasks!.Read(id) ?? throw new Exception("Task with such ID does not exist");

        // Prompt user to update the Alias field
        updateEngineer_PrintText("Alias");

        string alias;
        if (yesOrNo())
        {
            printFieldForYes("Alias");
            alias = Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not a null)");
        }
        else
        {
            alias = taskT!.Alias!;
        }

        // Prompt user to update the Description field
        updateEngineer_PrintText("Description");

        string description;
        if (yesOrNo())
        {
            printFieldForYes("Description");
            description = Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not a null)");
        }
        else
        {
            description = taskT!.Description!;
        }

        // The creation date remains unchanged, as it is not for updating
        DateTime? createdAtDate = taskT!.CreatedAtDate;

        // Prompt user to update the Required Effort Time field
        updateEngineer_PrintText("Required effort time");

        TimeSpan? requiredEffortTime;
        if (yesOrNo())
        {
            printFieldForYes("Required effort time");
            requiredEffortTime = checkTimeSpanFormat(Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not a null)"));
        }
        else
        {
            requiredEffortTime = taskT!.RequiredEffortTime;
        }

        // Prompt user to update the Complexity field
        updateEngineer_PrintText("Complexity");

        DO.EngineerExperience complexity;
        if (yesOrNo())
        {
            printFieldForYes("Complexity");
            complexity = (DO.EngineerExperience)getInt(Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not a null)"));
        }
        else
        {
            complexity = taskT!.Complexity;
        }


        // Create a new Task instance with the updated details
        BO.Task taskToUpdate = new BO.Task { Id = id,
            Alias = alias,
            Description = description,
            CreatedAtDate = createdAtDate,
            RequiredEffortTime = requiredEffortTime,
            Complexity = complexity,
        };

        // Call the Update method in the data access layer to apply the changes
        s_bl!.Tasks!.Update(taskToUpdate);

        // Display the updated data
        Console.WriteLine("The data received successfully, here is the updated Data:");
        Console.WriteLine(taskToUpdate);
    }

    private static void loopOverTasksForUpdatingPhase2()
    {
        List<BO.Task?> taskList = s_bl.Tasks.ReadAll(null).ToList();
        foreach (Task? task in taskList) 
        {
            UpdateTaskPhase2(task!);
        }
    }

    private static void UpdateTaskPhase2(BO.Task taskToUpdateTheStartDate)
    {

        string? userInput = null;
        DateTime? recomendedStartDate = s_bl!.Tasks.FirstAvailableStartDate(taskToUpdateTheStartDate);

        // Prompt user for planned start date
        Console.WriteLine("Enter the planned start date for the task (e.g., 2024-01-10): ");
        Console.WriteLine($"The recommended start date for task with id = {taskToUpdateTheStartDate.Id}" +
            $" is {recomendedStartDate}");
        userInput = Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not a null)");
        DateTime? _scheduleDate = CheckDateTimeFormat(userInput);

        // Prompt user for task forcast date
        Console.WriteLine("Enter the task foracst date (e.g., 2024-01-10):");
        userInput = Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not a null)");
        DateTime? _forcastDate = CheckDateTimeFormat(userInput);

        // Prompt user for task deadline date
        Console.WriteLine("Enter the task deadline date (e.g., 2024-01-10):");
        userInput = Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not a null)");
        DateTime _deadLineDate = CheckDateTimeFormat(userInput);

        // Create a new Task instance with the provided details
        Task inputOfUpdetedTask = new Task
        { 
            //updeted filds in phase 2
            ScheduledDate =  _scheduleDate,
            ForecastDate = _forcastDate,
            DeadlineDate =  _deadLineDate,
            //
            Id = taskToUpdateTheStartDate.Id,
            Alias = taskToUpdateTheStartDate.Alias,
            Description = taskToUpdateTheStartDate.Description,
            CreatedAtDate = taskToUpdateTheStartDate.CreatedAtDate,
            Status = taskToUpdateTheStartDate.Status,
            Dependencies = taskToUpdateTheStartDate.Dependencies,
            RequiredEffortTime = taskToUpdateTheStartDate.RequiredEffortTime,
            StartDate = taskToUpdateTheStartDate.StartDate,
            CompleteDate = taskToUpdateTheStartDate.CompleteDate,
            Deliverables = taskToUpdateTheStartDate.Deliverables,
            Remarks = taskToUpdateTheStartDate.Remarks,
            Engineer = taskToUpdateTheStartDate.Engineer,
            Complexity = taskToUpdateTheStartDate.Complexity
        };

        // Call the Create method in the data access layer to store the new task
        s_bl!.Tasks!.Update(inputOfUpdetedTask);

        // Display the received data
        Console.WriteLine("The data received successfully, here is the Data:");
        Console.WriteLine("At this stage, the task ID is not 0. To see the task ID, type 1 then 4. Don't worry, be happy!");
        Console.WriteLine(inputOfUpdetedTask);
    }
    private static void updateTaskPhase3()
    {
        // Prompt user to enter the ID of the task to be updated
        Console.WriteLine("Which task do you want to update? (enter an ID)");
        int id = isValidIntInput();

        // Retrieve the existing task from the data access layer using the provided ID
        BO.Task taskT = s_bl!.Tasks!.Read(id) ??
            throw new Exception("Task with such ID does not exist");

        // If the task is already completed, inform the user and exit the function
        if (taskT.Status == BO.Enums.Status.Done)
        {
            Console.WriteLine("The task is completed and cannot be updated.");
            return;
        }

        // Initialize variables with existing task details for potential updates
        List<BO.TaskInList>? dependencies = taskT!.Dependencies;
        DateTime? createdAtDate = taskT!.CreatedAtDate;
        TimeSpan? requiredEffortTime = taskT!.RequiredEffortTime;
        DO.EngineerExperience complexity = taskT!.Complexity;
        DateTime? startDate = taskT!.StartDate;
        DateTime? scheduleDate = taskT!.ScheduledDate;
        DateTime? deadLineDate = taskT!.DeadlineDate;
        string? deliverables = taskT!.Deliverables!;
        string? remarks = taskT!.Remarks!;
        string? alias = taskT!.Alias!;
        BO.EngineerInTask? engineer = taskT!.Engineer;
        DateTime? completeDate = taskT!.CompleteDate;
        BO.Enums.Status status = taskT.Status;
        DateTime? forecastDate = taskT.ForecastDate;

        // Prompt the user to decide whether to update the task description
        updateEngineer_PrintText("Description");
        string description;
        if (yesOrNo())
        {
            printFieldForYes("Description");
            description = Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not null)");
        }
        else
        {
            description = taskT!.Description!;
        }

        // Handle the engineer assignment to the task
        if (taskT!.Engineer != null)
        {
            Console.WriteLine("The task is assigned to the engineer with the following details:");
            Console.WriteLine($"ID: {taskT!.Engineer.Id}");
            Console.WriteLine($"Name: {taskT!.Engineer.Name}");

            // Prompt the user to decide whether to update the engineer assigned to the task
            Console.WriteLine("Would you like to update the engineer assigned to the task? (yes/no)");
            if (yesOrNo())
            {
                Console.WriteLine("Enter the Engineer ID for the task");
                int engineerId = isValidIntInput();
                string name = Console.ReadLine() ??
                    throw new Exception("ERROR: enter a valid input (Not null)");
                engineer = new BO.EngineerInTask
                {
                    Id = engineerId,
                    Name = name
                };
            }

            // Check if the task was completed
            Console.WriteLine("Was the task completed? (yes/no)");
            if (yesOrNo())
            {
                status = BO.Enums.Status.Done;
                completeDate = DateTime.Now;

                // Prompt the user to update deliverables and remarks if the task is completed
                updateEngineer_PrintText("Deliverables");
                if (yesOrNo())
                {
                    printFieldForYes("Deliverables");
                    deliverables = Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not null)");
                }

                updateEngineer_PrintText("Remarks");
                if (yesOrNo())
                {
                    printFieldForYes("Remarks");
                    remarks = Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not null)");
                }
            }
            else
            {
                // If the task was not completed, use the existing completion date
                completeDate = taskT!.CompleteDate;
            }
        }
        else
        {
            // If no engineer is assigned to the task, prompt the user to assign one
            Console.WriteLine("Would you like to assign a new engineer to the task? (yes/no)");
            if (yesOrNo())
            {
                Console.WriteLine("Enter the Engineer ID for the task");
                int engineerId = isValidIntInput();
                string name = Console.ReadLine() ??
                    throw new Exception("ERROR: enter a valid input (Not null)");
                engineer = new BO.EngineerInTask
                {
                    Id = engineerId,
                    Name = name
                };
            }
        }

        // Create a new task instance with the updated details
        Task taskToUpdate = new Task
        {
            Id = id,
            Alias = alias,
            Description = description,
            CreatedAtDate = createdAtDate,
            Status = status,
            Dependencies = dependencies,
            RequiredEffortTime = requiredEffortTime,
            StartDate = startDate,
            ScheduledDate = scheduleDate,
            ForecastDate = forecastDate,
            DeadlineDate = deadLineDate,
            CompleteDate = completeDate,
            Deliverables = deliverables,
            Remarks = remarks,
            Engineer = engineer,
            Complexity = complexity
        };

        // Apply the updates to the task in the data access layer
        s_bl!.Tasks!.Update(taskToUpdate);

        // Notify the user that the update was successful and display the updated data
        Console.WriteLine("The task was successfully updated. Here is the updated data:");
        Console.WriteLine(taskToUpdate);
    }




    // Method to read and print details of a specific task based on user input
    private static void readTask()
    {
        Console.WriteLine("Enter the task ID which you want to print:");
        string? userInput = Console.ReadLine() ??
            throw new Exception("ERROR: enter a valid input (Not a null)");
        int id = getInt(userInput);

        // Retrieve the task data from the data access layer based on the entered ID
        BO.Task currentTaskData = s_bl!.Tasks!.Read(id) ?? throw new Exception("Task with such ID does not exist");

        // Call the PrintTask method to display details of the current task
        Console.WriteLine(currentTaskData);
    }

    //// Method to print details of a specific task
    //private static void PrintTask(Task taskToPrint)
    //{
    //    Console.WriteLine("Task ID:" + taskToPrint.Id);
    //    Console.WriteLine("Task Alias:" + taskToPrint.Alias);
    //    Console.WriteLine("Task Description:" + taskToPrint.Description);
    //    Console.WriteLine("Task Created at:" + taskToPrint.CreatedAtDate);
    //    Console.WriteLine("Required time for the task:" + taskToPrint.RequiredEffortTime);
    //    Console.WriteLine("Does the task have a milestone?:" + taskToPrint.IsMilestone);
    //    Console.WriteLine("Complexity's task:" + (DO.EngineerExperience)taskToPrint.Complexity);
    //    Console.WriteLine("The task started at:" + taskToPrint.StartDate);
    //    Console.WriteLine("Task schedule date:" + taskToPrint.ScheduledDate);
    //    Console.WriteLine("Dead line task:" + taskToPrint.DeadlineDate);
    //    Console.WriteLine("Task completed at:" + taskToPrint.CompleteDate);
    //    Console.WriteLine("Task deliverables:" + taskToPrint.Deliverables);
    //    Console.WriteLine("Task remarks:" + taskToPrint.Remarks);
    //    Console.WriteLine("Task Engineer ID:" + taskToPrint.EngineerId);
    //    Console.WriteLine("\n");

    //}

    // Method to read and print details of all tasks
    private static void readAllTask()
    {
        // Retrieve a list of all tasks from the data access layer
        IEnumerable<Task> tasks = s_bl!.Tasks.ReadAll()!;

        // Iterate through each task and call the PrintTask method to display details
        if (tasks.Any())
            foreach (Task task in tasks)
            {
                Console.WriteLine(task!);
            }
    }

    // Method to remove a specific task based on user input
    private static void removeTask()
    {
        Console.WriteLine("Enter the ID of Task you want to remove");
        int id = isValidIntInput();

        // Retrieve the task data from the data access layer based on the entered ID
        BO.Task currentTaskData = s_bl!.Tasks!.Read(id) ??
            throw new Exception("Task with such ID does not exist");

        // Call the Delete method in the data access layer to remove the task
        s_bl!.Tasks!.Delete(id);
    }

    // Method to remove all tasks
    private static void removeAllTasks()
    {
        // Call the DeleteAll method in the data access layer to remove all tasks
        s_bl!.Tasks.DeleteAll();
    }


    //Help
    // Helper function to prompt the user to enter a task field
    private static void printFieldForYes(string field)
    {
        Console.WriteLine($"Enter the task {field}: ");
    }

    // Helper function to validate and retrieve an integer input
    private static int isValidIntInput()
    {
        // Check if the input can be transformed from string to int
        bool isValid = int.TryParse(Console.ReadLine(), out int value);
        if (isValid)
        {
            return value;
        }
        else
        {
            throw new Exception("Error: the input cannot be converted to int type");
        }
    }

    // Helper function to parse an integer from a string
    private static int getInt(string userInput)
    {
        return int.Parse(userInput);
    }

    // Helper function to check and parse a TimeSpan from user input
    private static TimeSpan checkTimeSpanFormat(string? userInput)
    {
        if (TimeSpan.TryParse(userInput, out TimeSpan _RequiredEffortTime))
        {
            return TimeSpan.Parse(userInput);
        }
        else
        {
            throw new Exception("ERROR: the duration is incorrect");
        }
    }

    // Helper function to check and parse a DateTime from user input
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

    // Helper function to get a yes or no answer from the user
    private static bool yesOrNo()
    {
        // Read input and convert to uppercase
        string message = Console.ReadLine()?.Trim().ToUpper() ?? throw new FormatException("Can't enter a null");
        bool _answer = message!.StartsWith('Y');
        return _answer;
    }
    
    /// <summary>
    /// This method allows the user to enter several tasks in a row for phase 1.
    /// To return to the main menu of stage 1, the user can enter 3.
    /// </summary>
    private static void phase1TasksInputInARow()
    {
        Console.WriteLine("In this option you can enter several tasks in a row. To return to the main menu of <stage 1>, enter 3.");
        while (true)
        {
            Console.WriteLine("Enter your choice: ");
            if (int.TryParse(Console.ReadLine(), out int userInput))
            {
                if (userInput != 3)
                {
                    createTaskPhase1();
                }
                else
                {
                    return;
                }
            }
            else
            {
                Console.WriteLine("Please enter an integer value.");
            }
        }
    }
}




