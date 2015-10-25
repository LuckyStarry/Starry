using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Services.EventArgs
{
    public class ExceptionHappendEventArgs : ServiceEventArgs
    {
        public ExceptionHappendEventArgs(Exception exception)
        {
            this.Exception = exception;
        }

        public Exception Exception { private set; get; }
    }
}
