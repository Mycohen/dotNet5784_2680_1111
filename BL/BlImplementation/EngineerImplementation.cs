
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;


namespace BlImplementation;


internal class EngineerImplementation :BlApi.IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    
    public int Create(BO.Engineer boEngineer)
    {

        try
        {
            CheckIfValidEngineerData(boEngineer);
            //if the data are valid
            //update the task that assigned to the engineer
            updateTaskThatAssignedToEngineer(boEngineer);

            DO.Engineer doEngineer = convertFrom_BO_to_DO(boEngineer);
            //try to add the doEngineer to the DAL 
            _dal.Engineer.Create(doEngineer);
        }
        catch (Exception ex)
        {
            throw new BO.BlDalError($"exception from DAL {ex}", ex);
        }
        finally
        {
            Console.WriteLine("Data from the BAL was successfully saved in DAL");
        }
        return boEngineer.Id;
    }

    public void Delete(int id)
    {
        try {
            BO.Engineer boEngineer = this.Read(id);
            if (boEngineer == null)
                throw new ($"Engineer with {id} does not exist");

            else if (boEngineer.Task != null)
                throw new BO.BlEngineerHasTaskExeption($"Engineer with {id} has a task and " +
                    $"can not be deleted");

            else if (_dal.Task.ReadAll(task => task.EngineerId == id) != null)
                throw new BO.BlEngineerHasTaskExeption($"Engineer with {id} has a task " +
                    $"and can not be deleted");
            else
                _dal.Engineer.Delete(id);
        }
        catch (Exception ex)
        {
            throw new BO.BlDalError($"exception from DAL {ex}", ex);
        }
        finally
        {
            Console.WriteLine("Data from the BAL was successfully deleted in DAL");
        }
        
    }

    public BO.Engineer Read(int id)
    {
        DO.Engineer doEngineer = _dal.Engineer.Read(id) ??
            throw new BO.BlDoesNotExistExeption($"Engineer with {id} does not exist");
        BO.Engineer boEngineer = convertFromDoToBo(doEngineer);
        return boEngineer;
    }

    public IEnumerable<BO.Engineer?> ReadAll(Func<BO.Engineer, bool>? filter = null)
    {
        IEnumerable<BO.Engineer>? filterdEngineers =
            (from DO.Engineer engineers in _dal.Engineer.ReadAll()
             select convertFromDoToBo(engineers)).ToList();

        if (filter == null)
        {
            return filterdEngineers;
        }
        else
        {
            return filterdEngineers.Where(filter).ToList<BO.Engineer>();
        }



    }
    
    public void Update(BO.Engineer boEngineer)
    {

        try
        {
            DO.Engineer doEngineer = _dal.Engineer.Read(boEngineer.Id) ??
                throw new BO.BlDoesNotExistExeption($"Engineer with such ID = {boEngineer.Id}" +
                $" does not excist");

            CheckIfValidEngineerData(boEngineer);

            if (doEngineer.Level > boEngineer.Level)
            {
                throw new BO.BlInvalidInputException($"The level of the updated Engineer" +
                    $" should not be lower than the current level: current level is {doEngineer.Level} " +
                    $"entered level was {boEngineer.Level}");
            }

            updateTaskThatAssignedToEngineer(boEngineer);

            updateTaskInDo(boEngineer);

            _dal.Engineer.Update(convertFrom_BO_to_DO(boEngineer));
        }
        catch (Exception ex)
        {
            throw new BO.BlDalError($"unrecognized exception from DAL {ex}", ex);
        }
        finally
        {
            Console.WriteLine("Data from the BAL was successfully saved in DAL");
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
            throw  ex;
        }
    }

    private void updateTaskInDo(BO.Engineer boEngineer)
    {
       if (boEngineer.Task!=null)
        {
            DO.Task? originalTask = _dal.Task.Read(boEngineer.Task.Id);
            if (originalTask != null)
            {
                DO.Task updatedExsitingTask = new DO.Task(
                                    EngineerId: boEngineer.Id,
                                    Id: originalTask.Id,
                                    Alias: originalTask.Alias,
                                    Description: originalTask.Description,
                                    CreatedAtDate: originalTask.CreatedAtDate,
                                    RequiredEffortTime: originalTask.RequiredEffortTime,
                                    IsMilestone: originalTask.IsMilestone,
                                    Complexity: originalTask.Complexity,
                                    StartDate: DateTime.Now,
                                    ScheduledDate: originalTask.ScheduledDate,
                                    DeadlineDate: originalTask.DeadlineDate,
                                    CompleteDate: originalTask.CompleteDate,
                                    Deliverables: originalTask.Deliverables,
                                    Remarks: originalTask.Remarks);
                _dal.Task.Update(updatedExsitingTask);
            }
        }
    }

    // function for converting a DO.Engineer to BO.Engineer
    private BO.Engineer convertFromDoToBo(DO.Engineer boEngineer)
    {
        return new BO.Engineer
        {
            Id = boEngineer.Id,
            Name = boEngineer.Name,
            Email = boEngineer.Email,
            Cost = boEngineer.Cost,
            Level = boEngineer.Level,
            Task = MapTask(boEngineer.Id)
        };
    }

    private DO.Engineer convertFrom_BO_to_DO(BO.Engineer boEngineer)
    {
        return new DO.Engineer
            (Id: boEngineer.Id, Email: boEngineer.Email,
            Cost: boEngineer.Cost, Name: boEngineer.Name, 
            Level: boEngineer.Level);
    }

    private BO.TaskInEngineer? MapTask(int? id)
    {
        if (id == null)
            return null;

        // Find the Engineer task by Task ID using Task.ReadAll with a lambda expression
        var engineerTask = _dal.Task.ReadAll(t => t.Id == id).FirstOrDefault();

        if (engineerTask == null)
            return null;

        // Map properties
        BO.TaskInEngineer mappedTask = new BO.TaskInEngineer
        {
            Id = engineerTask.Id,
            Alias = engineerTask.Alias
        };

        return mappedTask;
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            // Attempt to create a MailAddress object with the given email
            MailAddress mailAddress = new MailAddress(email);
            // If no exceptions are thrown, the email address is valid
            return true;
        }
        catch (FormatException)
        {
            // If FormatException is thrown, the email address is not valid
            return false;
        }
    }

    private bool CheckIfValidEngineerData(BO.Engineer boEngineer)
    {
        if (boEngineer.Id <= 0)
            throw new BO.BlInvalidInputException($"Invalid Input of Engineer ID value. The value must be greater than 0. Entered value is {boEngineer.Id}");
        if (boEngineer.Name == null || boEngineer.Name == "")
            throw new BO.BlInvalidInputException($"Invalid input of Engineer Name value. The value must be a non empty string");
        if (boEngineer.Cost <= 0)
            throw new BO.BlInvalidInputException($"Invalid Input of Engineer Cost value. The value must be greater than 0. Entered value is {boEngineer.Cost}");
        if (boEngineer.Email == null)
            throw new BO.BlNullException("The value of Engineer Email address must be entered (can not be null).");
        else
        if (!IsValidEmail(boEngineer.Email))
            throw new BO.BlInvalidInputException("The value of Engineer Email address must be entered.");
        else
            return true;
    }

    /// <summary>
    /// Updates the task that is assigned to the engineer.
    /// </summary>
    /// <param name="boUpdateEngineer">The engineer to update.</param>
    private void updateTaskThatAssignedToEngineer(BO.Engineer boUpdateEngineer)
    {
        // Check if the engineer has a task
        if (boUpdateEngineer.Task != null)
        {
            // Check if there is an existing task with the same engineer ID
            DO.Task? existingDoTaskWithBoEngineerId = _dal.Task.Read(task => task.EngineerId == boUpdateEngineer.Id);
            if (existingDoTaskWithBoEngineerId != null)
            {
                // Throw an exception if the engineer is already assigned to a task
                if (existingDoTaskWithBoEngineerId!.EngineerId == boUpdateEngineer.Id)
                    throw new BO.BlAlreadyExistsException($"The Engineer with ID = {boUpdateEngineer.Id}" +
                        $" already assigned to the Task with ID = {existingDoTaskWithBoEngineerId.Id}");
            }
            else if (boUpdateEngineer.Level < _dal.Task.Read(task=>task.Id == boUpdateEngineer.Task.Id)!.Complexity)
            {
                throw new BO.BlInvalidOperation($"Invalid Level of Engineer");
            }
            // Update the task in the DAL
            updateTaskInDal(boUpdateEngineer, existingDoTaskWithBoEngineerId);
        }
    }

    private void updateTaskInDal(BO.Engineer boUpdateEngineer, DO.Task? existingDoTaskWithBoEngineerId)
    {
        //if there is no engineer with such ID in the DAL.Task
        if (existingDoTaskWithBoEngineerId == null)
        {
            DO.Task tempTaskFromDal = _dal.Task.Read(boUpdateEngineer.Task.Id);
                DO.Task updatedExsitingTask = new DO.Task(
                    EngineerId: boUpdateEngineer.Id,
                    Id: tempTaskFromDal.Id,
                    Alias: tempTaskFromDal.Alias,
                    Description: tempTaskFromDal.Description,
                    CreatedAtDate: tempTaskFromDal.CreatedAtDate,
                    RequiredEffortTime: tempTaskFromDal.RequiredEffortTime,
                    IsMilestone: tempTaskFromDal.IsMilestone,
                    Complexity: tempTaskFromDal.Complexity,
                    StartDate: tempTaskFromDal.StartDate,
                    ScheduledDate: tempTaskFromDal.ScheduledDate,
                    DeadlineDate: tempTaskFromDal.DeadlineDate,
                    CompleteDate: tempTaskFromDal.CompleteDate,
                    Deliverables: tempTaskFromDal.Deliverables,
                    Remarks: tempTaskFromDal.Remarks);
                _dal.Task.Update(updatedExsitingTask);
        }
        else if (existingDoTaskWithBoEngineerId.Id != boUpdateEngineer.Task.Id)// chenge the refernce of engineer to another task
        {
            //get the task that is currently assign to the engineer
            DO.Task tempTaskFromDal = existingDoTaskWithBoEngineerId;
            //update the task that was assign to the engineer
            DO.Task updatedExsitingTask = new DO.Task(
                EngineerId: 0,
                Id: existingDoTaskWithBoEngineerId.Id,
                Alias: existingDoTaskWithBoEngineerId.Alias,
                Description: existingDoTaskWithBoEngineerId.Description,
                CreatedAtDate: existingDoTaskWithBoEngineerId.CreatedAtDate,
                RequiredEffortTime: existingDoTaskWithBoEngineerId.RequiredEffortTime,
                IsMilestone: existingDoTaskWithBoEngineerId.IsMilestone,
                Complexity: existingDoTaskWithBoEngineerId.Complexity,
                StartDate: existingDoTaskWithBoEngineerId.StartDate,
                ScheduledDate: existingDoTaskWithBoEngineerId.ScheduledDate,
                DeadlineDate: existingDoTaskWithBoEngineerId.DeadlineDate,
                CompleteDate: existingDoTaskWithBoEngineerId.CompleteDate,
                Deliverables: existingDoTaskWithBoEngineerId.Deliverables,
                Remarks: existingDoTaskWithBoEngineerId.Remarks);
            //
            _dal.Task.Update(updatedExsitingTask);

            //update the task that will be assign to the engineer
            DO.Task tempNewTaskFromDal = _dal.Task.Read(boUpdateEngineer.Task.Id)!;
            DO.Task updatedNewAssignTask = new DO.Task(EngineerId: boUpdateEngineer.Id,
                    Id: tempNewTaskFromDal.Id,
                    Alias: tempNewTaskFromDal.Alias,
                    Remarks: tempNewTaskFromDal.Remarks,
                    Description: tempNewTaskFromDal.Description,
                    CreatedAtDate: tempNewTaskFromDal.CreatedAtDate,
                    RequiredEffortTime: tempNewTaskFromDal.RequiredEffortTime,
                    IsMilestone: tempNewTaskFromDal.IsMilestone,
                    Complexity: tempNewTaskFromDal.Complexity,
                    StartDate: tempNewTaskFromDal.StartDate,
                    ScheduledDate: tempNewTaskFromDal.ScheduledDate,
                    DeadlineDate: tempNewTaskFromDal.DeadlineDate,
                    CompleteDate: tempNewTaskFromDal.CompleteDate,
                    Deliverables: tempNewTaskFromDal.Deliverables);

            _dal.Task.Update(updatedNewAssignTask);
        }
    }



}




