using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BO;

public class TaskInList
{
    // Properties
    public int Id { get; init; }
    public string? Description { get; set; }
    public string? Alias { get; set; }
    public Enums.Status Status { get; set; }

}

