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
using System.Security.Cryptography.X509Certificates;


internal class TaskImplementation : ITask
{
    readonly string s_task_xml = "student";

    // Method to create a new task
    public int Create(Task item)
    {
        // import the root of the XML file 
        XElement taskRoot = LoadListFromXMLElement(s_task_xml);
        // Check if a task with the same properties already exists
        if (tas)
            throw new DalAlreadyExistsException($"A task with ID={item.Id} already exist");

        // Create a new task with a new ID
        Task newTask = new Task(
            DataSource.Config.NextTaskID,
            item.Alias,
            item.Description,
            item.CreatedAtDate,
            item.RequiredEffortTime,
            item.IsMilestone,
            item.Complexity,
            item.StartDate,
            item.ScheduledDate,
            item.DeadlineDate,
            item.CompleteDate,
            item.Deliverables,
            item.Remarks,
            item.EngineerId
        );
        if (item.Alias == null || item.Alias.Length == 0
           || item.Description == null || item.Description.Length == 0
          || item.CreatedAtDate == null || item.RequiredEffortTime == null || item.StartDate == null
            || item.ScheduledDate == null || item.DeadlineDate == null || item.CompleteDate == null
           || item.EngineerId < 0)
            throw new Exception("The Task properties are invalid");

        // Add the new task to the collection
        DataSource.Tasks.Add(newTask);

        // Return the ID of the newly created task
        return newTask.Id;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public void DeleteAll()
    {
        throw new NotImplementedException();
    }

    public Task? Read(Func<Task, bool> filter)
    {
        throw new NotImplementedException();
    }

    public Task? Read(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Task?> ReadAll(Func<Task, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public void Update(Task item)
    {
        throw new NotImplementedException();
    }

   
    
}


