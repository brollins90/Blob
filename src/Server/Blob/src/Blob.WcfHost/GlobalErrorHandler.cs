﻿using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using log4net;

namespace Blob.WcfHost
{
    public class GlobalErrorHandler : IErrorHandler
    {
        private readonly ILog _log;
        public GlobalErrorHandler()
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        // Provide a fault. The Message fault parameter can be replaced, or set to
        // null to suppress reporting a fault.
        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            var newEx = new FaultException(
                string.Format("Exception caught at GlobalErrorHandler{0}Method: {1}{2}Message:{3}",
                             Environment.NewLine, error.TargetSite.Name, Environment.NewLine, error.Message));

            MessageFault msgFault = newEx.CreateMessageFault();
            fault = Message.CreateMessage(version, msgFault, newEx.Action);
        }

        // HandleError. Log an error, then allow the error to be handled as usual.
        // Return true if the error is considered as already handled
        public bool HandleError(Exception error)
        {
            _log.Error(string.Format("Exception:{0}{1}Method: {2}{3}Message:{4}",
                                     error.GetType().Name, Environment.NewLine, error.TargetSite.Name,
                                     Environment.NewLine, error.Message + Environment.NewLine), error);
            return false;
        }
    }
}
