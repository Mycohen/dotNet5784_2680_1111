namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        Engineer? DeletetEngineer=(Read(id));
        if (DeletetEngineer == null) throw new InvalidOperationException("ERROR: the engineer doesn't exist🙈");
        DataSource.Engineers.Remove(DeletetEngineer);

    }

    public Engineer? Read(int id)
    {
       Engineer? result =DataSource.Engineers.FirstOrDefault(x => x.Id == id);
        if (result != null) 
            return result;
        return null;
            
    }

    public List<Engineer> ReadAll()
    {
        throw new NotImplementedException();
    }

    public void Update(Engineer item)
    {
       Delete(item.Id);

    }
}
