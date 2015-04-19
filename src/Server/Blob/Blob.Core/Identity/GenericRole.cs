using Microsoft.AspNet.Identity;
using System.Collections.Generic;

namespace Blob.Core.Identity
{
    public class GenericRole : GenericRole<string, GenericUserRole> { }

    public class GenericRole<TKey, TUserRole> : IRole<TKey>
        where TUserRole : GenericUserRole<TKey>
    {
        public virtual TKey Id { get; set; }
        public virtual string Name { get; set; }

        public virtual ICollection<TUserRole> Users { get; set; }
    }
}
