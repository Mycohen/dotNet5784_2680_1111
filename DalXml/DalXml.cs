using DalApi;
namespace Dal;

//stage 3
sealed public class DalXml : IDal
{
    // Represents the implementation of the ITask interface.
    public ITask Task => new TaskImplementation();

    // Represents the implementation of the IDependency interface.
    public IDependency Dependency => new DependencyImplementation();

    // Represents the implementation of the IEngineer interface.
    public IEngineer Engineer => new EngineerImplementation();
}
