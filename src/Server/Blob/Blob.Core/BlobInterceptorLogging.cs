using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Repository.Hierarchy;

namespace Blob.Core
{
    public class BlobInterceptorLogging : DbCommandInterceptor
    {
        private readonly ILog _log = LogManager.GetLogger("dblog");
        private readonly Stopwatch _stopwatch = new Stopwatch();

        private delegate void ExecutingMethod<T>(System.Data.Common.DbCommand command, DbCommandInterceptionContext<T> interceptionContext);
 
        public override void NonQueryExecuting(System.Data.Common.DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            CommandExecuting<int>(base.NonQueryExecuting, command, interceptionContext);
        }
 
        public override void ReaderExecuting(System.Data.Common.DbCommand command, DbCommandInterceptionContext<System.Data.Common.DbDataReader> interceptionContext)
        {
            CommandExecuting<System.Data.Common.DbDataReader>(base.ReaderExecuting, command, interceptionContext);
        }
 
        public override void ScalarExecuting(System.Data.Common.DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            CommandExecuting<object>(base.ScalarExecuting, command, interceptionContext);
        }

        private void CommandExecuting<T>(ExecutingMethod<T> executingMethod, System.Data.Common.DbCommand command, DbCommandInterceptionContext<T> interceptionContext)
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

        //public override void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        //{
        //    base.ScalarExecuting(command, interceptionContext);
        //    _stopwatch.Restart();
        //}

        //public override void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        //{
        //    _stopwatch.Stop();
        //    if (interceptionContext.Exception != null)
        //    {
        //        _log.Error(string.Format("Error executing command: {0}", command.CommandText), interceptionContext.Exception);
        //    }
        //    else
        //    {
        //        _log.Debug(string.Format("Command: {0} \n Took: {1}", command.CommandText, _stopwatch.Elapsed));
        //        //_log.Debug("SQL Database", "SchoolInterceptor.ScalarExecuted", _stopwatch.Elapsed, "Command: {0}: ", command.CommandText);
        //    }
        //    base.ScalarExecuted(command, interceptionContext);
        //}

        //public override void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        //{
        //    base.NonQueryExecuting(command, interceptionContext);
        //    _stopwatch.Restart();
        //}

        //public override void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        //{
        //    _stopwatch.Stop();
        //    if (interceptionContext.Exception != null)
        //    {
        //        _log.Error(string.Format("Error executing command: {0}", command.CommandText), interceptionContext.Exception);
        //    }
        //    else
        //    {
        //        _log.Debug(string.Format("Command: {0} \n Took: {1}", command.CommandText, _stopwatch.Elapsed));
        //        //_log.Debug("SQL Database", "SchoolInterceptor.NonQueryExecuted", _stopwatch.Elapsed, "Command: {0}: ", command.CommandText);
        //    }
        //    base.NonQueryExecuted(command, interceptionContext);
        //}

        //public override void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        //{
        //    base.ReaderExecuting(command, interceptionContext);
        //    _stopwatch.Restart();
        //}

        //public override void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        //{
        //    _stopwatch.Stop();
        //    if (interceptionContext.Exception != null)
        //    {
        //        _log.Error(string.Format("Error executing command: {0}", command.CommandText), interceptionContext.Exception);
        //    }
        //    else
        //    {
        //        _log.Debug(string.Format("Command: {0} \n Took: {1}", command.CommandText, _stopwatch.Elapsed));
        //        //_log.Debug("SQL Database", "SchoolInterceptor.ReaderExecuted", _stopwatch.Elapsed, "Command: {0}: ", command.CommandText);
        //    }
        //    base.ReaderExecuted(command, interceptionContext);
        //}
    }
}
