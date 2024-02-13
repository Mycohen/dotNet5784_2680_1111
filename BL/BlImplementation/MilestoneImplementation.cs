using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation;
using BlApi;
using BO;

internal class MilestoneImplementation : IMilestone
{
    public int Create(Milestone item)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Milestone? Read(Func<Milestone, bool> filter)
    {
        throw new NotImplementedException();
    }

    public Milestone? Read(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Milestone> ReadAll()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Milestone?> ReadAll(Func<Milestone, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public void Update(Milestone item)
    {
        throw new NotImplementedException();
    }
}

