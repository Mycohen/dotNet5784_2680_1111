namespace BlImplementation;


using System;
using System.Collections.Generic;
using System.Security.Cryptography;

internal class TaskImplementation : BlApi.ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    // Creates a new task
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
        catch (BO.BlNullException)
        {

            throw new BO.BlNullException("Null value encountered during creation operation");
        }
        catch (BO.BlInvalidInputException)
        {

            throw new BO.BlInvalidInputException("Invalid input encountered during creation operation");
        }

        return item.Id;
    }

    // Updates an existing task
    public void Update(BO.Task item)
    {
        DO.Task doTaskToUpdate = convertFromBoToDo(item);

        try
        {
            BO.Task originalTask = Read(item.Id) ?? throw new BO.BlDoesNotExistExeption($"Task with ID={item.Id} doesn't exist");
            checkValidDoInput(doTaskToUpdate);
            _dal.Task.Update(doTaskToUpdate);

        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Task with ID={item.Id} already exists", ex);
        }
        catch (BO.BlNullException)
        {

            throw new BO.BlNullException("Null value encountered during creation operation");
        }
        catch (BO.BlInvalidInputException)
        {

            throw new BO.BlInvalidInputException("Invalid input encountered during creation operation");
        }
    }

    // Reads a task by its ID
    public BO.Task? Read(int id)
    {
        DO.Task doTaskRead = _dal.Task.Read(id) ?? throw new BO.BlDoesNotExistExeption($"Task with Id= {id} doesn't exist");
        return convertFromDotoBo(doTaskRead);
    }

    // Reads all tasks with an optional filter
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

    // Deletes a task by its ID
    public void Delete(int id)
    {
        try
        {
            BO.Task taskToDelete = Read(id) ?? throw new BO.BlDoesNotExistExeption($"Task with ID={id} doesn't exist");

            _dal.Task.Delete(id);
        }
        catch (DO.DalDoesNotExistExeption ex)
        {
            throw new BO.BlDoesNotExistExeption($"Task with ID={id} doesn't exist", ex);
        }
    }

    // Converts a business object task to a data object task
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

    // Converts a data object task to a business object task
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

    // Checks if the data object task has valid input
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

    // Generates an engineer in task object based on the task ID
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

    // Generates a forecast date based on the task ID
    private DateTime? generateForecastDate(int id)
    {
        throw new NotImplementedException();
    }

    // Generates a milestone in task object based on the task ID
    private BO.MilestoneInTask generateMilestone(int id)
    {
        return null;
    }

    // Generates a list of task dependencies based on the task ID
    private List<BO.TaskInList> generateDependencies(int id)
    {
       DO
    }

    // Generates the status of a task based on the task ID
    private BO.Enums.Status generateStatus(int id)
    {
        BO.Enums.Status status;
        DO.Task doTaskForStatus = _dal.Task.Read(id)!;

        if (doTaskForStatus.StartDate == null)
        {
            status = BO.Enums.Status.Uncheduled;
        }
        else if (doTaskForStatus.CompleteDate == null)
        {
            if (doTaskForStatus.EngineerId != 0)
            {
                status = BO.Enums.Status.OnTrack;
            }
            else
            {
                status = BO.Enums.Status.Scheduled;
            }
        }
        else
        {
            status = BO.Enums.Status.Done;
        }

        return status;
    }
}


