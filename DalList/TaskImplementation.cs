namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class TaskImplementation : ITask
{
    public Task? FindId(Task item)
    {
        foreach (var _task in DataSource.Tasks)
        {
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
                return null;
            }
        }
        return item;
    }

    public int Create(Task item)
    {
        if (FindId(item) == null)
            throw new InvalidOperationException("ERROR: a task with such property already exist");
        Task newTask = new Task(DataSource.Config.NextTaskID,item.Alias
            ,item.Description,item.CreatedAtDate,item.RequiredEffortTime,
            item.IsMilestone,item.Complexity, item.StartDate,
            item.ScheduledDate,item.DeadlineDate, item.CompleteDate,
            item.Deliverables, item.Remarks,item.EngineerId);
        DataSource.Tasks.Add(newTask);
        return newTask.Id;
    }

    public void Delete(int id)
    {
        Task? ptrTask = Read(id);
        if(Read(id)==null)
        { 
            throw new InvalidOperationException("ERROR: this Task with such ID doesn't exist"); 
        }
        DataSource.Tasks.Remove(ptrTask);
            
    }

    public Task? Read(int id)
    {
       Task ? temp = DataSource.Tasks.FirstOrDefault(t => t.Id == id);
        if(temp != null)
            return temp;
        return null;
    }

    public List<Task> ReadAll()
    {
        return new List<Task>(DataSource.Tasks);
    }

    public void Update(Task item)
    {
        Task? deletedTask = Read(item.Id);
        if (deletedTask == null)
            throw new InvalidOperationException("ERROR: task with such ID does not exist");
        DataSource.Tasks.Remove(deletedTask);
        DataSource.Tasks.Add(item);
    }
}
