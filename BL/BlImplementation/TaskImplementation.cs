namespace BlImplementation;


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
        catch (BO.BlNullException)
        {

            throw new BO.BlNullException("Null value encountered during creation operation");
        }
        catch (BO.BlInvalidInputException)
        {

            throw new BO.BlInvalidInputException("Invalid input encountered during creation operation");
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
        return item.Id;
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
    public void Update(BO.Task item)
    {
        DO.Task doTaskToUpdate = convertFromBoToDo(item);

        try
        if (doTask.Id < 0)
            BO.Task originalTask = Read(item.Id) ?? throw new BO.BlDoesNotExistExeption($"Task with ID={item.Id} doesn't exist");
            checkValidDoInput(doTaskToUpdate);

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
        var allItems = _dal.Task.ReadAll();

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
        try
        {
            _dal.Task.Delete(id);
        }
        catch (DO.DalDoesNotExistExeption ex)
        {
            throw new BO.BlDoesNotExistExeption($"Task with ID={id} doesn't exist", ex);
        }
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
            throw new BO.BlNullException("Alias cannot be null");
        }
        if (doTask.Id < 0)
        {
            throw new BO.BlInvalidInputException("ID cannot be negative");
        }
        if (doTask.RequiredEffortTime < TimeSpan.Zero)
        {
            throw new BO.BlInvalidInputException("Required effort time cannot be negative");
        }
        if (string.IsNullOrEmpty(doTask.Description))
        {
            throw new BO.BlNullException("Description cannot be null");
        }




    }






    private BO.EngineerInTask generateEngineerInTask(int id)
    {
        if (_dal.Task.Read(id).EngineerId == null)
        {
            return null;
        }
        int EngineerInTaskId = _dal.Task.Read(id).EngineerId!;
        string engineerName = _dal.Engineer.Read(EngineerInTaskId)!.Name!;
        return new BO.EngineerInTask
        {
            Id = EngineerInTaskId,
            Name = engineerName
        };
    }

    private DateTime? generateForecastDate(int id)
    {
        throw new NotImplementedException();
    }

    private BO.MilestoneInTask generateMilestone(int id)
    {
        return null;
    }

    private List<BO.TaskInList> generateDependencies(int id)
    {
        throw new NotImplementedException();
    }

    private BO.Enums.Status generateStatus(int id)
    {
        BO.Enums.Status status;
        DO.Task doTaskForStatus = _dal.Task.Read(id)!;
        if (doTaskForStatus.StartDate == null)
        {
            status = BO.Enums.Status.Uncheduled;
        }
        else if (doTaskForStatus.StartDate != null)
        {
            status = BO.Enums.Status.Scheduled;
        }
        else
        {
            status = BO.Enums.Status.Done;
        }
        return status;
    }


}


