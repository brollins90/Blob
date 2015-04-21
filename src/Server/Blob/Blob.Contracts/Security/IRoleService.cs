//using System.ServiceModel;

//namespace Blob.Contracts.Security
//{
//    [ServiceContract]
//    public interface IRoleService
//    {
//        string ApplicationName { [OperationContract] get; [OperationContract] set; }

//        [OperationContract]
//        void SetProvider(string providerName);

//        [OperationContract]
//        void AddUsersToRoles(string[] usernames, string[] roleNames);

//        [OperationContract]
//        void CreateRole(string roleName);

//        [OperationContract]
//        bool DeleteRole(string roleName, bool throwOnPopulatedRole);

//        [OperationContract]
//        string[] FindUsersInRole(string roleName, string usernameToMatch);

//        [OperationContract]
//        string[] GetAllRoles();

//        [OperationContract]
//        string[] GetRolesForUser(string username);

//        [OperationContract]
//        string[] GetUsersInRole(string roleName);

//        [OperationContract]
//        bool IsUserInRole(string username, string roleName);

//        [OperationContract]
//        void RemoveUsersFromRoles(string[] usernames, string[] roleNames);

//        [OperationContract]
//        bool RoleExists(string roleName);
//    }
//}
