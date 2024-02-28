namespace BlImplementation;
using System;
using System.Collections.Generic;

internal class TaskImplementation : BlApi.ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    // Creates a new task
    // This method is responsible for creating a new task. It converts the business object task to a data object task and performs necessary validations before saving it.
    public int Create(BO.Task item)
    {
        // Convert the business object task to a data object task, cannot be null
        DO.Task doTaskCreate = convertFromBoToDo(item);

        try
        {
            // Validate the data object task
            checkValidDoInput(doTaskCreate);
            // Call the data access layer to create the task
            int taskId = _dal.Task.Create(doTaskCreate);
            // Add dependencies to the newly created task
            addDependencyToDal(item, taskId);
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            // Handle the case where the task already exists in the data store
            throw new BO.BlAlreadyExistsException($"Task with ID={item.Id} already exists", ex);
        }
        catch (BO.BlNullException)
        {
            // Handle the case where a null value is encountered during the creation operation
            throw new BO.BlNullException("Null value encountered during creation operation");
        }
        catch (BO.BlInvalidInputException)
        {
            // Handle the case where invalid input is encountered during the creation operation
            throw new BO.BlInvalidInputException("Invalid input encountered during creation operation");
        }

        // Return the ID of the newly created task
        return item.Id;
    }

    // Updates an existing task
    // This method updates an existing task. It converts the business object task to a data object task and performs necessary validations before updating it.
    public void Update(BO.Task item)
    {
        // Convert the business object task to a data object task
        DO.Task doTaskToUpdate = convertFromBoToDo(item);

        try
        {
            // Check if the task exists before updating
            BO.Task originalTaskCreationTest = Read(item.Id) ?? throw new BO.BlDoesNotExistExeption($"Task with ID={item.Id} doesn't exist");
            // Validate the data object task
            checkValidDoInput(doTaskToUpdate);
            // Call the data access layer to update the task
            _dal.Task.Update(doTaskToUpdate);
            // Update dependencies for the task
            updateDependencyInDal(item);
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            // Handle the case where the task already exists in the data store
            throw new BO.BlAlreadyExistsException($"Task with ID={item.Id} already exists", ex);
        }
        catch (BO.BlNullException)
        {
            // Handle the case where a null value is encountered during the update operation
            throw new BO.BlNullException("Null value encountered during creation operation");
        }
        catch (BO.BlInvalidInputException)
        {
            // Handle the case where invalid input is encountered during the update operation
            throw new BO.BlInvalidInputException("Invalid input encountered during creation operation");
        }
    }

    // Reads a task by its ID
    public BO.Task? Read(int id)
    {
        // Read the task from the DAL based on the ID
        DO.Task doTaskRead = _dal.Task.Read(id) ?? throw new BO.BlDoesNotExistExeption($"Task with Id= {id} doesn't exist");
        return convertFromDotoBo(doTaskRead);
    }

    // Reads all tasks with an optional filter
    public IEnumerable<BO.Task?> ReadAll(Func<BO.Task, bool>? filter = null)
    {
        // Read all tasks from the DAL
        var allTasks = _dal.Task.ReadAll();

        if (filter == null)
        {
            // If no filter is provided, convert all tasks to business objects
            return (from DO.Task doTask in allTasks
                    select convertFromDotoBo(doTask));
        }

        // If a filter is provided, convert only the filtered tasks to business objects
        return (from DO.Task doTask in allTasks
                select convertFromDotoBo(doTask)).Where(filter);
    }

    // Deletes a task by its ID
    public void Delete(int id)
    {
        try
        {
            // Read the task from the DAL based on the ID
            BO.Task taskToDelete = Read(id) ?? throw new BO.BlDoesNotExistExeption($"Task with ID={id} doesn't exist");
            foreach (var boDependency in taskToDelete.Dependencies)
            {
                // Check if any dependencies are not done
                if (boDependency.Status != BO.Enums.Status.Done)
                {
                    throw new BO.BlInvalidInputException("Cannot delete a task with dependencies that are not done");
                }
            }

            // Delete the task from the DAL and its dependencies
            _dal.Task.Delete(id);
            deleteDEpendencies(taskToDelete.Id);
        }
        catch (DO.DalDoesNotExistExeption ex)
        {
            throw new BO.BlDoesNotExistExeption($"Task with ID={id} doesn't exist", ex);
        }

    }

    public void deleteAll()
    {
        try
        {
            _dal.Task.DeleteAll();
        }
      catch 
      (Exception ex)
        {
            throw ex;
        }
    }

    // Update the dependencies in the DAL based on the task item
    private void updateDependencyInDal(BO.Task item)
    {
        // Get the list of dependencies from the DAL based on the dependent task ID
        List<DO.Dependency> dependencyDalList = _dal.Dependency.ReadAll(dependency => dependency.DependentTask == item.Id).ToList();
        if (dependencyDalList == null)
        {
            // If no dependencies exist, add the dependency to the DAL
            addDependencyToDal(item, item.Id);
        }
        else if (item.Dependencies != null)
        {
            // If dependencies exist, delete the old dependencies and add the new ones
            foreach (var dependency in dependencyDalList)
            {
                _dal.Dependency.Delete(dependency.Id);
            }
            addDependencyToDal(item, item.Id);
        }

    }

    // Delete the dependencies in the DAL based on the task ID
    private void deleteDEpendencies(int id)
    {
        try
        {
            // Get the list of dependencies from the DAL based on the depends on task ID
            if (_dal.Dependency.ReadAll(dependency => dependency.DependsOnTask == id) != null)
            {
                // If dependencies exist, delete each dependency from the DAL
                foreach (var dependency in _dal.Dependency.ReadAll(dependency => dependency.DependsOnTask == id))
                {
                    _dal.Dependency.Delete(dependency.Id);
                }
            }
        }
        catch (DO.DalDoesNotExistExeption ex)
        {
            throw new BO.BlDoesNotExistExeption($"Dependency with ID={id} doesn't exist", ex);
        }

    }

    // Converts a business object task to a data object task
    private DO.Task convertFromBoToDo(BO.Task? item)
    {
        // Convert the properties of the business object task to the data object task
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
        // Convert the properties of the data object task to the business object task
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
        // Check if the alias is null or empty
        if (string.IsNullOrEmpty(doTask.Alias))
        {
            throw new BO.BlNullException("Alias cannot be null");
        }
        // Check if the ID is negative
        if (doTask.Id < 0)
        {
            throw new BO.BlInvalidInputException("ID cannot be negative");
        }
        // Check if the required effort time is negative
        if (doTask.RequiredEffortTime < TimeSpan.Zero)
        {
            throw new BO.BlInvalidInputException("Required effort time cannot be negative");
        }
        // Check if the description is null or empty
        if (string.IsNullOrEmpty(doTask.Description))
        {
            throw new BO.BlNullException("Description cannot be null");
        }
    }

    // Generates an engineer in task object based on the task ID
    private BO.EngineerInTask generateEngineerInTask(int id)
    {
        // Check if the engineer ID is null
        if (_dal.Task.Read(id).EngineerId == null)
        {
            return null;
        }
        // Get the engineer ID and name from the DAL
        int EngineerInTaskId = _dal.Task.Read(id).EngineerId!;
        string engineerName = _dal.Engineer.Read(EngineerInTaskId)!.Name!;
        // Create the engineer in task object
        return new BO.EngineerInTask
        {
            Id = EngineerInTaskId,
            Name = engineerName
        };
    }

    // Generates a forecast date based on the task ID
    private DateTime? generateForecastDate(int taskId)
    {
        BO.Task task = Read(taskId);
        if (task.Status == BO.Enums.Status.Unscheduled)
        {
            return null;
        }
        else if (task.Status == BO.Enums.Status.Scheduled)
        {
            DateTime actualStartDate = task.StartDate?? DateTime.MinValue;
            DateTime plannedStartDate = task.ScheduledDate ?? DateTime.MinValue;
            TimeSpan duration = task.RequiredEffortTime ?? TimeSpan.Zero;
            DateTime forecastDate = DateTime.MaxValue;
            
            if (actualStartDate != DateTime.MinValue)
            {
                forecastDate = actualStartDate;
                //throw new BO.BlInvalidOperation("Can't update the start date of a task that already started");
            }

            if (plannedStartDate != DateTime.MinValue)
            {
                forecastDate = DateTime.Compare(actualStartDate, plannedStartDate) > 0 ?
                    actualStartDate + duration : plannedStartDate + duration;

            }

            return forecastDate;
        }

        return null;
    }

    // Generates a milestone in task object based on the task ID
    private BO.MilestoneInTask generateMilestone(int id)
    {
        return null;
    }

    // Generates a list of task dependencies based on the task ID
    private List<BO.TaskInList> generateDependencies(int taskId)
    {
        // Create a list to store the task dependencies
        List<BO.TaskInList> dependencies = new List<BO.TaskInList>();
        // Get the list of dependencies from the DAL based on the dependent task ID
        List<DO.Dependency> doDependency = _dal.Dependency.ReadAll().Where(dependency => dependency.DependentTask == taskId).ToList();
        // Convert each dependency to a business object and add it to the list
        foreach (var dependency in doDependency)
        {
            dependencies.Add(new BO.TaskInList
            {
                Id = (int)dependency.DependentTask!,
                Alias = _dal.Task.Read((int)dependency.DependentTask)!.Alias
            });
        }
        return dependencies;
    }

    // Generates the status of a task based on the task ID
    private BO.Enums.Status generateStatus(int id)
    {
        BO.Enums.Status status;
        // Read the task from the DAL based on the ID
        DO.Task doTaskForStatus = _dal.Task.Read(id)!;

        if (doTaskForStatus.ScheduledDate == null)
        {
            status = BO.Enums.Status.Unscheduled;
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

    // Add the dependencies to the DAL based on the task item and task ID
    private void addDependencyToDal(BO.Task item, int taskId)
    {
        // Check if dependencies exist
        if (item.Dependencies != null)
        {
            // Create DO.Dependency objects for each dependency
            var dependenciesToAdd = item.Dependencies
                .Select(boDependency => new DO.Dependency
                {
                    DependentTask = taskId,
                    DependsOnTask = boDependency.Id
                });

            try
            {
                // If there are dependencies to add, execute the creation for each
                dependenciesToAdd.ToList().ForEach(doDependency =>
                {
                    try
                    {
                        _dal.Dependency.Create(doDependency);
                    }
                    catch (DO.DalAlreadyExistsException ex)
                    {
                        // Handle DAL exception
                        throw new BO.BlAlreadyExistsException($"Dependency with ID={doDependency.DependsOnTask} already exists", ex);
                    }
                });
            }
            catch (Exception ex)
            {
                // Handle any other exceptions if needed
                throw ex;
            }
        }
    }

    private void updateScheduleDate(BO.Task boTask, DateTime date)
    {
        List<DO.Dependency?> dependencies = _dal.Dependency.ReadAll(dependency => dependency.DependentTask == boTask.Id).ToList();

        if (dependencies != null)
        {
            foreach(var dependency in dependencies)
            {
                DO.Task task = _dal.Task.Read(task => task.Id == dependency!.DependsOnTask)!;
                if(task.ScheduledDate == null)
                {
                    throw new BO.BlUpdateImpossible($"Can't update Task {boTask.Id} becouse at least one of its dependent on previous tasks (found task {task.Id}) was not assign with its start date. please make sure to update the start dates of all the privious tasks");
                }
                else if (task.ScheduledDate < boTask.ScheduledDate)
                {
                    throw new BO.BlUpdateImpossible($"Can't update Task {boTask.Id} becouse at least one of its dependent on previous tasks (found task {task.Id}) has a start date that is smaller than the start date of {boTask.Id}");
                }
            }
          boTask.ScheduledDate = date;  
        }

        // if there was no problem with the date of boTask 
        DO.Task taskToUpdate = convertFromBoToDo(boTask);
        _dal.Task.Update(taskToUpdate);
    }


    public static DateTime FirstAvilableDateOfScheduleDate(this BO.Task task,  DateTime projectStartDate)
    {
        // if there is no preliminary tasks return the project start date
        if (task.Dependencies == null)
        {
            return projectStartDate;
        }
        else 
        {

        }
        return DateTime.MinValue;
    }
}



