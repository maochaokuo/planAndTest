using System;
using System.Collections.Generic;
using System.Text;

namespace SASDconstants
{
    public enum articleStatus
    {
        New=1,
        Open=2,
        Assigned=3,
        Resolved=4,
        Closed=5, // finished
        Removed=6, // not finished
        Suspended=7 // not living not dead
    }
}
