using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public record Milestone(

           int Id,
           string? Description = null,
           string? Alias = null,
           DateTime? CreatedAtDate = null,
           Status Status = Status.Uncheduled,
           DateTime? ForecastDate = null,
           DateTime? DeadlineDate = null,
           DateTime? CompleteDate = null,
           string? Remarks = null,
          List <TaskInList> ? Dependencies = null

       )

    {
        public Milestone() : this(0) { }
    }
}
