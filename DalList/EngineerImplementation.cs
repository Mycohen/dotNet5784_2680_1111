namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
        Engineer newEngineer= new Engineer(item.Id,item.Email,item.Cost,item.Name,item.Level);
       foreach (var _engineer in DataSource.Engineers)
        {
            if(_engineer.Id == item.Id)
                throw new InvalidOperationException("ERRROR: an Engineer with suhch ID already exist");
        }
            
        DataSource.Engineers.Add(newEngineer);
        return newEngineer.Id;
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
        return new List<Engineer>(DataSource.Engineers); // Return a copy of the dependencies collection
    }

    public void Update(Engineer item)
    {
       Engineer? deletedEngineer = Read(item.Id);
        if (deletedEngineer == null)
            throw new InvalidOperationException("ERROR: the engineer doesn't exist🙈");
        DataSource.Engineers.Remove(deletedEngineer);
        DataSource.Engineers.Add(item);

    }
}
