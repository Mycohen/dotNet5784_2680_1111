using DalApi;
using DO;
using System.Xml.Linq;

namespace Dal;

internal class EngineerImplementation : IEngineer
{
    readonly string s_engineer_xml = "engineer";

    // Method to create a new engineer
    public int Create(Engineer item)
    {
        // Create a new engineer with specified properties
        Engineer newEngineer = new Engineer(item.Id, item.Email, item.Cost, item.Name, item.Level);

        if (Read(item.Id) != null)
            throw new DalAlreadyExistsException($"Engineer with ID={item.Id} already exist");

        XElement elemEngineer = new XElement("Engineer", 
            new XElement("Id", item.Id),  
            new XElement("Email", item.Email),
            new XElement("Cost", item.Cost),
            new XElement("Name", item.Name),
            new XElement("Level", item.Level)
            );

        XMLTools.SaveListToXMLElement(elemEngineer, "engineer");

        // Return the ID of the newly created engineer
        return newEngineer.Id;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public void DeleteAll()
    {
        throw new NotImplementedException();
    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        throw new NotImplementedException();
    }

    public Engineer? Read(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public void Update(Engineer item)
    {
        throw new NotImplementedException();
    }
}
