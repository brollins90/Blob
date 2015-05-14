﻿using System.Data.Entity.ModelConfiguration;
// ReSharper disable DoNotCallOverridableMethodsInConstructor

namespace Blob.Data.Mapping
{
    public abstract class BlobEntityTypeConfiguration<T> : EntityTypeConfiguration<T> where T : class
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        protected BlobEntityTypeConfiguration()
        {
            PostInitialize();
        }

        protected virtual void PostInitialize()
        {

        }
    }
}
