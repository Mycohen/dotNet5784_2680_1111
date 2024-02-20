﻿namespace BlApi;

public interface ITask
{
    public int Create(BO.Task item);
    public BO.Task? Read(int id);
    public IEnumerable<BO.Task?> ReadAll(Func<BO.Task, bool>? filter = null);
    public void Update(BO.Task item);//Don't forget the bool func for StartDate
    public void Delete(int id);

}





