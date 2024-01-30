namespace Dal;
using DalApi;
using DO;
using System.Xml.Linq;

internal class DependencyImplementation : IDependency
{
    readonly string s_dependency_xml = "dependency";

    // Creates a new Engineer in the XML file
    public int Create(Dependency item)
    {
        XElement dependencyArrayRoot = XMLTools.LoadListFromXMLElement(s_dependency_xml);
        //create an instance of task (converted to XML)
        XElement elementDependency = new XElement("Task",
                new XElement("Id", Config.NextDepId,
                new XElement("DependentTask", item.DependentTask),
                new XElement("DependsOnTask", item.DependsOnTask)));
        dependencyArrayRoot.Add(elementDependency);
        XMLTools.SaveListToXMLElement(dependencyArrayRoot, s_dependency_xml);
        return (int)XMLTools.ToIntNullable(elementDependency, "Id")!;
    }

    // Reads an Engineer from the XML file based on the ID
    public Dependency? Read(int id)
    {
        // Load the XElement containing all Engineers from the XML file
        XElement dependencyRoot = XMLTools.LoadListFromXMLElement(s_dependency_xml);

        // Find the target Engineer XElement based on the ID
        XElement? targetDependencyChild = dependencyRoot.Elements("Dependency")
            .FirstOrDefault(elem => (int)elem.Element("Id")! == id);

        // If the target Engineer XElement exists, create a new Engineer object and populate it with the XElement's properties
        if (targetDependencyChild != null)
        {
            Dependency dependencyFound = new Dependency
            {
                Id = (int)targetDependencyChild.Element("Id")!,
                DependentTask = (int)targetDependencyChild.Element("DependentTask")!,
                DependsOnTask = (int)targetDependencyChild.Element("DependsOnTask")!
            };
            // Return the created Engineer object
            return dependencyFound;
        }

        // If the target Engineer XElement doesn't exist, return null
        return null;
    }

    // Reads an Engineer from the XML file based on a filter function
    public Dependency? Read(Func<Dependency, bool> filter)
    {

        // Load all Engineers from the XML file
        List<Dependency> dependencies = XMLTools.LoadListFromXMLSerializer<Dependency>(s_dependency_xml);

        // Find the first Engineer that matches the filter function
        return dependencies.FirstOrDefault(filter);
    }

    // Reads all Engineers from the XML file, optionally filtered by a filter function
    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null)
    {
        // If no filter function is provided, return all Engineers
        if (filter == null)
            return XMLTools.LoadListFromXMLSerializer<Dependency>(s_dependency_xml).Select(item => item);
        // If a filter function is provided, return the Engineers that match the filter
        else
            return XMLTools.LoadListFromXMLSerializer<Dependency>(s_dependency_xml).Where(filter);
    }

    // Updates an Engineer in the XML file
    public void Update(Dependency item)
    {
        chechIfDependencyExist(Read(item.Id)!);

        XElement dependencyArrayRoot = XMLTools.LoadListFromXMLElement(s_dependency_xml);
        //create an instance of task (converted to XML)
        XElement elementDependency = new XElement("Task",
                new XElement("Id", item.Id,
                new XElement("DependentTask", item.DependentTask),
                new XElement("DependsOnTask", item.DependsOnTask)));
        Delete(item.Id);
        dependencyArrayRoot.Add(elementDependency);

        XMLTools.SaveListToXMLElement(dependencyArrayRoot, s_dependency_xml);
    }

    // Deletes an Engineer from the XML file based on the ID
    public void Delete(int id)
    {
        // Check if the Engineer exists
        chechIfDependencyExist(Read(id)!);

        // Load the XElement containing all Engineers from the XML file
        XElement dependencyRoot = XMLTools.LoadListFromXMLElement(s_dependency_xml);

        // Find the target Engineer XElement based on the ID
        XElement? targetDependencyElement = dependencyRoot
                .Elements("Dependency")
                .FirstOrDefault(elem => (int)elem.Element("Id")! == id);

        // Remove the target Engineer XElement
        targetDependencyElement!.Remove();

        // Save the modified XElement back to the XML file
        XMLTools.SaveListToXMLElement(dependencyRoot, s_dependency_xml);
    }

    // Deletes all Engineers from the XML file
    public void DeleteAll()
    {
        // Load the XElement containing all Engineers from the XML file
        XElement dependencies = XMLTools.LoadListFromXMLElement(s_dependency_xml);

        // Remove all Engineer XElements
        dependencies.RemoveAll();

        // Save the modified XElement back to the XML file
        XMLTools.SaveListToXMLElement(dependencies, s_dependency_xml);
        XMLTools.ResetID("NextDepId");
    }

    // Checks if an Engineer exists based on the ID and throws an exception if it doesn't
    void chechIfDependencyExist(Dependency item)
    {
        if (Read(item.Id) == null)
            throw new DalDoesNotExistExeption($"Dependency with ID={item.Id} doesn't exist");
    }
}




