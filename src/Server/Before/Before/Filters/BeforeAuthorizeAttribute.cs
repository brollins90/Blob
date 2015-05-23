using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Before.Infrastructure.Extensions;
using Blob.Contracts.Models;

namespace Before.Filters
{
    public class BeforeAuthorizeAttribute : AuthorizeAttribute
    {
        public string Operation { get; set; }
        public string Resource { get; set; }
        
        public BeforeAuthorizeAttribute() { }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return true;
            //if (!string.IsNullOrWhiteSpace(Operation))
            //{
            //    return CheckAccess(httpContext, Operation, Resource);
            //}
            //else
            //{
            //    var controller = httpContext.Request.RequestContext.RouteData.Values["controller"] as string;
            //    var action = httpContext.Request.RequestContext.RouteData.Values["action"] as string;

            //    return CheckAccess(httpContext, action, controller);
            //}
        }

        protected virtual bool CheckAccess(HttpContextBase httpContext, string action, string resource)
        {
            AuthorizationContextDto context = new AuthorizationContextDto(action, resource, ClaimsPrincipal.Current);
            Task<bool> task = httpContext.CheckAccessAsync(context);

            if (task.Wait(50000))
                return task.Result;
            throw new TimeoutException();
        }

        public new string Roles
        {
            get
            {
                throw new NotSupportedException();
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public new string Users
        {
            get
            {
                throw new NotSupportedException();
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = (ActionResult)new HttpUnauthorizedResult();
        }
    }
}
