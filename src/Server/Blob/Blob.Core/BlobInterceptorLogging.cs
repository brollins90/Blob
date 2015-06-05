using System;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Diagnostics;
using log4net;

namespace Blob.Core
{
    public class BlobInterceptorLogging : DbCommandInterceptor
    {
        private readonly ILog _log = LogManager.GetLogger("dblog");
        private readonly Stopwatch _stopwatch = new Stopwatch();

        private delegate void ExecutingMethod<T>(DbCommand command, DbCommandInterceptionContext<T> interceptionContext);
 
        public override void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            CommandExecuting<int>(base.NonQueryExecuting, command, interceptionContext);
        }
 
        public override void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            CommandExecuting<DbDataReader>(base.ReaderExecuting, command, interceptionContext);
        }
 
        public override void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            CommandExecuting<object>(base.ScalarExecuting, command, interceptionContext);
        }

        private void CommandExecuting<T>(ExecutingMethod<T> executingMethod, DbCommand command, DbCommandInterceptionContext<T> interceptionContext)
        {
            Stopwatch sw = Stopwatch.StartNew();
            executingMethod.Invoke(command, interceptionContext);
            sw.Stop();

            if (interceptionContext.Exception != null)
            {
                _log.Error(String.Format("Error executing command: {0}", command.CommandText), interceptionContext.Exception);
            }
            else
            {
                _log.Debug(String.Format("{0} took {1}", command.CommandText, sw.Elapsed.ToString()));
            }
        }
    }
}
