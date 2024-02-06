using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public record class TaskInEngineer
    (
       int Id,
       string? Alias = null
    )
    {
        public TaskInEngineer() : this(0) { }

    }
   
}
