namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

// Class implementing ITask interface
public class TaskImplementation : ITask
{
    // Method to find a task based on its properties
    public Task? FindId(Task item)
    {
        foreach (var _task in DataSource.Tasks)
        {
            // Check if the properties match
            if (_task.Alias == item.Alias
                && _task.Description == item.Description
                && _task.CreatedAtDate == item.CreatedAtDate
                && _task.RequiredEffortTime == item.RequiredEffortTime
                && _task.IsMilestone == item.IsMilestone
                && _task.Complexity == item.Complexity
                && _task.StartDate == item.StartDate
                && _task.ScheduledDate == item.ScheduledDate
                && _task.DeadlineDate == item.DeadlineDate
                && _task.CompleteDate == item.CompleteDate
                && _task.Deliverables == item.Deliverables
                && _task.Remarks == item.Remarks
                && _task.EngineerId == item.EngineerId
            )
            {
                return null; // Return null if a matching task is found
            }
        }
        return item; // Return the input item if no matching task is found
    }

    // Method to create a new task
    public int Create(Task item)
    {
        // Check if a task with the same properties already exists
        if (FindId(item) == null)
            throw new InvalidOperationException("ERROR: a task with such properties already exists");

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

        // Add the new task to the collection
        DataSource.Tasks.Add(newTask);

        // Return the ID of the newly created task
        return newTask.Id;
    }

    // Method to delete a task by ID
    public void Delete(int id)
    {
        // Read the task with the given ID
        Task? ptrTask = Read(id);

        // Throw an exception if the task doesn't exist
        if (ptrTask == null)
        {
            throw new InvalidOperationException("ERROR: this task with such ID doesn't exist");
        }

        // Remove the task from the collection
        DataSource.Tasks.Remove(ptrTask);
    }

    // Method to read a task by ID
    public Task? Read(int id)
    {
        // Use LINQ to find the task with the given ID
        Task? temp = DataSource.Tasks.FirstOrDefault(t => t.Id == id);

        // Return the task if found, otherwise return null
        return temp;
    }

    // Method to read all tasks
    public List<Task> ReadAll()
    {
        // Return a copy of the tasks collection
        return new List<Task>(DataSource.Tasks);
    }

    // Method to update a task
    public void Update(Task item)
    {
        // Read the task with the given ID
        Task? deletedTask = Read(item.Id);

        // Throw an exception if the task doesn't exist
        if (deletedTask == null)
            throw new InvalidOperationException("ERROR: task with such ID does not exist");

        // Remove the existing task
        DataSource.Tasks.Remove(deletedTask);

        // Add the updated task
        DataSource.Tasks.Add(item);
    }
}
