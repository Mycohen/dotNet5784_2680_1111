namespace BlImplementation;

using BO;

//using BO;
////using BlApi;
//using BO;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

internal class TaskImplementation : BlApi.ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public int Create(BO.Task item)
    {
        DO.Task doTaskCreate = convertFromBoToDo(item);
        try
        {
            checkValidDoInput(doTaskCreate);
            int taskId = _dal.Task.Create(doTaskCreate);

        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Task with ID={item.Id} already exists", ex);
        }

        return item.Id;
    }
    public void Update(BO.Task item)
    {
        DO.Task doTaskToUpdate = convertFromBoToDo(item);
        
        try
        {BO.Task originalTask= Read(item.Id) ?? throw new BO.BlDoesNotExistExeption($"Task with ID={item.Id} doesn't exist");
            checkValidDoInput(doTaskToUpdate);
            if(originalTask.Status==)
           
        }
        catch (DO.DalDoesNotExistExeption ex)
        {
            throw new BO.BlDoesNotExistExeption($"Task with ID={item.Id} doesn't exist", ex);
        }
    }
    public BO.Task? Read(int id)
    {
       DO.Task doTaskRead = _dal.Task.Read(id) ?? throw new BO.BlDoesNotExistExeption($"Task with Id= {id} doesn't exist");
        return convertFromDotoBo(doTaskRead);

    }
    public IEnumerable<BO.Task?> ReadAll(Func<BO.Task, bool>? filter = null)
    {
        var allTasks = _dal.Task.ReadAll();

        if (filter == null)
        {
            return (from DO.Task doTask in allTasks
                    select convertFromDotoBo(doTask));
        }

        return (from DO.Task doTask in allTasks
                select convertFromDotoBo(doTask)).Where(filter);


    }
    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
    private DO.Task convertFromBoToDo(BO.Task? item)
    {
        return new DO.Task
        {
            Id = item.Id,
            Description = item.Description,
            Alias = item.Alias,
            CreatedAtDate = item.CreatedAtDate,
            RequiredEffortTime = item.RequiredEffortTime,
            StartDate = item.StartDate,
            ScheduledDate = item.ScheduledDate,
            DeadlineDate = item.DeadlineDate,
            CompleteDate = item.CompleteDate,
            Deliverables = item.Deliverables,
            Remarks = item.Remarks,
            Complexity = item.Complexity
        };
    }

    private BO.Task convertFromDotoBo(DO.Task doTask)
    {
        return new BO.Task
        {
            Id = doTask.Id,
            Description = doTask.Description,
            Alias = doTask.Alias,
            Status = generateStatus(doTask.Id),
            CreatedAtDate = doTask.CreatedAtDate,
            Dependencies = generateDependencies(doTask.Id),
            Milestone = generateMilestone(doTask.Id),
            RequiredEffortTime = doTask.RequiredEffortTime,
            StartDate = doTask.StartDate,
            ScheduledDate = doTask.ScheduledDate,
            ForecastDate = generateForecastDate(doTask.Id),
            DeadlineDate = doTask.DeadlineDate,
            CompleteDate = doTask.CompleteDate,
            Deliverables = doTask.Deliverables,
            Remarks = doTask.Remarks,
            Engineer = generateEngineerInTask(doTask.Id),
            Complexity = doTask.Complexity
        };
    }

    private void checkValidDoInput(DO.Task? doTask)
    {
        if (string.IsNullOrEmpty(doTask.Alias))
        {
            throw new BlNullException("Alias cannot be null");
        }
        if (doTask.Id < 0)
        {
            throw new BlInvalidInputException("ID cannot be negative");
        }
        if (doTask.RequiredEffortTime < TimeSpan.Zero)
        {
            throw new BlInvalidInputException("Required effort time cannot be negative");
        }
        if (string.IsNullOrEmpty(doTask.Description))
        {
            throw new BlNullException("Description cannot be null");
        }




    }






    private EngineerInTask generateEngineerInTask(int id)
    {
        throw new NotImplementedException();
    }

    private DateTime? generateForecastDate(int id)
    {
        throw new NotImplementedException();
    }

    private MilestoneInTask generateMilestone(int id)
    {
        throw new NotImplementedException();
    }

    private List<BO.TaskInList> generateDependencies(int id)
    {
        throw new NotImplementedException();
    }

    private BO.Enums.Status generateStatus(int id)
    {
        BO.Enums.Status status;
        DO.Task doTaskForStatus = _dal.Task.Read(id);


    }


}


