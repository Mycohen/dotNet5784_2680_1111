namespace DelTest;
using DalApi;
using DO;
using System;
using System.Data.Common;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

// Static class responsible for initializing test data
public static class Initialization
{
    // Constants defining the range of task IDs
    private static int MIN_ID = 200000000;
    private static int MAX_ID = 400000000;

    // DAL interfaces for dependencies, engineers, and tasks
    private static IDependency? s_dalDependency;
    private static IEngineer? s_dalEngineer;
    private static ITask? s_dalTask;

    // Random number generator for creating random data
    private static readonly Random s_rand = new();

    // Array to store generated task IDs
    private static int[] _ids = new int[10];

    // Method to generate unique task IDs and store them in the '_ids' array
    private static void setIds()
    {
        bool alredyExist;
        for (int i = 0; i < 10; i++)
        {
            alredyExist = false;
            do
            {
                _ids[i] = s_rand.Next(MIN_ID, MAX_ID);
                foreach (var element in _ids)
                {
                    if (_ids[i] == element)
                    {
                        alredyExist = true;
                    }
                }
            } while (alredyExist);
        }
    }

    // Arrays containing predefined data for tasks, descriptions, etc.
    private static string[] tasksAlias = {
            "Structural Analysis",
            "CAD Modeling",
            "Prototyping",
            "Electrical Circuit Design",
            "Software Development",
            "Project Management",
            "Material Testing",
            "Quality Assurance",
            "Fluid Dynamics Analysis",
            "Environmental Impact Assessment",
            "Robotics Programming",
            "Thermal Analysis",
            "Network Design",
            "Optimization Modeling",
            "Instrumentation Calibration",
            "Geotechnical Engineering",
            "Energy Efficiency Analysis",
            "Risk Assessment",
            "Finite Element Analysis (FEA)",
            "Aerospace Systems Design",
            "Machine Learning Integration",
            "Automated Manufacturing",
            "Telecommunication System Design",
            "Hydraulic System Design",
            "Fire Protection Engineering",
            "Renewable Energy System Optimization",
            "Human Factors Engineering",
            "Structural Health Monitoring",
            "Water Resource Management",
            "Simulations and Modeling"
    };

    private static string[] taskDescription = {
            "Evaluate the integrity and stability of structures through mathematical modeling and analysis.",
            "Create detailed computer-aided design (CAD) models for various engineering projects.",
            "Build physical prototypes to test and validate design concepts before full-scale production.",
            "Develop schematics and layouts for electrical circuits, considering safety and efficiency.",
            "Write, test, and maintain software code for controlling and monitoring engineering systems.",
            "Plan, execute, and oversee engineering projects, ensuring they meet deadlines and budget constraints.",
            "Conduct experiments to analyze the properties and behavior of materials under different conditions.",
            "Implement and maintain quality control processes to ensure products meet specified standards.",
            "Study the flow and behavior of fluids to optimize systems such as pipelines and hydraulic systems.",
            "Evaluate and minimize the environmental impact of engineering projects.",
            "Develop and program algorithms for robotic systems to perform specific tasks.",
            "Assess and optimize the thermal performance of devices and systems.",
            "Plan and implement data communication networks for efficient information transfer.",
            "Apply mathematical models to optimize processes and resource utilization.",
            "Calibrate and maintain measurement instruments for accurate data collection.",
            "Study soil and rock mechanics to provide recommendations for construction projects.",
            "Evaluate and improve the energy efficiency of systems and processes.",
            "Identify and assess potential risks associated with engineering projects.",
            "Use FEA software to simulate and analyze structural and mechanical behavior.",
            "Design components and systems for aircraft and spacecraft.",
            "Implement machine learning algorithms to enhance engineering processes and decision-making.",
            "Design and implement automated systems for manufacturing processes.",
            "Plan and design communication systems for efficient data transfer.",
            "Develop and optimize hydraulic systems for various applications.",
            "Design systems to prevent and control fires in buildings and facilities.",
            "Optimize the efficiency of renewable energy systems such as solar and wind power.",
            "Consider human capabilities and limitations in the design of products and systems.",
            "Implement systems to monitor and assess the health of structures over time.",
            "Plan and manage water resources for agricultural, industrial, and municipal use.",
            "Create computer simulations to model and analyze complex engineering systems and scenarios."
    };
    private static string[] deliverablesDescription = {
                "Consider collaboration with other teams.",
                "Critical task, handle with care.",
                "Check for legal compliance during execution.",
                "Coordinate with client for specific requirements.",
                "Ensure safety protocols are followed.",
                "Keep documentation up-to-date.",
                "Task requires specialized equipment.",
                "Verify resource availability before starting.",
                "Consider potential risks and mitigation strategies.",
                "Collaborate with stakeholders for input.",
                "Task may impact project timeline.",
                "Consider scalability for future enhancements.",
                "Validate results through thorough testing.",
                "Review and adhere to coding standards.",
                "Task involves confidential information.",
                "Check for dependencies on other tasks.",
                "Verify compatibility with existing systems.",
                "Collaborate with QA for testing procedures.",
                "Communicate progress to project manager.",
                "Task completion critical for project milestone."
    };
    private static int[] taskLevelsArray = {
            4, // "Structural Analysis"
            3, // "CAD Modeling"
            2, // "Prototyping"
            4, // "Electrical Circuit Design"
            5, // "Software Development"
            4, // "Project Management"
            3, // "Material Testing"
            4, // "Quality Assurance"
            4, // "Fluid Dynamics Analysis"
            3, // "Environmental Impact Assessment"
            5, // "Robotics Programming"
            4, // "Thermal Analysis"
            4, // "Network Design"
            3, // "Optimization Modeling"
            2, // "Instrumentation Calibration"
            4, // "Geotechnical Engineering"
            3, // "Energy Efficiency Analysis"
            4, // "Risk Assessment"
            4, // "Finite Element Analysis (FEA)"
            5, // "Aerospace Systems Design"
            5, // "Machine Learning Integration"
            3, // "Automated Manufacturing"
            4, // "Telecommunication System Design"
            3, // "Hydraulic System Design"
            4, // "Fire Protection Engineering"
            4, // "Renewable Energy System Optimization"
            3, // "Human Factors Engineering"
            4, // "Structural Health Monitoring"
            3, // "Water Resource Management"
            4  // "Simulations and Modeling"

    };
    private static string[] additionalRemarks = {
            "Task completed successfully.",
            "Encountered unexpected challenges but resolved them efficiently.",
            "Completed ahead of schedule.",
            "Collaborated with team members to overcome obstacles.",
            "Implemented innovative solutions for improved efficiency.",
            "Verified and tested thoroughly to ensure high-quality results.",
            "Documented the process for future reference.",
            "Coordinated with other departments for seamless integration.",
            "Performed rigorous testing to identify and fix potential issues.",
            "Task required additional resources due to complexity.",
            "Customer feedback was positive.",
            "Implemented security measures to enhance system integrity.",
            "Task involved cross-functional collaboration.",
            "Provided detailed documentation for easy maintenance.",
            "Implemented feedback from the quality assurance team.",
            "Explored alternative approaches to optimize performance.",
            "Task required close communication with stakeholders.",
            "Addressed and resolved performance bottlenecks.",
            "Task involved integration with third-party systems.",
            "Suggested improvements for future similar tasks."
    };
    private static string[] engineerNames = {
            "Yael Cohen",
            "Eitan Levi",
            "Maya Ben-David",
            "Itai Avraham",
            "Tamar Schwartz",
            "Emma Wilson",
            "Daniel Taylor",
            "Ava Anderson",
            "Alexander White",
            "Sophia Harris"
    };
    private static int[] tasksDependencies = {
        // Example of task dependencies
        0, 1,   // Structural Analysis depends on CAD Modeling
        2, 1,   // Prototyping depends on CAD Modeling
        3, 2,   // Electrical Circuit Design depends on Prototyping
        4, 3,   // Software Development depends on Electrical Circuit Design
        5, 4,   // Project Management depends on Software Development
        6, 5,   // Material Testing depends on Project Management
        7, 6,   // Quality Assurance depends on Material Testing
        8, 7,   // Fluid Dynamics Analysis depends on Quality Assurance
        9, 8,   // Environmental Impact Assessment depends on Fluid Dynamics Analysis
        10, 4,  // Robotics Programming depends on Software Development
        11, 10, // Thermal Analysis depends on Robotics Programming
        12, 11, // Network Design depends on Thermal Analysis
        13, 12, // Optimization Modeling depends on Network Design
        14, 13, // Instrumentation Calibration depends on Optimization Modeling
        15, 14, // Geotechnical Engineering depends on Instrumentation Calibration
        16, 15, // Energy Efficiency Analysis depends on Geotechnical Engineering
        17, 16, // Risk Assessment depends on Energy Efficiency Analysis
        18, 17, // Finite Element Analysis (FEA) depends on Risk Assessment
        19, 18, // Aerospace Systems Design depends on Finite Element Analysis (FEA)
        20, 4,  // Machine Learning Integration depends on Software Development
        21, 20, // Automated Manufacturing depends on Machine Learning Integration
        22, 21, // Telecommunication System Design depends on Automated Manufacturing
        23, 22, // Hydraulic System Design depends on Telecommunication System Design
        24, 23, // Fire Protection Engineering depends on Hydraulic System Design
        25, 24, // Renewable Energy System Optimization depends on Fire Protection Engineering
        26, 25, // Human Factors Engineering depends on Renewable Energy System Optimization
        27, 26, // Structural Health Monitoring depends on Human Factors Engineering
        28, 27, // Water Resource Management depends on Structural Health Monitoring
        29, 28  // Simulations and Modeling depends on Water Resource Management
    };
    private static string[] engineerMails = {
    "yael.cohen@example.com",
    "eitan.levi@example.com",
    "maya.ben-david@example.com",
    "itai.avraham@example.com",
    "tamar.schwartz@example.com",
    "emma.wilson@example.com",
    "daniel.taylor@example.com",
    "ava.anderson@example.com",
    "alexander.white@example.com",
    "sophia.harris@example.com"
};
    private static int[] costs = new int[10];

    // Method to create tasks with predefined data
    private static void CreateTask()
    {
        int index = 0;
        int randomIndexId = s_rand.Next(0, 9);
        foreach (var _taskName in tasksAlias)
        {
            // Retrieve data from predefined arrays
            string _Alias = _taskName;
            string _Description = taskDescription[index];
            DateTime _CreatedAtDate = DateTime.Now.AddDays(index).AddHours(index * 2);
            TimeSpan _RequierdEffortTime = TimeSpan.FromDays(s_rand.Next(1, 15));
            bool _IsMileStone = (_ids[randomIndexId] % 2 == 0) ? true : false;
            EngineerExperience _Complexity = (EngineerExperience)taskLevelsArray[index];
            DateTime _StartDate = _CreatedAtDate.Add(_RequierdEffortTime).AddDays(s_rand.Next(0, 5));
            DateTime _ScheduledDate = _StartDate.Add(_RequierdEffortTime);
            DateTime _DeadLineDate = _ScheduledDate.AddDays(s_rand.Next(7, 14));
            DateTime _CompleteDate = _StartDate.AddDays(s_rand.Next(0, 40));
            string _Remarks = (_ids[randomIndexId] % 4 == 0) ? additionalRemarks[index] : string.Empty;
            string _deliverables = (_ids[randomIndexId] % 2 == 0) ? deliverablesDescription[index] : string.Empty;

            // Create a new task using the Task constructor
            Task newTask = new Task(0, _Alias, _Description, _CreatedAtDate, _RequierdEffortTime,
               _IsMileStone, _Complexity, _StartDate, _ScheduledDate, _DeadLineDate, _CompleteDate,
               _deliverables, _Remarks, _ids[randomIndexId]);

            // Call the DAL to create the task
            s_dalTask!.Create(newTask);
            index++;
        }
    }

    // Method to create dependencies based on predefined data
    private static void createDependency()
    {
        for (int i = 0; i < 59; i += 2)
        {
            int _dependentTask = tasksDependencies[i];
            int _dependsOnTask = tasksDependencies[i + 1];

            // Create a new dependency using the Dependency constructor
            Dependency newDependency = new Dependency(_dependentTask, _dependsOnTask);

            // Call the DAL to create the dependency
            s_dalDependency!.Create(newDependency);
        }
    }

    // Method to create engineers with predefined data
    private static void CreateEngineer()
    {
        int index = 0;
        foreach (var engineerName in engineerNames)
        {
            // Retrieve data from predefined arrays
            int _Id = _ids[index];
            string _Email = engineerMails[index];
            int _Cost = s_rand.Next(5000, 35000);
            string _Name = engineerNames[index];

            // Logical error: _Level variable is created but not used
            EngineerExperience _Level = (EngineerExperience)s_rand.Next(0, 4);

            // Create a new engineer using the Engineer constructor
            Engineer newEnginner = new Engineer(_Id, _Email, _Cost, _Name, _Level);

            // Call the DAL to create the engineer
            s_dalEngineer!.Create(newEnginner);
            index++;
        }
    }

    // Main method to initiate the data creation process
    public static void Do(IDependency? dalDependency, IEngineer? dalEngineer, ITask? dalTask)
    {
        // Assign DAL instances to local variables
        s_dalTask = dalTask ?? throw new NullReferenceException("DAL can not be null!");
        s_dalEngineer = dalEngineer ?? throw new NullReferenceException("DAL can not be null!");
        s_dalDependency = dalDependency ?? throw new NullReferenceException("DAL can not be null!");

        // Create tasks, engineers, and dependencies
        CreateTask();
        CreateEngineer();
        createDependency();
    }
}
