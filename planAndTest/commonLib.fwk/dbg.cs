using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace commonLib
{
    public class dbg : IDisposable
    {
        protected readonly static string DBG_PATH = @"C:\TEMP\";
        protected readonly static string DBG_FILE = "o.t";
        protected string dbgPath = "";
        protected string dbgFile = "";
        private bool disposedValue;
        public dbg()
        {
            dbgPath = DBG_PATH;
            dbgFile = DBG_FILE;
        }
        public dbg(string dbgPath, string dbgFile="o.t")
        {
            this.dbgPath = dbgPath;
            this.dbgFile = dbgFile;
        }
        protected static bool busy = false;
        public static string o(string outS)
        {
            string ret = "";
            while (busy)
                Thread.Sleep(50);
            busy = true;
            try
            {
                StreamWriter sw = new StreamWriter(
                    Path.Combine(DBG_PATH, DBG_FILE), true);
                string sout = $"{DateTime.Now.ToString("HH:mm:ss")}: {outS}";
                sw.WriteLine(sout);
                sw.Close();
                sw = null;
                Console.WriteLine(sout);
            }
            catch(Exception ex)
            {
                ret = ex.Message;
            }
            busy = false;
            return ret;
        }
        public void t(string outStr)
        {
            StreamWriter sw = new StreamWriter(
                Path.Combine( DBG_PATH, DBG_FILE), true);
            string sout = $"{DateTime.Now.ToString("HH:mm:ss")} {outStr}";
            sw.WriteLine(sout);
            sw.Close();
            sw = null;
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TO DO: dispose managed state (managed objects)
                }

                // TO DO: free unmanaged resources (unmanaged objects) and override finalizer
                // TO DO: set large fields to null
                disposedValue = true;
            }
        }

        // // TO DO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~dbg()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
