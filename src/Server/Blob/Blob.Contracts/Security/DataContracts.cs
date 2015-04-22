using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Claims;

namespace Blob.Contracts.Security
{
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
        public bool Succeeded { get; private set; }

        [DataMember]
        public IEnumerable<string> Errors
        {
            get { return _errors ?? (_errors = new List<string>()); }
            set { _errors = value; }
        }
        private IEnumerable<string> _errors;

        //public IdentityResultDto(bool success)
        //{
        //    Succeeded = success;
        //    Errors = new string[0];
        //}

        //public IdentityResultDto(IEnumerable<string> errors)
        //{
        //    if (errors == null)
        //    {
        //        errors = new[] { "error" };
        //    }
        //    Succeeded = false;
        //    Errors = errors;
        //}
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
        public string UserName { get; set; }
    }


    [DataContract]
    public class UserLoginInfoDto
    {
        [DataMember]
        public string LoginProvider { get; set; }
        [DataMember]
        public string ProviderKey { get; set; }

        //public UserLoginInfoDto(string loginProvider, string providerKey)
        //{
        //    LoginProvider = loginProvider;
        //    ProviderKey = providerKey;
        //}
    }
}
