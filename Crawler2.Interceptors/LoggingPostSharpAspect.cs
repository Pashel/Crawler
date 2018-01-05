using System;
using System.IO;
using PostSharp.Aspects;

namespace Crawler2.Interceptors
{
    [Serializable]
    public class LoggingPostSharpAspect : OnMethodBoundaryAspect
    {
        public override void OnEntry(MethodExecutionArgs args)
        {
            // Before invoking the method on the original target.
            WriteLog(String.Format(
                "Invoking method {0} at {1}",
                args.Method, DateTime.Now.ToLongTimeString()));

            args.FlowBehavior = FlowBehavior.Default;
        }

        public override void OnSuccess(MethodExecutionArgs args)
        {
            WriteLog(String.Format(
                "Method {0} returned {1} at {2}",
                args.Method, args.ReturnValue?.GetType(),
                DateTime.Now.ToLongTimeString()));
        }

        public override void OnException(MethodExecutionArgs args)
        {
            WriteLog(String.Format(
                "Method {0} threw exception {1} at {2}",
                args.Method, args.Exception.Message,
                DateTime.Now.ToLongTimeString()));
        }

        private void WriteLog(string message)
        {
            using (var writer = new StreamWriter("Log-PostSharp.txt", true)) {
                writer.WriteLine(message);
            }
        }
    }
}
