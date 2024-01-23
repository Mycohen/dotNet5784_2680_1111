namespace Dal;
using DalApi;
using DO;
using System.Data.Common;
using System.Xml.Linq;

internal class EngineerImplementation: IEngineer
{
    readonly string s_engineer_xml = "engineer";

    public void DeleteAll()
    {
        throw new NotImplementedException();
    }

    public int Create(Engineer item)
    {
        if(Read(item.Id)!= null)
            throw new DalAlreadyExistsException($"Engineer with ID={item.Id} already exist");

        XElement elemEngineer = new XElement("Engineer",
            new XElement("Id", item.Id),
            new XElement("Name", item.Name),
            new XElement("Email", item.Email),
            new XElement("Cost", item.Cost)
            , new XElement("Level", item.Level));
        XMLTools.SaveListToXMLElement(elemEngineer, s_engineer_xml);
        return item.Id;

  
    }

    public Engineer? Read(int id)
    {
        XElement engineerElement = XMLTools.LoadListFromXMLElement(s_engineer_xml);
        XElement ? targetEngineerElement = engineerElement.Elements("Engineer")
            .FirstOrDefault(elem => (int)elem.Element("Id")! == id);

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

            return engineer;
        }

        return null;
    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        XElement engineerElement = XMLTools.LoadListFromXMLElement(s_engineer_xml);
        XElement? targetEngineerElement = engineerElement.Elements("Engineer")
            .FirstOrDefault(filter);
    }

    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public void Update(Engineer item)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
}
