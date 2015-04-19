//using Blob.Core.Domain;

//namespace Blob.Data.Mapping
//{
//    public class DeviceSecurityMap : BlobEntityTypeConfiguration<DeviceSecurity>
//    {
//        public DeviceSecurityMap()
//        {
//            ToTable("DeviceSecurities");

//            HasKey(x => x.DeviceId);
//            Property(x => x.DeviceId)
//                .HasColumnType("uniqueidentifier")
//                .IsRequired();
//            HasRequired(x => x.Device).WithOptional();

//            Property(x => x.Key1)
//                .HasColumnType("nvarchar").HasMaxLength(128)
//                .IsRequired();
//            Property(x => x.Key1Format)
//                .HasColumnType("int")
//                .IsRequired();
//            Property(x => x.Key1Salt)
//                .HasColumnType("nvarchar").HasMaxLength(128)
//                .IsRequired();

//            Property(x => x.Key2)
//                .HasColumnType("nvarchar").HasMaxLength(128)
//                .IsRequired();
//            Property(x => x.Key2Format)
//                .HasColumnType("int")
//                .IsRequired();
//            Property(x => x.Key2Salt)
//                .HasColumnType("nvarchar").HasMaxLength(128)
//                .IsRequired();
//            Property(x => x.IsApproved)
//                .HasColumnType("bit")
//                .IsRequired();
//            Property(x => x.IsLockedOut)
//                .HasColumnType("bit")
//                .IsRequired();
//            Property(x => x.CreateDate)
//                .HasColumnType("datetime2")
//                .IsRequired();
//            Property(x => x.LastLoginDate)
//                .HasColumnType("datetime2")
//                .IsOptional();
//            Property(x => x.LastKey1ChangedDate)
//                .HasColumnType("datetime2")
//                .IsOptional();
//            Property(x => x.LastKey2ChangedDate)
//                .HasColumnType("datetime2")
//                .IsOptional();
//            Property(x => x.FailedLoginAttemptCount)
//                .HasColumnType("int")
//                .IsOptional();
//            Property(x => x.FailedLoginAttemptWindowStart)
//                .HasColumnType("datetime2")
//                .IsOptional();
//            Property(x => x.Comment)
//                .HasColumnType("nvarchar").HasMaxLength(256)
//                .IsOptional();
//        }
//    }
//}
