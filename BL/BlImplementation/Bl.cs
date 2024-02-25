using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation;
using BlApi;
internal class Bl : IBl
{
    public ITask Tasks => new TaskImplementation();
    public IEngineer Engineers => new EngineerImplementation();
    public IMilestone Milestones => new MilestoneImplementation();
}

   

