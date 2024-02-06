using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public record Task(

           int Id,
           string? Description = null,
           string? Alias = null,
           DateTime? CreatedAtDate = null,
           BO.Status Status = Status.Uncheduled,
           List <TaskInList> ? Dependencies = null,
           MilestoneInTask? Milestone = null,
           TimeSpan? RequiredEffortTime = null,
           DateTime? StartDate = null,
           DateTime? ScheduledDate = null,
           DateTime ? ForecastDate = null,
           DateTime? DeadlineDate = null,
           DateTime? CompleteDate = null,
           string? Deliverables = null,
           string? Remarks = null,
           EngineerInTask? Engineer = null,
           DO.EngineerExperience Complexity = EngineerExperience.Intermediate

    
       )
    {
        public Task() : this(0) { }
    }
}
