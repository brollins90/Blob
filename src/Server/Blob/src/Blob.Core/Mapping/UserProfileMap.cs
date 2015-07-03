namespace Blob.Core.Mapping
{
    using Data.Mapping;
    using Models;

    public class UserProfileMap : BlobEntityTypeConfiguration<UserProfile>
    {
        public UserProfileMap()
        {
            ToTable("UsersProfiles");

            HasKey(x => x.UserId);

            Property(x => x.UserId).HasColumnType("uniqueidentifier").IsRequired();
            HasRequired(x => x.User).WithOptional();

            Property(x => x.SendEmailNotifications).HasColumnType("bit").IsRequired();
            Property(x => x.EmailNotificationScheduleId).HasColumnType("uniqueidentifier").IsOptional();
            HasOptional(x => x.EmailNotificationSchedule);
        }
    }
}