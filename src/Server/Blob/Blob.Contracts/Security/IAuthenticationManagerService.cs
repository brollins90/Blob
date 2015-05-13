using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Blob.Contracts.Security
{
    [ServiceContract]
    public interface IAuthenticationManagerService
    {
        // Summary:
        //     Exposes the security.Challenge environment value as a strong type.
        AuthenticationResponseChallengeDto AuthenticationResponseChallenge { [OperationContract] get; }
        //
        // Summary:
        //     Exposes the security.SignIn environment value as a strong type.
        AuthenticationResponseGrantDto AuthenticationResponseGrant { [OperationContract] get; }
        //
        // Summary:
        //     Exposes the security.SignOut environment value as a strong type.
        AuthenticationResponseRevokeDto AuthenticationResponseRevoke { [OperationContract] get; }
        //
        // Summary:
        //     Returns the current user for the request
        ClaimsPrincipal User { [OperationContract] get; set; }

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
        [OperationContract(Name = "AuthenticateAsyncSingle")]
        Task<AuthenticateResultDto> AuthenticateAsync(string authenticationType);
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
        [OperationContract(Name = "AuthenticateAsyncMulti")]
        Task<IEnumerable<AuthenticateResultDto>> AuthenticateAsync(string[] authenticationTypes);
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
        [OperationContract(Name = "ChallengeAsync1")]
        Task ChallengeAsync(params string[] authenticationTypes);
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
        [OperationContract(Name = "ChallengeAsync2")]
        Task ChallengeAsync(AuthenticationPropertiesDto properties, params string[] authenticationTypes);
        //
        // Summary:
        //     Lists all of the description data provided by authentication middleware that
        //     have been chained
        //
        // Returns:
        //     The authentication descriptions
        [OperationContract(Name = "GetAuthenticationTypesAsync1")]
        Task<IEnumerable<AuthenticationDescriptionDto>> GetAuthenticationTypesAsync();
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
        [OperationContract(Name = "GetAuthenticationTypesAsync2")]
        Task<IEnumerable<AuthenticationDescriptionDto>> GetAuthenticationTypesAsync(Func<AuthenticationDescriptionDto, bool> predicate);
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
        [OperationContract(Name = "SignInAsync1")]
        Task SignInAsync(params ClaimsIdentity[] identities);
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
        [OperationContract(Name = "SignInAsync2")]
        Task SignInAsync(AuthenticationPropertiesDto properties, params ClaimsIdentity[] identities);
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
        [OperationContract(Name = "SignOutAsync1")]
        Task SignOutAsync(params string[] authenticationTypes);
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
        [OperationContract(Name = "SignOutAsync2")]
        Task SignOutAsync(AuthenticationPropertiesDto properties, params string[] authenticationTypes);
    }
}
