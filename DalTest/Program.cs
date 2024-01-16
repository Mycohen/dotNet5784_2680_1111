// This namespace is for testing classes related to Data Access Layer (DAL)
namespace DalTest
{
    // Importing necessary namespaces
    using DalApi;
    using Dal;
    using System.Net.Mail;
    using DO;
    using System.Runtime.InteropServices.Marshalling;

    // A static class that serves as the entry point for the program
    internal static class Program
    {
        // Dependency, Engineer, and Task instances for Data Access Layer
        public static IDependency? s_dalDependency = new DependencyImplementation();
        public static IEngineer? s_dalEngineer = new EngineerImplementation();
        public static ITask? s_dalTask = new TaskImplementation();


        // Main method, the starting point of the program
        public static void Main()
        {
            try
            {
                // Initialize Data Access Layer dependencies
                Initialization.Do(s_dalDependency, s_dalEngineer, s_dalTask);

                // Main program loop
                do
                {
                    // Display the main menu
                    printMainMenu();

                    // Parse the user's choice from the main menu
                    if (Enum.TryParse(Console.ReadLine(), out MainMenuOption mainMenuChoice))
                    {
                        // Process the user's choice
                        switch (mainMenuChoice)
                        {
                            case MainMenuOption.MainExit:
                                // Exit the program
                                Environment.Exit(0);
                                break;
                            case MainMenuOption.TaskMenu:
                                // Handle user choice for Task operations
                                taskOptions();
                                break;
                            case MainMenuOption.EngineerMenu:
                                // Handle user choice for Engineer operations
                                engineerOptions();
                                break;
                            case MainMenuOption.DependencyMenu:
                                // Handle user choice for Dependency operations
                                dependencyOptions();
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
                } while (true); // Infinite loop for continuous user interaction
            }
            catch (Exception errorMessage)
            {
                // Catch and display any exceptions that occur during program execution
                Console.WriteLine(errorMessage);
            }
        }

        //Sub menu for task options
        private static void taskOptions()
        {
            // Display sub-menu for Task operations
            printSubMenu("Task");

            // Parse user's choice from the sub-menu
            if (Enum.TryParse(Console.ReadLine(), out CrudMenuOption crudMenuChoice))
            {
                // Process the user's choice
                switch (crudMenuChoice)
                {
                    case CrudMenuOption.SubExit:
                        // Exit to the main menu
                        return;
                    case CrudMenuOption.CreateOp:
                        // Handle user choice to create a new task
                        createTask();
                        break;
                    case CrudMenuOption.UpdateOp:
                        // Handle user choice to update an existing task
                        updateTask();
                        break;
                    case CrudMenuOption.PrintSingleOp:
                        // Handle user choice to print a single task
                        readTask();
                        break;
                    case CrudMenuOption.PrintAllOp:
                        // Handle user choice to print all tasks
                        readAllTask();
                        break;
                    case CrudMenuOption.DeleteOp:
                        // Handle user choice to remove a task
                        removeTask();
                        break;
                    case CrudMenuOption.DeleteAllOp:
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
            printSubMenu("Engineer");

            // Parse user's choice from the sub-menu
            if (Enum.TryParse(Console.ReadLine(), out CrudMenuOption crudMenuChoice))
            {
                // Process the user's choice
                switch (crudMenuChoice)
                {
                    case CrudMenuOption.SubExit:
                        // Exit to the main menu
                        return;
                    case CrudMenuOption.CreateOp:
                        // Handle user choice to create a new engineer
                        createEngineer();
                        break;
                    case CrudMenuOption.UpdateOp:
                        // Handle user choice to update an existing engineer
                        updateEngineer();
                        break;
                    case CrudMenuOption.PrintSingleOp:
                        // Handle user choice to print a single engineer
                        readEngineer();
                        break;
                    case CrudMenuOption.PrintAllOp:
                        // Handle user choice to print all engineers
                        readAllEngineers();
                        break;
                    case CrudMenuOption.DeleteOp:
                        // Handle user choice to remove an engineer
                        removeEngineer();
                        break;
                    case CrudMenuOption.DeleteAllOp:
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

        //Sub menu for dependency options
        private static void dependencyOptions()
        {
            // Display sub-menu for Dependency operations
            printSubMenu("Dependency");

            // Parse user's choice from the sub-menu
            if (Enum.TryParse(Console.ReadLine(), out CrudMenuOption crudMenuChoice))
            {
                // Process the user's choice
                switch (crudMenuChoice)
                {
                    case CrudMenuOption.SubExit:
                        // Exit to the main menu
                        return;
                    case CrudMenuOption.CreateOp:
                        // Handle user choice to create a new dependency
                        createDependency();
                        break;
                    case CrudMenuOption.UpdateOp:
                        // Handle user choice to update an existing dependency
                        updateDependency();
                        break;
                    case CrudMenuOption.PrintSingleOp:
                        // Handle user choice to print a single dependency
                        readDependency();
                        break;
                    case CrudMenuOption.PrintAllOp:
                        // Handle user choice to print all dependencies
                        readAllDependency();
                        break;
                    case CrudMenuOption.DeleteOp:
                        // Handle user choice to remove a dependency
                        removeDependency();
                        break;
                    case CrudMenuOption.DeleteAllOp:
                        // Handle user choice to remove all dependencies
                        removeAllDependency();  // Note: This might be a mistake, consider updating the action
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

        //printing the main menu for the switch
        private static void printMainMenu()
        {
            // Display the main menu options
            Console.WriteLine(@"Enter 0 for quitting the program:");
            Console.WriteLine("Enter 1 for Tasks menu:");
            Console.WriteLine("Enter 2 for Engineers menu:");
            Console.WriteLine("Enter 3 for Dependencies menu");
        }

        //printing the sub menu for the switch
        private static void printSubMenu(string entity)
        {
            // Display the sub-menu options for a given entity (Task, Engineer, Dependency)
            Console.WriteLine("Enter 0 for returning to the main menu.");
            Console.WriteLine($"Enter 1 to Create {entity}");
            Console.WriteLine($"Enter 2 to Update the {entity}");
            Console.WriteLine($"Enter 3 to Print a {entity} by entering its ID");
            Console.WriteLine($"Enter 4 to Read all of the {entity} elements");
            Console.WriteLine($"Enter 5 to Remove an instance of the {entity} element");
            Console.WriteLine($"Enter 6 to Remove all of the {entity} elements");
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
            EngineerExperience level = (EngineerExperience)isValidIntInput(); // Assuming EngineerExperience is an enum

            // Create a new Engineer instance with the collected information
            Engineer engineerInstance = new Engineer(Id: id, Email: email, Cost: cost, Name: name, Level: level);

            // Call the Create method on the data access layer to store the Engineer instance
            s_dalEngineer!.Create(engineerInstance);

            // Inform the user that the data has been received successfully
            Console.WriteLine("The data received successfully. Here is the Data:");

            // Print the details of the created Engineer instance
            printEngineer(engineerInstance);
        }

        // Method to update an existing Engineer's information
        private static void updateEngineer()
        {
            // Prompt user to enter the ID of the Engineer to be updated
            Console.WriteLine("What Engineer do you want to update: (enter an ID)\n");
            int id = isValidIntInput(); // Call a method to validate and retrieve an integer input

            // Retrieve the current data of the Engineer to be updated from the data access layer
            Engineer currentEngineerData = s_dalEngineer!.Read(id) ?? throw new Exception("Engineer with such ID does not exist");

            // Flag to determine whether to update each attribute, initially set to false
            bool flag;

            // Update the Engineer's Email
            updateEngineer_PrintText("Email");
            flag = yesOrNo();
            string? email;
            if (flag)
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
            flag = yesOrNo();
            string? name;
            if (flag)
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
            flag = yesOrNo();
            double cost;
            if (flag)
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
            flag = yesOrNo();
            EngineerExperience level;
            if (flag)
            {
                Console.WriteLine("Enter the Engineer's level");
                level = (EngineerExperience)isValidIntInput();
            }
            else
            {
                level = currentEngineerData!.Level!;
            }

            // Create a new Engineer instance with the updated information
            Engineer updatedEngineerData = new Engineer(Id: id, Email: email, Name: name, Cost: cost, Level: level);

            // Call the Update method on the data access layer to apply the changes
            s_dalEngineer!.Update(updatedEngineerData);

            // Inform the user that the data has been received successfully
            Console.WriteLine("The data received successfully. Here is the Data:");

            // Print the details of the updated Engineer instance
            printEngineer(updatedEngineerData);
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
            Engineer correntEngineerData = s_dalEngineer!.Read(id) ?? throw new Exception("Engineer with such ID does not exist");

            // Call the printEngineer method to display details of the current Engineer
            printEngineer(correntEngineerData);
        }

        // Method to print details of a specific Engineer
        private static void printEngineer(Engineer engineer)
        {
            // Display the details of the Engineer
            Console.WriteLine("Engineer ID: " + engineer.Id);
            Console.WriteLine("Engineer Name: " + engineer.Name);
            Console.WriteLine("Engineer Level: " + engineer.Level);
            Console.WriteLine("Engineer Email: " + engineer.Email);
            Console.WriteLine("Engineer Cost: " + engineer.Cost);
            Console.WriteLine("\n");
        }

        // Method to read details of all Engineers
        private static void readAllEngineers()
        {
            // Retrieve a list of all Engineers from the data access layer
            List<Engineer> engineers = s_dalEngineer!.ReadAll();

            // Iterate through each Engineer and print their details
            foreach (Engineer engineer in engineers)
            {
                // Call the printEngineer method to display details of the current Engineer
                printEngineer(engineer!);
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
            Engineer correntEngineerData = s_dalEngineer!.Read(id) ?? throw new Exception("Engineer with such ID does not exist");

            // Delete the Engineer using the data access layer
            s_dalEngineer.Delete(id);
        }

        // Method to remove all Engineers
        private static void removeAllEngineers()
        {
            // Delete all Engineers using the data access layer
            s_dalEngineer!.DeleteAll();
        }


        //Dependency 
        // Method to create a new Dependency between tasks
        private static void createDependency()
        {
            // Prompt the user to enter the ID of the dependent task
            Console.WriteLine("Enter dependent task ID");
            int dependentTask = isValidIntInput();

            // Prompt the user to provide the task necessary for the current task
            Console.WriteLine("Provide the task necessary for the current task");
            int dependsOnTask = isValidIntInput();

            // Create a new Dependency instance with the provided information
            Dependency dependencyInstance = new Dependency(0, dependentTask, dependsOnTask);

            // Save the Dependency instance using the data access layer
            s_dalDependency!.Create(dependencyInstance);

            // Display a success message and print the created Dependency data
            Console.WriteLine("The data received successfully. Here is the Data:\n");
            Console.WriteLine("At this stage, the task ID is not 0. For seeing the task ID, type 3 then 4. Don't worry, be happy!");
            printDependency(dependencyInstance);
        }

        // Method to update an existing Dependency
        private static void updateDependency()
        {
            // Prompt the user to enter the ID of the Dependency they want to update
            Console.WriteLine("Enter the ID of the task you want to update");
            int id = isValidIntInput();

            // Retrieve the current Dependency data based on the provided ID
            Dependency currentDependencyData = s_dalDependency!.Read(id) ?? throw new Exception("Dependency with such ID does not exist");

            // Prompt the user if they want to update the dependent task
            Console.WriteLine("Do you want to update the dependent task? (y/n)");
            bool flag = yesOrNo();
            int dependentTask;
            if (flag)
            {
                // If yes, prompt the user to enter the new dependent task
                Console.WriteLine("Enter the new dependent task");
                dependentTask = isValidIntInput();
            }
            else
            {
                // If no, retain the current dependent task
                dependentTask = (int)currentDependencyData!.DependentTask!;
            }

            // Prompt the user if they want to update the dependency task
            Console.WriteLine("Do you want to update the dependency task? (y/n)");
            flag = yesOrNo();
            int dependsOnTask;
            if (flag)
            {
                // If yes, prompt the user to enter the new dependency task
                Console.WriteLine("Enter the new dependency task");
                dependsOnTask = isValidIntInput();
            }
            else
            {
                // If no, retain the current dependency task
                dependsOnTask = (int)currentDependencyData!.DependsOnTask!;
            }

            // Create a new Dependency instance with the updated information
            Dependency updatedDependencyData = new Dependency(currentDependencyData.Id,dependentTask, dependsOnTask);

            // Update the Dependency in the data access layer
            s_dalDependency!.Update(updatedDependencyData);

            // Display a success message and print the updated Dependency data
            Console.WriteLine("The data received successfully. Here is the Data:\n");
            printDependency(updatedDependencyData);
        }

        // Method to read and print details of a specific Dependency
        private static void readDependency()
        {
            // Prompt the user to enter the ID of the Dependency they want to read
            Console.WriteLine("Enter the ID of dependency you want to read");
            int id = isValidIntInput();
            
            // Retrieve the current Dependency data based on the provided ID
            Dependency currentDependencyData = s_dalDependency!.Read(id) ?? throw new Exception("Dependency with such ID does not exist");

            // Call the printDependency method to display details of the current Dependency
            printDependency(currentDependencyData);
        }

        // Method to read and print details of all Dependencies
        private static void readAllDependency()
        {
            // Retrieve a list of all Dependencies from the data access layer
            List<Dependency> dependencies = s_dalDependency!.ReadAll();

            // Iterate through each Dependency and print its details
            foreach (Dependency dependency in dependencies)
            {
                // Call the printDependency method to display details of the current Dependency
                printDependency(dependency);
            }
        }

        // Method to remove a Dependency based on the provided ID
        private static void removeDependency()
        {
            // Prompt the user to enter the ID of the Dependency they wish to delete
            Console.WriteLine("Enter the ID of the Dependency you wish to delete");

            // Read and validate the user input for the Dependency ID
            int id = isValidIntInput();

            // Call the Delete method in the data access layer to remove the Dependency
            s_dalDependency!.Delete(id);
        }

        // Method to print details of a Dependency
        private static void printDependency(Dependency dependency)
        {
            // Display the ID of the dependency
            Console.WriteLine($"The ID of the dependency is: {dependency.Id}");

            // Display the ID of the dependent task
            Console.WriteLine($"The dependent task is: {dependency.DependentTask}");

            // Display the ID of the task that is necessary for the current task
            Console.WriteLine($"The task that is necessary for the current task: {dependency.DependsOnTask}");

            // An empty line between Dependency details
            Console.WriteLine("\n");
        }

        // Method to remove all Dependencies
        private static void removeAllDependency()
        {
            // Call the DeleteAll method in the data access layer to remove all Dependencies
            s_dalDependency!.DeleteAll();
        }


        //Task 
        // Method to create a new task
        private static void createTask()
        {
            // Prompt user for task alias
            Console.WriteLine("Enter the task alias: ");
            string? _Alias = Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not a null)");

            // Prompt user for task description
            Console.WriteLine("Enter the task description");
            string? _Description = Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not a null)");

            // Set the creation date to the current date and time
            DateTime _CreatedAtDate = DateTime.Now;

            // Prompt user for required effort time in the specified format
            Console.WriteLine("Enter the required effort time for the task, enter in the format [d.]hh:mm:ss[.fffffff]");
            string? userInput = Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not a null)");
            TimeSpan _requiredEffortTimeI = checkTimeSpanFormat(userInput);

            // Prompt user to determine if the task is a milestone
            Console.WriteLine("Does the task have a milestone? (Y/N):");
            userInput = Console.ReadLine()?.Trim().ToUpper(); // Read input and convert to uppercase
            bool _IsMilestone = userInput == "Y";

            // Prompt user for the complexity of the task in the range 0-5
            Console.WriteLine("Enter the complexity of the task? (0-5)");
            userInput = Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not a null)");
            int complexity = getInt(userInput);
            EngineerExperience? _complexity = (EngineerExperience)complexity;

            // Prompt user for planned start date
            Console.WriteLine("Enter the planned start date for the task (e.g., 2024-01-10): ");
            userInput = Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not a null)");
            DateTime? _startDate = CheckDateTimeFormat(userInput);

            // Prompt user for schedule date
            Console.WriteLine("Enter the schedule date for the task (e.g., 2024-01-10):");
            userInput = Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not a null)");
            DateTime? _scheduleDate = CheckDateTimeFormat(userInput);

            // Prompt user for task deadline date
            Console.WriteLine("Enter the task deadline date (e.g., 2024-01-10):");
            userInput = Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not a null)");
            DateTime _deadLineDate = CheckDateTimeFormat(userInput);

            // Prompt user to determine if the task is completed
            Console.WriteLine("Was the task completed? (Y/N)");
            DateTime _completeDatte;
            if (yesOrNo())
            {
                // Prompt user for completion date if the task is completed
                Console.WriteLine("Enter the completion date (e.g., 2024-01-10)");
                userInput = Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not a null)");
                _completeDatte = CheckDateTimeFormat(userInput);
            }
            else
                _completeDatte = DateTime.MinValue;

            // Prompt user for associated deliverables
            Console.WriteLine("Does the task have deliverables associated with it? (Enter Y/N)");
            string? _deliverables;
            if (yesOrNo())
            {
                Console.WriteLine("Enter the deliverables:");
                _deliverables = Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not a null)");
            }
            else
                _deliverables = null;

            // Prompt user for remarks associated with the task
            Console.WriteLine("Does the task have remarks associated withit? (Enter Y/N)");
            string? _remarks;
            if (yesOrNo())
            {
                Console.WriteLine("Enter the remarks:");
                _remarks = Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not a null)");
            }
            else _remarks = null;

            // Prompt user for Engineer ID associated with the task
            Console.WriteLine("Enter the Engineer ID for the task");
            userInput = Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not a null)");
            int _engineerid = getInt(userInput);

            // Create a new Task instance with the provided details
            Task inputTask = new Task(0, _Alias, _Description, _CreatedAtDate, _requiredEffortTimeI,
                _IsMilestone, (EngineerExperience)complexity, _startDate, _scheduleDate, _deadLineDate,
                _completeDatte, _deliverables, _remarks, _engineerid);

            // Call the Create method in the data access layer to store the new task
            s_dalTask!.Create(inputTask);

            // Display the received data
            Console.WriteLine("The data received successfully, here is the Data:");
            Console.WriteLine("At this stage, the task ID is not 0. To see the task ID, type 1 then 4. Don't worry, be happy!");
            PrintTask(inputTask);
        }

        // Method to update an existing task
        private static void updateTask()
        {
            // Prompt user to enter the ID of the task to be updated
            Console.WriteLine("Which task do you want to update? (enter an ID)\n");
            int id = isValidIntInput();

            // Read the existing task data from the data access layer
            Task taskT = s_dalTask!.Read(id) ?? throw new Exception("Task with such ID does not exist");

            // Prompt user to update the Alias field
            updateEngineer_PrintText("Alias");
            bool flag = yesOrNo();
            string alias;
            if (flag)
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
            flag = yesOrNo();
            string description;
            if (flag)
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
            flag = yesOrNo();
            TimeSpan? requiredEffortTime;
            if (flag)
            {
                printFieldForYes("Required effort time");
                requiredEffortTime = checkTimeSpanFormat(Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not a null)"));
            }
            else
            {
                requiredEffortTime = taskT!.RequiredEffortTime;
            }

            // Prompt user to update the Is Milestone field
            updateEngineer_PrintText("Is Milestone");
            bool isMilestone = yesOrNo();

            // Prompt user to update the Complexity field
            updateEngineer_PrintText("Complexity");
            flag = yesOrNo();
            EngineerExperience? complexity;
            if (flag)
            {
                printFieldForYes("Complexity");
                complexity = (EngineerExperience?)getInt(Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not a null)"));
            }
            else
            {
                complexity = taskT!.Complexity;
            }

            // Prompt user to update the Start Date field
            updateEngineer_PrintText("Start Date");
            DateTime? startDate;
            flag = yesOrNo();
            if (flag)
            {
                printFieldForYes("Start Date");
                startDate = CheckDateTimeFormat(Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not a null)"));
            }
            else { startDate = taskT!.StartDate; }

            // Prompt user to update the Schedule Date field
            updateEngineer_PrintText("Schedule Date");
            flag = yesOrNo();
            DateTime? scheduleDate;
            if (flag)
            {
                printFieldForYes("Schedule Date");
                scheduleDate = CheckDateTimeFormat(Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not a null)"));
            }
            else
            {
                scheduleDate = taskT!.ScheduledDate;
            }

            // Prompt user to update the Deadline Date field
            updateEngineer_PrintText("Dead Line Date");
            DateTime? deadLineDate;
            flag = yesOrNo();
            if (flag)
            {
                printFieldForYes("Dead Line Date");
                deadLineDate = CheckDateTimeFormat(Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not a null)"));
            }
            else
            {
                deadLineDate = taskT!.DeadlineDate;
            }

            // Prompt user to update the Complete Date field
            updateEngineer_PrintText("Complete Date");
            flag = yesOrNo();
            DateTime? completeDate;
            if (flag)
            {
                printFieldForYes("Complete Date");
                completeDate = CheckDateTimeFormat(Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not a null)"));
            }
            else
            {
                completeDate = taskT!.CompleteDate;
            }

            // Prompt user to update the Deliverables field
            updateEngineer_PrintText("Deliverables");
            flag = yesOrNo();
            string deliverables;
            if (flag)
            {
                printFieldForYes("Deliverables");
                deliverables = Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not a null)");
            }
            else
            {
                deliverables = taskT!.Deliverables!;
            }

            // Prompt user to update the Remarks field
            updateEngineer_PrintText("Remarks");
            flag = yesOrNo();
            string remarks;
            if (flag)
            {
                printFieldForYes("Remarks");
                remarks = Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not a null)");
            }
            else
            {
                remarks = taskT!.Remarks!;
            }

            // Prompt user to update the Engineer ID field
            updateEngineer_PrintText("Engineer ID");
            int engineerId;
            flag = yesOrNo();
            if (flag)
            {
                printFieldForYes("Engineer ID");
                engineerId = isValidIntInput();
            }
            else
            {
                engineerId = taskT!.EngineerId;
            }
            // Create a new Task instance with the updated details
            Task taskToUpdate = new Task(Id: id, Alias: alias, Description: description, CreatedAtDate: createdAtDate, RequiredEffortTime: requiredEffortTime,
                IsMilestone: isMilestone, Complexity: (EngineerExperience)complexity, StartDate: startDate, ScheduledDate: scheduleDate,
                DeadlineDate: deadLineDate, CompleteDate: completeDate, Deliverables: deliverables,
                Remarks: remarks, EngineerId: engineerId);

            // Call the Update method in the data access layer to apply the changes
            s_dalTask!.Update(taskToUpdate);

            // Display the updated data
            Console.WriteLine("The data received successfully, here is the updated Data:");
            PrintTask(taskToUpdate);
        }

        // Method to read and print details of a specific task based on user input
        private static void readTask()
        {
            Console.WriteLine("Enter the task ID which you want to print:");
            string? userInput = Console.ReadLine() ?? throw new Exception("ERROR: enter a valid input (Not a null)");
            int id = getInt(userInput);

            // Retrieve the task data from the data access layer based on the entered ID
            Task currentTaskData = s_dalTask!.Read(id) ?? throw new Exception("Task with such ID does not exist");

            // Call the PrintTask method to display details of the current task
            PrintTask(currentTaskData);
        }

        // Method to print details of a specific task
        private static void PrintTask(Task taskToPrint)
        {
            Console.WriteLine("Task ID:" + taskToPrint.Id);
            Console.WriteLine("Task Alias:" + taskToPrint.Alias);
            Console.WriteLine("Task Description:" + taskToPrint.Description);
            Console.WriteLine("Task Created at:" + taskToPrint.CreatedAtDate);
            Console.WriteLine("Required time for the task:" + taskToPrint.RequiredEffortTime);
            Console.WriteLine("Does the task have a milestone?:" + taskToPrint.IsMilestone);
            Console.WriteLine("Complexity's task:" + taskToPrint.Complexity);
            Console.WriteLine("The task started at:" + taskToPrint.StartDate);
            Console.WriteLine("Task schedule date:" + taskToPrint.ScheduledDate);
            Console.WriteLine("Dead line task:" + taskToPrint.DeadlineDate);
            Console.WriteLine("Task completed at:" + taskToPrint.CompleteDate);
            Console.WriteLine("Task deliverables:" + taskToPrint.Deliverables);
            Console.WriteLine("Task remarks:" + taskToPrint.Remarks);
            Console.WriteLine("Task Engineer ID:" + taskToPrint.EngineerId);
            Console.WriteLine("\n");
        }

        // Method to read and print details of all tasks
        private static void readAllTask()
        {
            // Retrieve a list of all tasks from the data access layer
            List<Task> tasks = s_dalTask!.ReadAll();

            // Iterate through each task and call the PrintTask method to display details
            foreach (Task task in tasks)
            {
                PrintTask(task!);
            }
        }

        // Method to remove a specific task based on user input
        private static void removeTask()
        {
            Console.WriteLine("Enter the ID of Task you want to remove");
            int id = isValidIntInput();

            // Retrieve the task data from the data access layer based on the entered ID
            Task currentTaskData = s_dalTask!.Read(id) ?? throw new Exception("Task with such ID does not exist");

            // Call the Delete method in the data access layer to remove the task
            s_dalTask!.Delete(id);
        }

        // Method to remove all tasks
        private static void removeAllTasks()
        {
            // Call the DeleteAll method in the data access layer to remove all tasks
            s_dalTask!.DeleteAll();
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
            string message = Console.ReadLine()?.Trim().ToUpper() ?? throw new Exception("Can't enter a null");
            bool _answer = message!.StartsWith('Y');
            return _answer;
        }

    }
}




