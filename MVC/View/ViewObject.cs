using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MVC.View
{
    public abstract class ViewObject : IDisposable
    {
        public int StatusCode { protected set; get; } = 200;
        public string Content { protected set; get; } = string.Empty;
        public string ContentType { get; protected set; } = string.Empty;

        public virtual void Respond(HttpListenerResponse response)
        {
            response.StatusCode = this.StatusCode;
            response.ContentType = "text/html";
            if (ContentType != string.Empty) response.ContentType = ContentType;
            WriteToStream(response);
        }

        protected virtual void WriteToStream(HttpListenerResponse response)
        {
            var buffer = System.Text.Encoding.UTF8.GetBytes(this.Content);
            response.ContentLength64 = buffer.Length;

            var output = response.OutputStream;

            output.Write(buffer, 0, buffer.Length);

            output.Close();
        }


        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ViewObject() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
