using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using System.Xml.Linq;
using DO;
using DalApi;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Data;

internal class TaskImplementation : ITask
{
    static readonly string s_task_xml = "task";
    internal XElement taskArrayRoot = XMLTools.LoadListFromXMLElement(s_task_xml);

    
    public int Create(Task item)
    {
        XElement taskArrayRoot = XMLTools.LoadListFromXMLElement(s_task_xml);
        //create an instance of task (converted to XML)
        XElement elementTask = new XElement("Task", 
                new XElement("Id", Config.NextTaskId,
                new XElement("Alias", item.Alias),
                new XElement("Description", item.Description),
                new XElement("CreatedAtDate", item.CreatedAtDate),
                new XElement("IsMilestone", item.IsMilestone),
                new XElement("Complexity", item.Complexity),
                new XElement("StartDate", item.StartDate),
                new XElement("ScheduledDate", item.ScheduledDate),
                new XElement("DeadlineDate", item.DeadlineDate),
                new XElement("CompleteDate", item.CompleteDate),
                (item.Deliverables != null)? new XElement("Deliverables", item.Deliverables): null,
                (item.Remarks != null)? new XElement("Remarks", item.Remarks) : null,
                new XElement("EngineerId", item.EngineerId)
                ));
        taskArrayRoot.Add(elementTask);
        XMLTools.SaveListToXMLElement(taskArrayRoot, s_task_xml);
        return (int)XMLTools.ToIntNullable(elementTask, "Id")!;
    }

    public void Delete(int id)
    {
        // Check if the Engineer exists
        chechIfTaskExist(Read(id)!);

        // Load the XElement containing all Tasks from the XML file
        XElement rootTaskElement = XMLTools.LoadListFromXMLElement(s_task_xml);

        // Find the target Tasks XElement based on the ID
        XElement? targetTaskElement = rootTaskElement
                .Elements("Task")
                .FirstOrDefault(elem => (int)elem.Element("Id")! == id);

        // Remove the target Engineer XElement
        targetTaskElement!.Remove();

        // Save the modified XElement back to the XML file
        XMLTools.SaveListToXMLElement(rootTaskElement, s_task_xml);
    }

    // Deletes all Task from the XML file
    public void DeleteAll()
    {
        // Load the XElement containing all Engineers from the XML file
        XElement xmlTask = XMLTools.LoadListFromXMLElement(s_task_xml);

        // Remove all Engineer XElements
        xmlTask.RemoveAll();

        // Save the modified XElement back to the XML file
        XMLTools.SaveListToXMLElement(xmlTask, s_task_xml);
    }

    public Task? Read(Func<Task, bool> filter)
    {
        Task? selectedTask = (Task?)taskArrayRoot.Elements("Task").Select(xmlTaskElement => new Task(
                    Id: (int)xmlTaskElement.ToIntNullable("Id")!,
                    Alias: (string?)xmlTaskElement.Element("Alias"),
                    Description: (string?)xmlTaskElement.Element("Description"),
                    CreatedAtDate: xmlTaskElement.ToDateTimeNullable("CreatedAtDate"),
                    RequiredEffortTime: (TimeSpan)xmlTaskElement.Element("RequiredEffortTime")!,
                    IsMilestone: (bool)xmlTaskElement.Element("IsMilestone")!,
                    Complexity: (EngineerExperience)xmlTaskElement.ToEnumNullable<EngineerExperience>("Complexity")!,
                    StartDate: xmlTaskElement.ToDateTimeNullable("StartDate"),
                    ScheduledDate: xmlTaskElement.ToDateTimeNullable("ScheduledDate"),
                    DeadlineDate: xmlTaskElement.ToDateTimeNullable("DeadlineDate"),
                    CompleteDate: xmlTaskElement.ToDateTimeNullable("CompleteDate"),
                    Deliverables: (string?)xmlTaskElement.Element("Deliverables"),
                    Remarks: (string?)xmlTaskElement.Element("Remarks"),
                    EngineerId: (int)xmlTaskElement.ToIntNullable("EngineerId")!)
                ).FirstOrDefault(filter);
        return selectedTask;
    }

    public Task? Read(int id)
    {
        return Read(task => (task.Id == id));
    }

    ///<summary>
    /// Create a list that contain all of the element that satisfies the condition of the filter function.
    /// if the condition is null (if there was no given delegate to function) the returned value will be the all list.
    ///</summary>
    public IEnumerable<Task?> ReadAll(Func<Task, bool>? filter = null)
    {
        if (filter == null)
        {
            List<Task> tasks = taskArrayRoot.Elements(s_task_xml).Select(taskElement => new Task(
                                Id: (int)taskElement.ToIntNullable("Id")!,
                                Alias: (string?)taskElement.Element("Alias"),
                                Description: (string?)taskElement.Element("Description"),
                                CreatedAtDate: taskElement.ToDateTimeNullable("CreatedAtDate"),
                                RequiredEffortTime: (TimeSpan)taskElement.Element("RequiredEffortTime")!,
                                IsMilestone: (bool)taskElement.Element("IsMilestone")!,
                                Complexity: (EngineerExperience)taskElement.ToEnumNullable<EngineerExperience>("Complexity")!,
                                StartDate: taskElement.ToDateTimeNullable("StartDate"),
                                ScheduledDate: taskElement.ToDateTimeNullable("ScheduledDate"),
                                DeadlineDate: taskElement.ToDateTimeNullable("DeadlineDate"),
                                CompleteDate: taskElement.ToDateTimeNullable("CompleteDate"),
                                Deliverables: (string?)taskElement.Element("Deliverables"),
                                Remarks: (string?)taskElement.Element("Remarks"),
                                EngineerId: (int)taskElement.ToIntNullable("EngineerId")!)
                            ).ToList();

            return tasks;
        }
        else
        {
           
            List<Task> filteredTasks = taskArrayRoot.Elements(s_task_xml).Select(taskElement => new Task(
                                Id: (int)taskElement.ToIntNullable("Id")!,
                                Alias: (string?)taskElement.Element("Alias"),
                                Description: (string?)taskElement.Element("Description"),
                                CreatedAtDate: taskElement.ToDateTimeNullable("CreatedAtDate"),
                                RequiredEffortTime: (TimeSpan)taskElement.Element("RequiredEffortTime")!,
                                IsMilestone: (bool)taskElement.Element("IsMilestone")!,
                                Complexity: (EngineerExperience)taskElement.ToEnumNullable<EngineerExperience>("Complexity")!,
                                StartDate: taskElement.ToDateTimeNullable("StartDate"),
                                ScheduledDate: taskElement.ToDateTimeNullable("ScheduledDate"),
                                DeadlineDate: taskElement.ToDateTimeNullable("DeadlineDate"),
                                CompleteDate: taskElement.ToDateTimeNullable("CompleteDate"),
                                Deliverables: (string?)taskElement.Element("Deliverables"),
                                Remarks: (string?)taskElement.Element("Remarks"),
                                EngineerId: (int)taskElement.ToIntNullable("EngineerId")!)
                            ).Where(filter).ToList();

            return filteredTasks;
        }
    }

    public void Update(Task item)
    {
        if (Read(item.Id) == null)
            throw new DalDoesNotExistExeption($"A task with ID={item.Id} doesn't exist");

        XElement taskArrayRoot = XMLTools.LoadListFromXMLElement(s_task_xml);

        //create an instance of task (converted to XML)
        XElement elementTask = new XElement("Task",
                new XElement("Id", item.Id,
                new XElement("Alias", item.Alias),
                new XElement("Description", item.Description),
                new XElement("CreatedAtDate", item.CreatedAtDate),
                new XElement("IsMilestone", item.IsMilestone),
                new XElement("Complexity", item.Complexity),
                new XElement("StartDate", item.StartDate),
                new XElement("ScheduledDate", item.ScheduledDate),
                new XElement("DeadlineDate", item.DeadlineDate),
                new XElement("CompleteDate", item.CompleteDate),
                (item.Deliverables != null) ? new XElement("Deliverables", item.Deliverables) : null,
                (item.Remarks != null) ? new XElement("Remarks", item.Remarks) : null,
                new XElement("EngineerId", item.EngineerId)
                ));
        Delete(item.Id);
        taskArrayRoot.Add(elementTask);
        XMLTools.SaveListToXMLElement(taskArrayRoot, s_task_xml);
    }

    // Checks if an Engineer exists based on the ID and throws an exception if it doesn't
    void chechIfTaskExist(Task item)
    {
        if (Read(item.Id) == null)
            throw new DalDoesNotExistExeption($"Engineer with ID={item.Id} doesn't exist");
    }

}

