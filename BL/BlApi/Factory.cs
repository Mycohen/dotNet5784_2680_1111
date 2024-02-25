using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

internal static class Factory
{
    public static IBl Get() => new BlImplementation.Bl();
}
