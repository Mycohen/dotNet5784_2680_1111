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
            //if there is a task assigne to the engineer 
            if (boEngineer.Task != null)
            {
                BO.Task boTask = _taskImplementation.Read(task => task.EngineerId == boEngineer.Id);
                
            }

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

    private bool CheckTheProjectStatus()
    {
        if (_dal.Task.ReadAll() != null)
            foreach (DO.Task task in _dal.Task.ReadAll().ToList()!)
            {
                if (task.StartDate == null)
                    return false;
            }
        else
        {
            return false;
        }
        return true;
    }
}




