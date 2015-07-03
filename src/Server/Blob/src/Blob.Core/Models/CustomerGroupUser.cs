namespace Blob.Core.Models
{
    using System;

    public class CustomerGroupUser
    {
        public Guid GroupId { get; set; }
        public Guid UserId { get; set; }
    }
}