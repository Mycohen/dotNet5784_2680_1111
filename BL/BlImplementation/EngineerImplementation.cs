using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using BlApi;
namespace BlImplementation;


internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public readonly BlApi.ITask _taskImplementation = new TaskImplementation();
    // function for converting a DO.Engineer to BO.Engineer


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
            throw new BlDalError($"unrecognized exception from DAL {ex}", ex);
        }
        finally
        {
            Console.WriteLine("Data from the BAL was successfully saved in DAL");
        }
        return boEngineer.Id;
    }

    public void Delete(int id)
    {
        BO.Engineer boEngineer = this.Read(id);
        if (boEngineer == null)
            throw new BO.BlDoesNotExistExeption($"Engineer with {id} does not exist");
        else if (boEngineer.Task != null)
            throw new BO.BlEngineerHasTaskExeption($"Engineer with {id} has a task and can not be deleted");
        else if (_dal.Task.ReadAll(task => task.EngineerId == id) != null)
            throw new BO.BlEngineerHasTaskExeption($"Engineer with {id} has a task and can not be deleted");
        else
        _dal.Engineer.Delete(id);
    }

    public BO.Engineer Read(int id)
    {
        DO.Engineer doEngineer = _dal.Engineer.Read(id) ?? throw new BO.BlDoesNotExistExeption($"Engineer with {id} does not exist");
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
            DO.Engineer doEngineer = _dal.Engineer.Read(boEngineer.Id) ?? throw new BO.BlDoesNotExistExeption($"Engineer with such ID = {boEngineer.Id} does not excist");
            CheckIfValidEngineerData(boEngineer);
            if (doEngineer.Level > boEngineer.Level)
            {
                throw new BlInvalidInputException($"The level of the updated Engineer should not be lower than the current level: current level is {doEngineer.Level} entered level was {boEngineer.Level}");
            }
            
            updateTaskThatAssignedToEngineer(boEngineer);

        }
        catch (Exception ex)
        {
            throw new BlDalError($"unrecognized exception from DAL {ex}", ex);
        }
        finally
        {
            Console.WriteLine("Data from the BAL was successfully saved in DAL");
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
            (Id: boEngineer.Id, Email: boEngineer.Email, Cost: boEngineer.Cost, Name: boEngineer.Name, Level: boEngineer.Level);
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
            throw new BlInvalidInputException($"Invalid Input of Engineer ID value. The value must be greater than 0. Entered value is {boEngineer.Id}");
        if (boEngineer.Name == null || boEngineer.Name == "")
            throw new BlInvalidInputException($"Invalid input of Engineer Name value. The value must be a non empty string");
        if (boEngineer.Cost <= 0)
            throw new BlInvalidInputException($"Invalid Input of Engineer Cost value. The value must be greater than 0. Entered value is {boEngineer.Cost}");
        if (boEngineer.Email == null)
            throw new BlNullException("The value of Engineer Email address must be entered (can not be null).");
        else
        if (!IsValidEmail(boEngineer.Email))
            throw new BlInvalidInputException("The value of Engineer Email address must be entered.");
        else
            return true;
    }

   private void updateTaskThatAssignedToEngineer(BO.Engineer boEngineer)
   {
        //if the engineer has a task
        if (boEngineer.Task != null)
        {

            //if there is a chenge in the task assign to the engineer then
            //1. chenge the task1.EngineerID to null 
            //2. chenge the task2.EngineerID to the ID of engineer.

            // the task that curently assign to the engineer 
            DO.Task currentDoTask = _dal.Task.Read(boEngineer.Task.Id)!;
            if (currentDoTask.Id != boEngineer.Task.Id)
            {
                //get the task that is currently assign to the engineer
                DO.Task newDoTask = _dal.Task.Read(boEngineer.Task.Id)!;
                //update the task that was assign to the engineer
                DO.Task updatedCurrentTask = new DO.Task(EngineerId: 0, Id: currentDoTask.Id, Alias: currentDoTask.Alias,
                    Description: currentDoTask.Description, CreatedAtDate: currentDoTask.CreatedAtDate, RequiredEffortTime: currentDoTask.RequiredEffortTime,
                    IsMilestone: currentDoTask.IsMilestone, Complexity: currentDoTask.Complexity, StartDate: currentDoTask.StartDate, ScheduledDate: currentDoTask.ScheduledDate,
                    DeadlineDate: currentDoTask.DeadlineDate, CompleteDate: currentDoTask.CompleteDate, Deliverables: currentDoTask.Deliverables,
                    Remarks: currentDoTask.Remarks);
                //update the task that will be assign to the engineer
                DO.Task updatedNewTask = new DO.Task(EngineerId: boEngineer.Id, Id: newDoTask.Id, Alias: newDoTask.Alias, Remarks: newDoTask.Remarks,
                        Description: newDoTask.Description, CreatedAtDate: newDoTask.CreatedAtDate, RequiredEffortTime: newDoTask.RequiredEffortTime,
                        IsMilestone: newDoTask.IsMilestone, Complexity: newDoTask.Complexity, StartDate: newDoTask.StartDate, ScheduledDate: newDoTask.ScheduledDate,
                        DeadlineDate: newDoTask.DeadlineDate, CompleteDate: newDoTask.CompleteDate, Deliverables: newDoTask.Deliverables);
                _dal.Task.Update(updatedCurrentTask);
                _dal.Task.Update(updatedNewTask);
            }

        }
    }
}




