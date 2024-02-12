using DalApi;
using System.Diagnostics;
namespace Dal;

//stage 3
sealed public class DalXml : IDal
{
    // Private static instance variable to hold the single instance of DalXml
    /*private static readonly DalXml instance = new DalXml();*/

    //Lazy implemen
    private static DalXml? instance = null;

    private static object objLock = new object();
    // Private constructor to prevent instantiation from outside the class
    private DalXml() { }

    // Public static method to provide access to the single instance of DalXml
    public static DalXml Instance
    {
        get
        {
            if (instance == null)
            {
                lock (objLock)
                {
                    if (instance == null)
                    {
                        instance = new DalXml();
                    }
                }
            }
            return instance;
        }
    }
    // Represents the implementation of the ITask interface.
    public ITask Task => new TaskImplementation();

    // Represents the implementation of the IDependency interface.
    public IDependency Dependency => new DependencyImplementation();

    // Represents the implementation of the IEngineer interface.
    public IEngineer Engineer => new EngineerImplementation();
}
