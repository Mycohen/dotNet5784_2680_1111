using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BO;

public class Engineer
{
    // Properties with public getters and setters
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public double? Cost { get; set; }
    public EngineerExperience? Level { get; set; }
    public BO.TaskInEngineer? Task { get; set; }

   
}
