using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BO;

public class Task
{
    // Properties
    public int Id { get; init; }
    public string? Description { get; set; }
    public string? Alias { get; set; }
    public DateTime? CreatedAtDate { get; init; }
    public Enums.Status Status { get; set; }
    // List of tasks that this task depends on
    public List<TaskInList>? Dependencies { get; set; }
    public MilestoneInTask? Milestone { get; set; }
    public TimeSpan? RequiredEffortTime { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? ScheduledDate { get; set; }
    public DateTime? ForecastDate { get; set; }
    public DateTime? DeadlineDate { get; set; }
    public DateTime? CompleteDate { get; set; }
    public string? Deliverables { get; set; }
    public string? Remarks { get; set; }
    public EngineerInTask? Engineer { get; set; }
    public EngineerExperience Complexity { get; set; }

   
}
