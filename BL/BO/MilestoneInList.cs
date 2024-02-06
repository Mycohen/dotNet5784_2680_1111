using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public record MilestoneInList
    (   
        int Id,
        string? Description = null,
        string? Alias = null,
        Status Status = Status.Uncheduled,
        double CompletionPercentage = 0.0
        )
    {
        public MilestoneInList() : this(0) { }
    }
}
