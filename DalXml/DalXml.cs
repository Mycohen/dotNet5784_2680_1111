using DalApi;
namespace Dal;

//stage 3
sealed public class DalXml : IDal
{
    // Private static instance variable to hold the single instance of DalXml
    private static readonly DalXml instance = new DalXml();

    // Private constructor to prevent instantiation from outside the class
    private DalXml() { }

    // Public static method to provide access to the single instance of DalXml
    public static DalXml Instance
    {
        get { return instance; }
    }
    // Represents the implementation of the ITask interface.
    public ITask Task => new TaskImplementation();

    // Represents the implementation of the IDependency interface.
    public IDependency Dependency => new DependencyImplementation();

    // Represents the implementation of the IEngineer interface.
    public IEngineer Engineer => new EngineerImplementation();
}
