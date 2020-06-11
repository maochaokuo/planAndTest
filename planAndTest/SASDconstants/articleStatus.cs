using System;
using System.Collections.Generic;
using System.Text;

namespace SASDconstants
{
    public enum articleStatus
    {
        NEW=1,
        OPEN=2,
        ASSIGNED=3,
        RESOLVED=4,
        CLOSED=5, // finished
        REMOVED=6, // not finished
        SUSPENDED=7 // not living not dead
    }
}
