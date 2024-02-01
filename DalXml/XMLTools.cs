namespace Dal;

using DO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

static class XMLTools
{
    const string s_xml_dir = @"..\xml\";
    static XMLTools()
    {
        if (!Directory.Exists(s_xml_dir))
            Directory.CreateDirectory(s_xml_dir);
    }

    #region Extension Fuctions
    // Converts the value of the specified XML element to a nullable enum of type T.
    // Returns the nullable enum value if the conversion is successful, otherwise returns null.
    public static T? ToEnumNullable<T>(this XElement element, string name) where T : struct, Enum =>
        Enum.TryParse<T>((string?)element.Element(name), out var result) ? (T?)result : null;

    // Converts the value of the specified XML element to a nullable DateTime.
    // Returns the nullable DateTime value if the conversion is successful, otherwise returns null.
    public static DateTime? ToDateTimeNullable(this XElement element, string name) =>
        DateTime.TryParse((string?)element.Element(name), out var result) ? (DateTime?)result : null;

    // Converts the value of the specified XML element to a nullable double.
    // Returns the nullable double value if the conversion is successful, otherwise returns null.
    public static double? ToDoubleNullable(this XElement element, string name) =>
        double.TryParse((string?)element.Element(name), out var result) ? (double?)result : null;

    // Converts the value of the specified XML element to a nullable int.
    // Returns the nullable int value if the conversion is successful, otherwise returns null.
    public static int? ToIntNullable(this XElement element, string name) =>
        int.TryParse((string?)element.Element(name), out var result) ? (int?)result : null;
    #endregion

    #region XmlConfig
    // Gets the next ID value from the specified XML element, increases it by 1, and saves the updated value back to the XML element.
    // Returns the original next ID value.
    public static int GetAndIncreaseNextId(string data_config_xml, string elemName)
    {
        XElement root = XMLTools.LoadListFromXMLElement(data_config_xml);
        int nextId = root.ToIntNullable(elemName) ?? throw new FormatException($"can't convert id.  {data_config_xml}, {elemName}");
        root.Element(elemName)?.SetValue((nextId + 1).ToString());
        XMLTools.SaveListToXMLElement(root, data_config_xml);
        return nextId;
    }

    // Resets the ID value of the specified XML element to 0 and saves the updated value back to the XML element.
    public static void ResetID(string elemName)
    {
        XElement root = XMLTools.LoadListFromXMLElement("data-config");
        root.Element(elemName)?.SetValue((0).ToString());
        XMLTools.SaveListToXMLElement(root, "data-config");
    }


    #endregion

    #region SaveLoadWithXElement
    // Saves the specified root XElement to an XML file with the given entity name.
    public static void SaveListToXMLElement(XElement rootElem, string entity)
    {
        string filePath = $"{s_xml_dir + entity}.xml";
        try
        {
            rootElem.Save(filePath);
        }
        catch (Exception ex)
        {
            throw new DalXMLFileLoadCreateException($"fail to create xml file: {s_xml_dir + filePath}, {ex.Message}");
        }
    }

    // Loads the XElement from an XML file with the given entity name.
    // If the file does not exist, creates a new XElement with the given entity name and saves it to the XML file.
    // Returns the loaded or created XElement.
    public static XElement LoadListFromXMLElement(string entity)
    {
        string filePath = $"{s_xml_dir + entity}.xml";
        try
        {
            if (File.Exists(filePath))
                return XElement.Load(filePath);
            XElement rootElem = new(entity);
            rootElem.Save(filePath);
            return rootElem;
        }
        catch (Exception ex)
        {
            throw new DalXMLFileLoadCreateException($"fail to load xml file: {s_xml_dir + filePath}, {ex.Message}");
        }
    }
    #endregion

    #region SaveLoadWithXMLSerializer
    // Saves the specified list to an XML file using XML serialization with the given entity name.
    public static void SaveListToXMLSerializer<T>(List<T> list, string entity) where T : class
    {
        string filePath = $"{s_xml_dir + entity}.xml";
        try
        {
            using FileStream file = new(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
            new XmlSerializer(typeof(List<T>)).Serialize(file, list);
        }
        catch (Exception ex)
        {
            throw new DalXMLFileLoadCreateException($"fail to create xml file: {s_xml_dir + filePath}, {ex.Message}");
        }
    }

    // Loads a list from an XML file using XML deserialization with the given entity name.
    // If the file does not exist, returns an empty list.
    // Returns the loaded list.
    public static List<T> LoadListFromXMLSerializer<T>(string entity) where T : class
    {
        string filePath = $"{s_xml_dir + entity}.xml";
        try
        {
            if (!File.Exists(filePath)) return new();
            using FileStream file = new(filePath, FileMode.Open);
            XmlSerializer x = new(typeof(List<T>));
            return x.Deserialize(file) as List<T> ?? new();
        }
        catch (Exception ex)
        {
            throw new DalXMLFileLoadCreateException($"fail to load xml file: {filePath}, {ex.Message}");
        }
    }
    #endregion
}
