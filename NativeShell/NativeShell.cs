using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NativeShell
{
    public enum LogType
    {
        Error,
        Warning,
        Trace
    }

    public class NativeShell
    {

        public static NativeShell Instance { get; } = new NativeShell();

        public Action<LogType, string> Log = delegate { };

    }
}
