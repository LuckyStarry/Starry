using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Services.Core
{
    internal class CoreDebugger
    {
        public static void WriteLine(string format, params object[] args)
        {
#if DEBUG_CORE_DEBUGGER
            Console.WriteLine("[{0:MM:ss.fff}]-[{1}] {2}", DateTime.Now, System.Threading.Thread.CurrentThread.ManagedThreadId, string.Format(format, args));
#endif
        }
    }
}
