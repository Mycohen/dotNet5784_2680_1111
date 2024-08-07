﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

// Nested static class containing configuration constants for the data source
internal static class Config
{

    static string s_data_config_xml = "data-config";

    // Property to get the next task ID by calling the XMLTools.GetAndIncreaseNextId method
    internal static int NextTaskId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextTaskId"); }

    // Property to get the next dependency ID by calling the XMLTools.GetAndIncreaseNextId method
    internal static int NextDepId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextDepId"); }
}


