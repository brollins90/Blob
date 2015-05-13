//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Security.Claims;
//using System.ServiceModel;
//using System.Text;
//using System.Threading.Tasks;
//using Blob.Contracts.Security;

//namespace Blob.Security.Authorization
//{
//    [ServiceBehavior]
//    public class BlobAuthorizationManagerService : IAuthorizationManagerService
//    {
//        public Task<bool> CheckAccessAsync(AuthorizationContextDto context)
//        {
//            var am = new BlobClaimsAuthorizationManager();

//            System.Collections.ObjectModel.Collection<Claim> actions = new Collection<Claim>(context.Action.ToList());
//            System.Collections.ObjectModel.Collection<Claim> resources = new Collection<Claim>(context.Resource.ToList());
                
//            return new Task<bool>(() => am.CheckAccess(new AuthorizationContext(context.Principal, resources, actions)));
//        }
//    }
//}
