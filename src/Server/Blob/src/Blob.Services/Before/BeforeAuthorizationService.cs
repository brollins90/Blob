﻿using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using System.ServiceModel;
using System.Threading.Tasks;
using Blob.Contracts.Models;
using Blob.Contracts.ServiceContracts;
using Blob.Core.Authorization;

namespace Blob.Services.Before
{
    [ServiceBehavior]
    [GlobalErrorBehavior(typeof(GlobalErrorHandler))]
    public class BeforeAuthorizationService : IAuthorizationManagerService
    {
        [OperationBehavior]
        public Task<bool> CheckAccessAsync(AuthorizationContextDto context)
        {
            BlobClaimsAuthorizationManager am = new BlobClaimsAuthorizationManager();

            Collection<Claim> actions = new Collection<Claim>(context.Action.ToList());
            Collection<Claim> resources = new Collection<Claim>(context.Resource.ToList());

            return new Task<bool>(() => am.CheckAccess(new AuthorizationContext(context.Principal, resources, actions)));
        }
    }
}