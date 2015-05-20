using Blob.Managers.Notification;
using Ninject.Modules;

namespace Behind.Service
{
    public class BehindServiceModule : NinjectModule
    {
        // http://spin.atomicobject.com/2014/03/14/ninject-binding-kernel-cleanup/
        public override void Load()
        {
            Bind<BehindService>().ToSelf();

            Bind<INotificationManager>().To<NotificationManager>().InSingletonScope();
        }
    }
}
