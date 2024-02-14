using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation;
//using BlApi;
//using BO;

internal class EngineerImplementation : BlApi.IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    // function for converting a DO.Engineer to BO.Engineer


    public int Create(BO.Engineer item)
    {
        try
        {

        }
        catch 
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public BO.Engineer Read(int id)
    {
        DO.Engineer doEngineer = _dal.Engineer.Read(id) ?? throw new BO.BlDoesNotExistExeption($"Engineer with {id} alredy exist");
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

    public void Update(BO.Engineer item)
    {
        throw new NotImplementedException();
    }


    private BO.Engineer convertFromDoToBo(DO.Engineer engineer)
    {
        return new BO.Engineer
        {
            Id = engineer.Id,
            Name = engineer.Name,
            Email = engineer.Email,
            Cost = engineer.Cost,
            Level = engineer.Level,
            Task = MapTask(engineer.Id)
        };
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

    private 
}





