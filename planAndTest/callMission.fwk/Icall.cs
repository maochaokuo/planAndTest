using System;
using System.Collections.Generic;
using System.Text;

namespace callMission
{
    public interface Icall
    {
        string doCall(string callId, string inputJson);
    }
}
