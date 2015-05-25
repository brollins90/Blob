namespace Blob.Core
{
    public class BlobPermissions
    {
        public class Operations
        {
            public const string Create = "Create";
            public const string Edit = "Edit";
            public const string View = "View";
            public const string Delete = "Delete";
        }

        public class Resources
        {
            public const string Customer = "Customer";
            public const string Device = "Device";
            public const string User = "User";
            public const string StatusRecord = "StatusRecord";
            public const string PerformanceRecord = "PerformanceRecord";
        }
    }
}