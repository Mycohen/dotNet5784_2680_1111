using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface IEngineer
{
    public int Create(BO.Engineer item);
    public BO.Task? Read(int id);
    public IEnumerable<BO.TaskInEngineer> ReadAll();
    public void Update(BO.Engineer item);
    public void Delete(int id);
}
