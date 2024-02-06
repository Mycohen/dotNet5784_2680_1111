using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
   public record MilestoneInTask
    (int Id,
       string? Alias=null)
    { 
        
        public MilestoneInTask() : this(0) { } }
}
