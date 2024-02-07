using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface IMilestone
{
    public int Create(BO.Milestone item);
    public BO.Milestone? Read(Func<BO.Milestone, bool> filter);
    public BO.Milestone? Read(int id);
    public IEnumerable<BO.Milestone> ReadAll();
    public IEnumerable<BO.Milestone?> ReadAll(Func<BO.Milestone, bool>? filter = null);
    public void Update(BO.Milestone item);
    public void Delete(int id);
}
