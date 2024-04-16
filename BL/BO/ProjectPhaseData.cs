using System;
using System.Xml.Serialization;

namespace BO
{
    /// <summary>
    /// Represents data about a phase in a project, including the phase type and the start date.
    /// </summary>
    public class ProjectPhaseData
    {
        /// <summary>
        /// Gets or sets the phase of the project, represented by an enumeration.
        /// </summary>
        [XmlElement("projectPhase")] // Specify custom XML element name for the 'Phase' property
        public Enums.projectPhase Phase { get; set; }

        /// <summary>
        /// Gets or sets the start date of the project phase.
        /// </summary>
        [XmlElement("projectStartDate")] // Specify custom XML element name for the 'StartedDate' property
        public DateTime StartedDate { get; set; }
    }
}
