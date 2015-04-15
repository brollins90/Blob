using System;
using System.Configuration;
using System.Linq;
using System.Web.Security;
using Blob.Core.Domain;

namespace Blob.Security
{
    public class BlobMembershipUser : MembershipUser
    {
        public long Id { get; set; }
        public long CustomerId { get; set; }

        public BlobMembershipUser(string providerName, string name, object providerUserKey, string email, string passwordQuestion, 
            string comment, bool isApproved, bool isLockedOut, DateTime creationDate, DateTime lastLoginDate, DateTime lastActivityDate, 
            DateTime lastPasswordChangedDate, DateTime lastLockoutDate, long customerId)
            : base(providerName, name, providerUserKey, email, passwordQuestion, comment, isApproved, isLockedOut, creationDate,
            lastLoginDate,lastActivityDate, lastPasswordChangedDate, lastLockoutDate)
        {
            CustomerId = customerId;
        }

        public BlobMembershipUser() { }

        public void Delete(BlobMembershipUser mUser)
        {
            User user = GetUserById(mUser.Id);
            Membership.DeleteUser(user.Username);
        }

        public void Update(BlobMembershipUser mUser)
        {
            throw new NotImplementedException();
        }

        public static User GetUserById(long id)
        {
            try
            {
                using (Data.BlobDbContext context = new Data.BlobDbContext(ConfigurationManager.ConnectionStrings["BlobDbContext"].ConnectionString))
                {
                    return context.Set<User>().FirstOrDefault(u => u.Id.Equals(id));
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
