using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation;
using BlApi;
using BO;

internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = Factory.Get;

    public int Create(Engineer item)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Engineer? Read(int id)
    {
        throw new NotImplementedException();
    }

    public Engineer? Read(Func<Task, bool> filter)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<TaskInEngineer> ReadAll()
    {
        throw new NotImplementedException();
    }

    public void Update(Engineer item)
    {
        throw new NotImplementedException();
    }
}
