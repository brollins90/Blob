using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Blob.Contracts.Security;
using Microsoft.AspNet.Identity;

namespace Before.Infrastructure.Identity
{
    public class BeforeUser : UserDto, IUser
    {
        public BeforeUser()
        {
            Id = Guid.NewGuid().ToString();
        }

        public BeforeUser(UserDto userDto)
        {
            Id = userDto.Id;
            Email = userDto.Email;
            UserName = userDto.UserName;
        }

        public UserDto ToDto()
        {
            return new UserDto { Id = Id, Email = Email, UserName = UserName };
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(BeforeUserManager manager)
        {
            return await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie).ConfigureAwait(false);
        }

        public string PasswordHash { get; set; }
    }
}