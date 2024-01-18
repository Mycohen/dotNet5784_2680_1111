namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Linq;

// Class implementing IDependency interface
internal class DependencyImplementation : IDependency
{


    // Method to find a dependency based on its properties
    static public Dependency? FindId(Dependency item)
    {
        return DataSource.Dependencies
            .FirstOrDefault(depItem =>
                depItem.DependentTask == item.DependentTask && depItem.DependsOnTask == item.DependsOnTask);
    }


    // Method to create a new dependency
    public int Create(Dependency item)
    {
        //do not check for the first dependency (all of the dependencis are initialize by ID of 0)
        if (DataSource.Dependencies.Count != 0)
        {
            Dependency? res = FindId(item);
            if (res != null)
                throw new DalDoesNotExistExeption($"Dependency with ID = {res.Id} already exist");

        }
        if (item.DependentTask == item.DependsOnTask)
            throw new Exception("ERROR: The task cannot depend on itself");


        // Create a new dependency with a new ID
        Dependency newItem = new Dependency(DataSource.Config.NextDepID, item.DependentTask, item.DependsOnTask);
        DataSource.Dependencies.Add(newItem); // Add the new dependency to the collection

        return newItem.Id;
    }

    // Method to delete a dependency by ID
    public void Delete(int id)
    {
        Dependency? deletedItem = Read(id); // Read the dependency with the given ID
        if (deletedItem == null) new DalDoesNotExistExeption("ERROR: There is no such dependency");
        DataSource.Dependencies.Remove(deletedItem!); // Remove the dependency from the collection
    }

    // Method to read a dependency by ID
    public Dependency? Read(int id)
    {
        return DataSource.Dependencies.FirstOrDefault(d => d.Id == id);
    }
    // Method to read a dependency by soem Boolian function
    public Dependency? Read(Func<Dependency, bool> filter) // stage 2
    {
        // Return the Dependency element if found. Else, return null
        return DataSource.Dependencies.FirstOrDefault(filter);
    }

    // Method to read all tasks
    public IEnumerable<Dependency?> ReadAll(Func<Dependency?, bool>? filter = null) //stage 2
    {
        if (filter == null)
            return DataSource.Dependencies.Select(item => item);
        else
            return DataSource.Dependencies.Where(filter);
    }

    // Method to update a dependency
    public void Update(Dependency item)
    {
        Dependency? deletedItem = Read(item.Id);
        if (deletedItem == null) throw new DalDoesNotExistExeption($"Dependency with ID={item.Id} doesn't exist");
        if (item.DependentTask == item.DependsOnTask)
            throw new Exception("ERROR: The task cannot depend on itself");
        DataSource.Dependencies.Remove(deletedItem); // Remove the existing dependency
        DataSource.Dependencies.Add(item); // Add the updated dependency
    }
    // Method to delete all the dependencies
    public void DeleteAll()
    {
        if (!DataSource.Dependencies.Any())
            throw new DalDeletionImpossible();
        else
        DataSource.Dependencies.Clear();
    }

   
}
