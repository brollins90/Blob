using System;
using Microsoft.Deployment.WindowsInstaller;

namespace BMonitor.Setup.CA
{
    public class CustomActions
    {
        [CustomAction]
        public static ActionResult PersistDeviceId(Session session)
        {
            ActionResult result = ActionResult.Failure;
            try
            {
                session.Log("Begin PersistDeviceId Custom Action");

                var regPath = session["REGPATH"];
                var deviceId = session["DEVICEID"];

                session.Log(String.Format("regPath: {0}, deviceId: {1}", regPath, deviceId));

                session.Log("End PersistDeviceId Custom Action");
                result = ActionResult.Success;
            }
            catch (Exception ex)
            {
                session.Log("ERROR in custom action PersistDeviceId {0}", ex.ToString());
            }
            return result;
        }
    }
}
