using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BO;

public class ProjectPhaseData
{
    [XmlElement("projectPhase")] // Specify custom element name for the 'Phase' property
    public Enums.projectPhase Phase { get; set; }

    [XmlElement("projectStartDate")] // Specify custom element name for the 'StartedDate' property
    public DateTime StartedDate { get; set; }
}
