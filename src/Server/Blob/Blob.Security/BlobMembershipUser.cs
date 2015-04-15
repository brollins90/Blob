using System;
using System.Web.Security;

namespace Blob.Security
{
    public class BlobMembershipUser : MembershipUser
    {
        public string CustomerId { get; set; }

        public BlobMembershipUser(string providerName, string name, object providerUserKey, string email, string passwordQuestion, 
            string comment, bool isApproved, bool isLockedOut, DateTime creationDate, DateTime lastLoginDate, DateTime lastActivityDate, 
            DateTime lastPasswordChangedDate, DateTime lastLockoutDate, string customerId)
            : base(providerName, name, providerUserKey, email, passwordQuestion, comment, isApproved, isLockedOut, creationDate,
            lastLoginDate,lastActivityDate, lastPasswordChangedDate, lastLockoutDate)
        {
            CustomerId = customerId;
        }

        public BlobMembershipUser() { }
    }
}
