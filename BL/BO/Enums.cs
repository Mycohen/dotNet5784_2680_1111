﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public enum Status
    {
        Uncheduled=1,
        Scheduled,
        OnTrack, 
        InJeopardy,
        Done
    }
}