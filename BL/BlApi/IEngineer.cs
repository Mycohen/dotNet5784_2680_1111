using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface IEngineer
{
    public int Create(BO.Engineer item);
    public BO.Engineer Read(int id);
    //public BO.Engineer? ReadAll(Func<BO.Engineer, bool> filter);
    //public IEnumerable<BO.TaskInEngineer> ReadAll();
    public IEnumerable<BO.Engineer?> ReadAll(Func<BO.Engineer, bool>? filter );
    public void Update(BO.Engineer item);
    public void Delete(int id);
    public void deleteAll();

}
