namespace DalApi;
using DO;

public interface ITask : ICrud<Task>
{
   
    void DeleteAll();//Help function for removing all the items.
}

