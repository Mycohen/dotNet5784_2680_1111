namespace BlImplementation;
using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

internal class TaskImplementation:ITask
{
    private DalApi.IDal _dal = Factory.Get;

    public int Create(Task item)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task? Read(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Task?> ReadAll(Func<Task, bool>? filter = null)
    {
        if(filter == null)
            return (from DO.Task doTask in _dal.Task.ReadAll()
                                       select new Task(doTask)).ToList();
        return (from DO.Task doTask in _dal.Task.ReadAll()
                select 

    }
    public void Update(Task item)
    {
        throw new NotImplementedException();
    }
}

    
