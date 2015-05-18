﻿using System;
using System.Collections.Generic;
using Blob.Core.Identity;

namespace Blob.Core.Domain
{
    public class BlobGroupRole : GenericGroupRole<Guid> { }

    public class BlobUserGroup : GenericUserGroup<Guid> { }

    public class BlobGroup : GenericGroup<Guid, BlobUserGroup, BlobGroupRole>
    {
        public string Description { get; set; }
    }
}
