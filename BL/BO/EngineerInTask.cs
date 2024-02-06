﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public record EngineerInTask
    (int Id,
        string? Name = null)
    {
        public EngineerInTask() : this(0) { }
    }
}