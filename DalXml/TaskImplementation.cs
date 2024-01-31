﻿namespace Dal;
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
        int taskId = Config.NextTaskId;
        XElement taskArrayRoot = XMLTools.LoadListFromXMLElement(s_task_xml);

        // Create an instance of task (converted to XML)
        XElement elementTask = new XElement("Task",
            new XElement("Id", taskId), // Separate Id element with its own closing tag
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
        );

        taskArrayRoot.Add(elementTask);
        XMLTools.SaveListToXMLElement(taskArrayRoot, s_task_xml);

        return taskId;
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
        XMLTools.ResetID("NextTaskId");
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

        // Load the XElement containing all Engineers from the XML file
        XElement taskRoot = XMLTools.LoadListFromXMLElement(s_task_xml);

        // Find the target Engineer XElement based on the ID
        XElement? targetTaskElement = taskRoot.Elements("Task")
            .FirstOrDefault(elem => (int)elem.Element("Id")! == id);

        // If the target Engineer XElement exists, create a new Engineer object and populate it with the XElement's properties
        if (targetTaskElement != null)
        {
            Task task = new Task
            {
                Id = (int)targetTaskElement.Element("Id")!,
                Alias = (string)targetTaskElement.Element("Alias")!,
                Description = (string)targetTaskElement.Element("Description")!,
                CreatedAtDate = (DateTime)targetTaskElement.Element("CreatedAtDate")!,
                IsMilestone = (bool)targetTaskElement.Element("IsMilestone")!,
                Complexity = (EngineerExperience)targetTaskElement.ToEnumNullable<EngineerExperience>("Complexity")!,
                StartDate = (DateTime)targetTaskElement.Element("StartDate")!,
                ScheduledDate = (DateTime)targetTaskElement.Element("ScheduledDate")!,
                DeadlineDate = (DateTime)targetTaskElement.Element("DeadlineDate")!,
                CompleteDate = (DateTime)targetTaskElement.Element("CompleteDate")!,
                Deliverables = (string?)targetTaskElement.Element("Deliverables"),
                Remarks = (string?)targetTaskElement.Element("Remarks"),
                EngineerId = (int)targetTaskElement.Element("EngineerId")!
            };

            // Return the created Task object
            return task;
        }

        // If the target Task XElement doesn't exist, return null
        return null;
    }

    ///<summary>
    /// Create a list that contain all of the element that satisfies the condition of the filter function.
    /// if the condition is null (if there was no given delegate to function) the returned value will be the all list.
    ///</summary>
    public IEnumerable<Task?> ReadAll(Func<Task, bool>? filter = null)
    {
        // If no filter function is provided, return all Tasks
        if (filter == null)
            return XMLTools.LoadListFromXMLSerializer<Task>(s_task_xml).Select(item => item);
        // If a filter function is provided, return the Tasks that match the filter
        else
            return XMLTools.LoadListFromXMLSerializer<Task>(s_task_xml).Where(filter);
    }


    public void Update(Task item)
    {
        if (Read(item.Id) == null)
            throw new DalDoesNotExistExeption($"A task with ID={item.Id} doesn't exist");

        XElement taskArrayRoot = XMLTools.LoadListFromXMLElement(s_task_xml);

        //create an instance of task (converted to XML)
        XElement elementTask = new XElement("Task",
                new XElement("Id", item.Id),
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
        );
        Delete(item.Id);
        taskArrayRoot.Add(elementTask);
        XMLTools.SaveListToXMLElement(taskArrayRoot, s_task_xml);
    }

    // Checks if a Task exists based on the ID and throws an exception if it doesn't
    void chechIfTaskExist(Task item)
    {
        if (Read(item.Id) == null)
            throw new DalDoesNotExistExeption($"Task with ID={item.Id} doesn't exist");
    }

}

