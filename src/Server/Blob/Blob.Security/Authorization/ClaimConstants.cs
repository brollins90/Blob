
namespace Blob.Security.Authorization
{
    public static class ClaimConstants
    {
        public const string SchemaNS = "http://schemas.rritc.com/blobservice/2015/01";
        
        public const string IdentityProvider = "Blob";
        public const string CustomerId = "customerId";

        public const string AllInOne = SchemaNS + "/claims/allinone";

        public const string Operation = SchemaNS + "/claims/operation";
        public const string Resource = SchemaNS + "/claims/resource";

        public const string OperationAdd = Operation + "/add";
        public const string OperationDelete = Operation + "/delete";
        public const string OperationUpdate = Operation + "/update";
        public const string OperationView = Operation + "/view";

        public const string ResourceCustomer = Resource + "/customer";
        public const string ResourceDevice = Resource + "/device";
        public const string ResourceUser = Resource + "/user";
    }
}
