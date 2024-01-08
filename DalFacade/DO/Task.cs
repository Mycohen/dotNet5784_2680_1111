namespace DO;

public record Task(
    int Id,
    string? Alias = null,
    string? Description = null,
    DateTime? CreatedAtDate = null,
    TimeSpan? RequiredEffortTime = null,
    bool IsMilestone = false,
    DO.EngineerExperience Complexity=EngineerExperience.Intermediate,
    DateTime? StartDate = null,
    DateTime? ScheduledDate = null,
    DateTime? DeadlineDate = null,
    DateTime? CompleteDate = null,
    string? Deliverables = null,
    string? Remarks = null,
    int EngineerId =0
    )
{
public Task ():this(0) { }

}


