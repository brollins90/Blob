using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace Blob.Contracts.Security
{
    public interface IAuthenticationManagerService
    {
        AuthenticationResponseChallenge AuthenticationResponseChallenge { get; set; }
        //
        // Summary:
        //     Exposes the security.SignIn environment value as a strong type.
        AuthenticationResponseGrant AuthenticationResponseGrant { get; set; }
        //
        // Summary:
        //     Exposes the security.SignOut environment value as a strong type.
        AuthenticationResponseRevoke AuthenticationResponseRevoke { get; set; }
        //
        // Summary:
        //     Returns the current user for the request
        ClaimsPrincipal User { get; set; }

        // Summary:
        //     Call back through the middleware to ask for a specific form of authentication
        //     to be performed on the current request
        //
        // Parameters:
        //   authenticationType:
        //     Identifies which middleware should respond to the request for authentication.
        //     This value is compared to the middleware's Options.AuthenticationType property.
        //
        // Returns:
        //     Returns an object with the results of the authentication. The AuthenticationResult.Identity
        //     may be null if authentication failed. Even if the Identity property is null,
        //     there may still be AuthenticationResult.properties and AuthenticationResult.Description
        //     information returned.
        Task<AuthenticateResult> AuthenticateAsync(string authenticationType);
        //
        // Summary:
        //     Called to perform any number of authentication mechanisms on the current
        //     request.
        //
        // Parameters:
        //   authenticationTypes:
        //     Identifies one or more middleware which should attempt to respond
        //
        // Returns:
        //     Returns the AuthenticationResult information from the middleware which responded.
        //     The order is determined by the order the middleware are in the pipeline.
        //     Latest added is first in the list.
        Task<IEnumerable<AuthenticateResult>> AuthenticateAsync(string[] authenticationTypes);
        //
        // Summary:
        //     Add information into the response environment that will cause the authentication
        //     middleware to challenge the caller to authenticate. This also changes the
        //     status code of the response to 401. The nature of that challenge varies greatly,
        //     and ranges from adding a response header or changing the 401 status code
        //     to a 302 redirect.
        //
        // Parameters:
        //   authenticationTypes:
        //     Identify which middleware should perform their alterations on the response.
        //     If the authenticationTypes is null or empty, that means the AuthenticationMode.Active
        //     middleware should perform their alterations on the response.
        void Challenge(params string[] authenticationTypes);
        //
        // Summary:
        //     Add information into the response environment that will cause the authentication
        //     middleware to challenge the caller to authenticate. This also changes the
        //     status code of the response to 401. The nature of that challenge varies greatly,
        //     and ranges from adding a response header or changing the 401 status code
        //     to a 302 redirect.
        //
        // Parameters:
        //   properties:
        //     Additional arbitrary values which may be used by particular authentication
        //     types.
        //
        //   authenticationTypes:
        //     Identify which middleware should perform their alterations on the response.
        //     If the authenticationTypes is null or empty, that means the AuthenticationMode.Active
        //     middleware should perform their alterations on the response.
        void Challenge(AuthenticationProperties properties, params string[] authenticationTypes);
        //
        // Summary:
        //     Lists all of the description data provided by authentication middleware that
        //     have been chained
        //
        // Returns:
        //     The authentication descriptions
        IEnumerable<AuthenticationDescription> GetAuthenticationTypes();
        //
        // Summary:
        //     Lists the description data of all of the authentication middleware which
        //     are true for a given predicate
        //
        // Parameters:
        //   predicate:
        //     A function provided by the caller which returns true for descriptions that
        //     should be in the returned list
        //
        // Returns:
        //     The authentication descriptions
        IEnumerable<AuthenticationDescription> GetAuthenticationTypes(Func<AuthenticationDescription, bool> predicate);
        //
        // Summary:
        //     Add information to the response environment that will cause the appropriate
        //     authentication middleware to grant a claims-based identity to the recipient
        //     of the response. The exact mechanism of this may vary.  Examples include
        //     setting a cookie, to adding a fragment on the redirect url, or producing
        //     an OAuth2 access code or token response.
        //
        // Parameters:
        //   identities:
        //     Determines which claims are granted to the signed in user. The ClaimsIdentity.AuthenticationType
        //     property is compared to the middleware's Options.AuthenticationType value
        //     to determine which claims are granted by which middleware. The recommended
        //     use is to have a single ClaimsIdentity which has the AuthenticationType matching
        //     a specific middleware.
        void SignIn(params ClaimsIdentity[] identities);
        //
        // Summary:
        //     Add information to the response environment that will cause the appropriate
        //     authentication middleware to grant a claims-based identity to the recipient
        //     of the response. The exact mechanism of this may vary.  Examples include
        //     setting a cookie, to adding a fragment on the redirect url, or producing
        //     an OAuth2 access code or token response.
        //
        // Parameters:
        //   properties:
        //     Contains additional properties the middleware are expected to persist along
        //     with the claims. These values will be returned as the AuthenticateResult.properties
        //     collection when AuthenticateAsync is called on subsequent requests.
        //
        //   identities:
        //     Determines which claims are granted to the signed in user. The ClaimsIdentity.AuthenticationType
        //     property is compared to the middleware's Options.AuthenticationType value
        //     to determine which claims are granted by which middleware. The recommended
        //     use is to have a single ClaimsIdentity which has the AuthenticationType matching
        //     a specific middleware.
        void SignIn(AuthenticationProperties properties, params ClaimsIdentity[] identities);
        //
        // Summary:
        //     Add information to the response environment that will cause the appropriate
        //     authentication middleware to revoke any claims identity associated the the
        //     caller. The exact method varies.
        //
        // Parameters:
        //   authenticationTypes:
        //     Identifies which middleware should perform the work to sign out.  Multiple
        //     authentication types may be provided to clear out more than one cookie at
        //     a time, or to clear cookies and redirect to an external single-sign out url.
        void SignOut(params string[] authenticationTypes);
        //
        // Summary:
        //     Add information to the response environment that will cause the appropriate
        //     authentication middleware to revoke any claims identity associated the the
        //     caller. The exact method varies.
        //
        // Parameters:
        //   properties:
        //     Additional arbitrary values which may be used by particular authentication
        //     types.
        //
        //   authenticationTypes:
        //     Identifies which middleware should perform the work to sign out.  Multiple
        //     authentication types may be provided to clear out more than one cookie at
        //     a time, or to clear cookies and redirect to an external single-sign out url.
        void SignOut(AuthenticationProperties properties, params string[] authenticationTypes);

        //Ext
        ClaimsIdentity CreateTwoFactorRememberBrowserIdentity(string userId);
        IEnumerable<AuthenticationDescription> GetExternalAuthenticationTypes();
        Task<ClaimsIdentity> GetExternalIdentityAsync(string externalAuthenticationType);
        ExternalLoginInfo GetExternalLoginInfo();
        ExternalLoginInfo GetExternalLoginInfo(string xsrfKey, string expectedValue);
        Task<ExternalLoginInfo> GetExternalLoginInfoAsync();
        Task<ExternalLoginInfo> GetExternalLoginInfoAsync(string xsrfKey, string expectedValue);
        Task<bool> TwoFactorBrowserRememberedAsync(string userId);

    }
}
