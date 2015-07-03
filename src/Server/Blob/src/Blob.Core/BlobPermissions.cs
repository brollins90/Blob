namespace Blob.Core
{
    public class BlobPermissions
    {
        public class Operations
        {
            public const string CREATE = "Create";
            public const string EDIT = "Edit";
            public const string VIEW = "View";
            public const string DELETE = "Delete";
        }

        public class Resources
        {
            public const string CUSTOMER = "Customer";
            public const string DEVICE = "Device";
            public const string USER = "User";
            public const string STATUS_RECORD = "StatusRecord";
            public const string PERFORMANCE_RECORD = "PerformanceRecord";
        }
    }
}