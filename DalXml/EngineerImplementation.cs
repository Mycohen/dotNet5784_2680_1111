namespace Dal;
using DalApi;
using DO;
using System.Data.Common;
using System.Linq;
using System.Xml.Linq;

internal class EngineerImplementation : IEngineer
{
    readonly string s_engineer_xml = "engineer";

    // Creates a new Engineer in the XML file
    public int Create(Engineer item)
    {
        // Check if Engineer with the same ID already exists
        if (Read(item.Id) != null)
            throw new DalAlreadyExistsException($"Engineer with ID={item.Id} already exists");

        // Create a new XElement for the Engineer and populate it with the item's properties
        XElement elemEngineer = new XElement("Engineer",
            new XElement("Id", item.Id),
            new XElement("Name", item.Name),
            new XElement("Email", item.Email),
            new XElement("Cost", item.Cost),
            new XElement("Level", item.Level));

        // Save the XElement to the XML file
        XMLTools.SaveListToXMLElement(elemEngineer, s_engineer_xml);

        // Return the ID of the created Engineer
        return item.Id;
    }

    // Reads an Engineer from the XML file based on the ID
    public Engineer? Read(int id)
    {
        // Load the XElement containing all Engineers from the XML file
        XElement engineerElement = XMLTools.LoadListFromXMLElement(s_engineer_xml);

        // Find the target Engineer XElement based on the ID
        XElement? targetEngineerElement = engineerElement.Elements("Engineer")
            .FirstOrDefault(elem => (int)elem.Element("Id")! == id);

        // If the target Engineer XElement exists, create a new Engineer object and populate it with the XElement's properties
        if (targetEngineerElement != null)
        {
            Engineer engineer = new Engineer
            {
                Id = (int)targetEngineerElement.Element("Id")!,
                Name = (string)targetEngineerElement.Element("Name")!,
                Email = (string)targetEngineerElement.Element("Email")!,
                Cost = (double)targetEngineerElement.Element("Cost")!,
                Level = (DO.EngineerExperience)(int)targetEngineerElement.Element("Level")!
            };

            // Return the created Engineer object
            return engineer;
        }

        // If the target Engineer XElement doesn't exist, return null
        return null;
    }

    // Reads an Engineer from the XML file based on a filter function
    public Engineer? Read(Func<Engineer, bool> filter)
    {

        // Load all Engineers from the XML file
        List<Engineer> engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineer_xml);

        // Find the first Engineer that matches the filter function
        return engineers.FirstOrDefault(filter);
    }

    // Reads all Engineers from the XML file, optionally filtered by a filter function
    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
    {
        // If no filter function is provided, return all Engineers
        if (filter == null)
            return XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineer_xml).Select(item => item);
        // If a filter function is provided, return the Engineers that match the filter
        else
            return XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineer_xml).Where(filter);
    }

    // Updates an Engineer in the XML file
    public void Update(Engineer item)
    {
        // Check if the Engineer exists
        chechIfEngineerExist(item);

        // Delete the existing Engineer
        Delete(item.Id);

        // Create the updated Engineer
        Create(item);
    }

    // Deletes an Engineer from the XML file based on the ID
    public void Delete(int id)
    {
        // Check if the Engineer exists
        chechIfEngineerExist(Read(id)!);

        // Load the XElement containing all Engineers from the XML file
        XElement engineerElement = XMLTools.LoadListFromXMLElement(s_engineer_xml);

        // Find the target Engineer XElement based on the ID
        XElement? targetEngineerElement = engineerElement
                .Elements("Engineer")
                .FirstOrDefault(elem => (int)elem.Element("Id")! == id);

        // Remove the target Engineer XElement
        targetEngineerElement!.Remove();

        // Save the modified XElement back to the XML file
        XMLTools.SaveListToXMLElement(engineerElement, s_engineer_xml);
    }

    // Deletes all Engineers from the XML file
    public void DeleteAll()
    {
        // Load the XElement containing all Engineers from the XML file
        XElement engineers = XMLTools.LoadListFromXMLElement(s_engineer_xml);

        // Remove all Engineer XElements
        engineers.RemoveAll();

        // Save the modified XElement back to the XML file
        XMLTools.SaveListToXMLElement(engineers, s_engineer_xml);
    }

    // Checks if an Engineer exists based on the ID and throws an exception if it doesn't
    void chechIfEngineerExist(Engineer item)
    {
        if (Read(item.Id) == null)
            throw new DalDoesNotExistExeption($"Engineer with ID={item.Id} doesn't exist");
    }
}

