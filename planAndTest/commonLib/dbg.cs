using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace commonLib
{
    public class dbg
    {
        protected readonly string DBG_PATH = @"C:\TEMP\";
        protected readonly string DBG_FILE = "o.t";

        public dbg()
        {
        }
        public dbg(string dbgPath, string dbgFile="o.t")
        {
            DBG_PATH = dbgPath;
            DBG_FILE = dbgFile;
        }
        public void output(string outStr)
        {
            StreamWriter sw = new StreamWriter(
                Path.Combine( DBG_PATH, DBG_FILE), true);
            sw.WriteLine(outStr);
            sw.Close();
            sw = null;
        }
    }
}
