using System;
using System.ServiceModel.Configuration;

namespace Blob.Security
{
    public class WcfMessageLoggerExtension : BehaviorExtensionElement
    {
        protected override object CreateBehavior()
        {
            return new WcfMessageLogger();
        }

        public override Type BehaviorType
        {
            get
            {
                return typeof(WcfMessageLogger);
            }
        }
    }
}
