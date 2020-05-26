using System;
using System.Collections.Generic;
using System.Text;

namespace callMission.calls
{
    public class srvBase : IDisposable, Icall
    {
        //protected string paraTypeName = "";
        //protected string bodyJson = "";
        protected callExe ce = null;
        public srvBase()
        {
            ce = new callExe();
        }
        //public srvBase(string paraTypeName)
        //{
        //    this.paraTypeName = paraTypeName;
        //    ce = new callExe();
        //}

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TO DO: dispose managed state (managed objects).
                }

                // TO DO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TO DO: set large fields to null.

                disposedValue = true;
            }
        }

        // TO DO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~srvBase()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TO DO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        /// <summary>
        /// the do call procedure
        /// </summary>
        /// <param name="inputJson">call type in json</param>
        /// <returns>return type in json</returns>
        public virtual string doCall(string inputJson)
        {
            // never implement in base class
            throw new NotImplementedException();
        }
        #endregion
    }
}
