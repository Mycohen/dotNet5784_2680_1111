namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

// Class implementing IEngineer interface
internal class EngineerImplementation : IEngineer
{
    // Method to create a new engineer
    public int Create(Engineer item)
    {
        // Create a new engineer with specified properties
        Engineer newEngineer = new Engineer(item.Id, item.Email, item.Cost, item.Name, item.Level);

        // Check if an engineer with the same ID already exists
        foreach (var _engineer in DataSource.Engineers)
        {

            if (_engineer.Id == item.Id)
                throw new DalAlreadyExistsException($"Engineer with ID={item.Id} already exist");

        }
        if (item.Id < 0 || item.Email == null || item.Email.Length == 0 || item.Cost <= 0 ||
            item.Name == null || item.Name.Length == 0)
            throw new Exception("The Engineer properties are invalid");

        // Add the new engineer to the collection
        DataSource.Engineers.Add(newEngineer);

        // Return the ID of the newly created engineer
        return newEngineer.Id;
    }

    // Method to delete an engineer by ID
    public void Delete(int id)
    {
        // Read the engineer with the given ID
        Engineer? deletedEngineer = Read(id);

        // Throw an exception if the engineer doesn't exist
        if (deletedEngineer == null)
            throw new DalDoesNotExistExeption($"Engineer with ID={id} doesn't exist");

        // Remove the engineer from the collection
        DataSource.Engineers.Remove(deletedEngineer);
    }

    // Method to read an engineer by ID
    public Engineer? Read(int id)
    {
        // Use LINQ to find the engineer with the given ID
        Engineer? result = DataSource.Engineers.FirstOrDefault(x => x.Id == id);

        // Return the engineer if found, otherwise return null
        return result;
    }
    public Engineer? Read(Func<Engineer, bool> filter)
    {
        return DataSource.Engineers.FirstOrDefault(filter);

    }


// Method to read all engineers
public IEnumerable<Engineer> ReadAll(Func<Engineer, bool>? filter = null)
    {
        //if there is no parameter of a point to a function
        if (filter == null)
            return DataSource.Engineers.Select(item => item);
        //filter based on the boolian function
        else
            return DataSource.Engineers.Where(filter);
    }

    // Method to update an engineer
    public void Update(Engineer item)
    {
        // Read the engineer with the given ID
        Engineer? deletedEngineer = Read(item.Id);

        // Throw an exception if the engineer doesn't exist
        if (deletedEngineer == null)
            throw new DalDoesNotExistExeption($"Engineer with ID={item.Id} doesn't exist");

        // Remove the existing engineer
        DataSource.Engineers.Remove(deletedEngineer);

        // Add the updated engineer
        DataSource.Engineers.Add(item);
    }
    //method to delete all the Engineers
    public void DeleteAll()
    {
        DataSource.Engineers.Clear();
    }

}
