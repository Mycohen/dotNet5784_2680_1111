namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

// Class implementing IDependency interface
public class DependencyImplementation : IDependency
{
    // Method to find a dependency based on its properties
    public Dependency? FindId(Dependency item)
    {
        foreach (var depItem in DataSource.Dependencies)
        {
            // Check if the properties match
            if (depItem.DependentTask == item.DependentTask && depItem.DependsOnTask == item.DependsOnTask)
                return depItem; // Return the found dependency
        }
        return null; // Return null if no match is found
    }

    // Method to create a new dependency
    public int Create(Dependency item)
    {
        if (FindId(item) != null)
            throw new InvalidOperationException("ERROR!! The dependency already exists");

        if (item.DependentTask == item.DependsOnTask)
            throw new Exception("ERROR: The task cannot depend on itself");

        // Create a new dependency with a new ID
        Dependency newItem = new Dependency(DataSource.Config.NextDepID, item.DependentTask, item.DependsOnTask);
        DataSource.Dependencies.Add(newItem); // Add the new dependency to the collection
        return newItem.Id; // Return the ID of the newly created dependency
    }

    // Method to delete a dependency by ID
    public void Delete(int id)
    {
        Dependency? deletedItem = Read(id); // Read the dependency with the given ID
        if (deletedItem == null) throw new InvalidOperationException("ERROR: There is no such dependency");
        DataSource.Dependencies.Remove(deletedItem); // Remove the dependency from the collection
    }

    // Method to read a dependency by ID
    public Dependency? Read(int id)
    {
        foreach (var depElement in DataSource.Dependencies)
        {
            if (depElement.Id == id) return depElement; // Return the dependency if ID matches
        }
        return null; // Return null if no match is found
    }

    // Method to read all dependencies
    public List<Dependency> ReadAll()
    {
        return new List<Dependency>(DataSource.Dependencies); // Return a copy of the dependencies collection
    }

    // Method to update a dependency
    public void Update(Dependency item)
    {
        Dependency? deletedItem = Read(item.Id);
        if (deletedItem == null) throw new InvalidOperationException("ERROR: There is no such dependency");
        DataSource.Dependencies.Remove(deletedItem); // Remove the existing dependency
        DataSource.Dependencies.Add(item); // Add the updated dependency
    }
}
