//using System.Collections.Generic;
//using System.ServiceModel;
//using System.Web.Security;

//namespace Blob.Contracts.Security
//{
//    [ServiceContract]
//    public interface IMembershipService
//    {
//        [OperationContract]
//        void SetProvider(string providerName);

//        string ApplicationName { [OperationContract] get; [OperationContract] set; }

//        [OperationContract]
//        bool ChangePassword(string username, string oldPassword, string newPassword);

//        [OperationContract]
//        bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer);

//        [OperationContract]
//        MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status);

//        [OperationContract]
//        bool DeleteUser(string username, bool deleteAllRelatedData);

//        bool EnablePasswordReset { [OperationContract] get; }

//        bool EnablePasswordRetrieval { [OperationContract] get; }

//        [OperationContract]
//        List<MembershipUser> ListUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords);

//        [OperationContract]
//        List<MembershipUser> ListUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords);

//        [OperationContract]
//        List<MembershipUser> ListAllUsers(int pageIndex, int pageSize, out int totalRecords);

//        [OperationContract]
//        int GetNumberOfUsersOnline();

//        [OperationContract]
//        string GetPassword(string username, string answer);

//        [OperationContract(Name = "GetUserByUserName")]
//        MembershipUser GetUser(string username, bool userIsOnline);

//        [OperationContract(Name = "GetUserByUserKey")]
//        MembershipUser GetUser(object providerUserKey, bool userIsOnline);

//        [OperationContract]
//        string GetUserNameByEmail(string email);

//        int MaxInvalidPasswordAttempts { [OperationContract] get; }

//        int MinRequiredNonAlphanumericCharacters { [OperationContract] get; }

//        int MinRequiredPasswordLength { [OperationContract] get; }

//        int PasswordAttemptWindow { [OperationContract] get; }

//        MembershipPasswordFormat PasswordFormat { [OperationContract] get; }

//        string PasswordStrengthRegularExpression { [OperationContract] get; }

//        bool RequiresQuestionAndAnswer { [OperationContract] get; }


//        bool RequiresUniqueEmail { [OperationContract] get; }

//        [OperationContract]
//        string ResetPassword(string username, string answer);

//        [OperationContract]
//        bool UnlockUser(string userName);

//        [OperationContract]
//        void UpdateUser(MembershipUser user);

//        [OperationContract]
//        bool ValidateUser(string username, string password);
//    }
//}
