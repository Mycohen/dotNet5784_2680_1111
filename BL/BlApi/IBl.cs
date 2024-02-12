using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface IBl
{
    public IEngineer Engineers { get; }
    public ITask Tasks { get; }
    public IMilestone Milestones { get; }
  
}
