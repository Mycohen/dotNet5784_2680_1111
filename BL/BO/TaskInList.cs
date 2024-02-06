using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public record  TaskInList
    (int Id,
        string? Description = null,
        string? Alias = null,
         BO.Status Status = Status.Uncheduled

        )
    {
        public TaskInList() : this(0) { }
    }
}
