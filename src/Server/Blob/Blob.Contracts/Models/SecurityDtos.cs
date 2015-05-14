using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Claims;

namespace Blob.Contracts.Models
{
    [DataContract]
    public class AuthenticateResultDto
    {
        [DataMember]
        public AuthenticationDescriptionDto Description { get; set; }
        [DataMember]
        public ClaimsIdentity Identity { get; set; }
        [DataMember]
        public AuthenticationPropertiesDto Properties { get; set; }
    }
    
    [DataContract]
    public class AuthenticationDescriptionDto
    {
        [DataMember]
        public string AuthenticationType { get; set; }
        [DataMember]
        public string Caption { get; set; }
        [DataMember]
        public IDictionary<string, object> Properties { get; set; }
    }

    [DataContract]
    public class AuthenticationPropertiesDto
    {
        [DataMember]
        public bool? AllowRefresh { get; set; }
        [DataMember]
        public IDictionary<string, string> Dictionary { get; set; }
        [DataMember]
        public DateTimeOffset? ExpiresUtc { get; set; }
        [DataMember]
        public bool IsPersistent { get; set; }
        [DataMember]
        public DateTimeOffset? IssuedUtc { get; set; }
        [DataMember]
        public string RedirectUri { get; set; }
    }


    [DataContract]
    public class AuthenticationResponseChallengeDto
    {
        public string[] AuthenticationTypes { get; set; }
        public AuthenticationPropertiesDto Properties { get; set; }
    }


    [DataContract]
    public class AuthenticationResponseGrantDto
    {
        [DataMember]
        public ClaimsIdentity Identity { get; set; }
        [DataMember]
        public ClaimsPrincipal Principal { get; set; }
        [DataMember]
        public AuthenticationPropertiesDto Properties { get; set; }
    }


    [DataContract]
    public class AuthenticationResponseRevokeDto
    {
        [DataMember]
        public string[] AuthenticationTypes { get; set; }
        [DataMember]
        public AuthenticationPropertiesDto Properties { get; set; }
    }


    [DataContract]
    public class AuthorizationContextDto
    {
        [DataMember]
        public IEnumerable<Claim> Action { get; set; }
        [DataMember]
        public IEnumerable<Claim> Resource { get; set; }
        [DataMember]
        public ClaimsPrincipal Principal { get; set; }

        public AuthorizationContextDto(string action, string resource, ClaimsPrincipal principal)
        {
            Action = new List<Claim> { new Claim("action", action) };
            Principal = principal;
            Resource = new List<Claim> { new Claim("resource", resource) };
        }
    }


    [DataContract]
    public class ExternalLoginInfoDto
    {
        [DataMember]
        public string DefaultUserName { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public ClaimsIdentity ExternalIdentity { get; set; }

        [DataMember]
        public UserLoginInfoDto Login { get; set; }
    }


    [DataContract]
    public class IdentityResultDto
    {
        [DataMember]
        public bool Succeeded { get; set; }

        [DataMember]
        public IEnumerable<string> Errors
        {
            get { return _errors ?? (_errors = new List<string>()); }
            set { _errors = value; }
        }
        private IEnumerable<string> _errors;
    }


    [DataContract]
    public enum SignInStatusDto
    {
        [EnumMember]
        Success = 0,
        [EnumMember]
        LockedOut = 1,
        [EnumMember]
        RequiresVerification = 2,
        [EnumMember]
        Failure = 3,
    }


    [DataContract]
    public class UserDto
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string UserName { get; set; }
    }


    [DataContract]
    public class UserLoginInfoDto
    {
        [DataMember]
        public string LoginProvider { get; set; }
        [DataMember]
        public string ProviderKey { get; set; }
    }

}