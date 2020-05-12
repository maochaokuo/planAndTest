using callMission;
using models.calls;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace exeMission
{
    public class mainClass
    {
        protected clsMainLoopInput cmli = null;
        protected callExe ce = new callExe();

        public mainClass(clsMainLoopInput cmli)
        {
            this.cmli = cmli;
        }
        public string startLooping()
        {
            string ret = "";
            clsMainLoop cml=new clsMainLoop();
            ret = cml.theMainLoop(cmli);
            return ret;
        }
    }
}
