namespace Dal;
using DalApi;

sealed internal class DalList : IDal
{
    //singalton implementation 

    /*public static IDal Instance { get; } = new DalList();
    private DalList() { }*/

    // singalton - lazy implementation.

    // Declares a static variable to hold the single instance of the class.
    // Initially, it's null since the object hasn't been created yet.
    private static DalList? instance = null;

    //  static object for synchronization.
    //  This object will be used to control access to the instance creation process.
    private static object lockObject = new object();

    //Defines a private constructor to prevent direct instantiation of the class outside.
    //Only the Instance property can create the object.
    private DalList() { }
    // lazy initialization with double-checking that insures tread safety

    public static DalList Instance
    {
        get
        {
            if (instance == null)
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new DalList();
                    }
                }
            }
            return instance;
        }
    }


    public IDependency Dependency => new DependencyImplementation();
    public IEngineer Engineer => new EngineerImplementation();
    public ITask Task => new TaskImplementation(); 
}


