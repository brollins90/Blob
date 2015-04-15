using Blob.Core.Domain;
using Blob.Data;
using log4net;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.Data.Entity;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;
using System.Web.Security;

namespace Blob.Security
{
    // http://blog.ianchivers.com/2012/03/entity-framework-custom-membership.html

    public class BlobMembershipProvider : MembershipProvider
    {
        private readonly ILog _log;
        private readonly string _dbConnectionString;
        private MachineKeySection _machineKey;

        public BlobMembershipProvider()
        {
            _log = LogManager.GetLogger("MembershipLogger");
            _dbConnectionString = ConfigurationManager.ConnectionStrings["BlobDbContext"].ConnectionString;
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            _log.Debug("Initializing BlobMembershipProvider");
            if (config == null)
                throw new ArgumentNullException("config");

            if (string.IsNullOrEmpty(name))
                name = "BlobMembershipManager";

            if (string.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Blob membership manager");
            }

            base.Initialize(name, config);

            ApplicationName = GetConfigValue(config["applicationName"], System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
            _enablePasswordReset = Convert.ToBoolean(GetConfigValue(config["enablePasswordReset"], "true"));
            _enablePasswordRetrieval = Convert.ToBoolean(GetConfigValue(config["enablePasswordRetrieval"], "true"));
            _logExceptions = Convert.ToBoolean(GetConfigValue(config["logExceptions"], "false"));
            _maxInvalidPasswordAttempts = Convert.ToInt32(GetConfigValue(config["maxInvalidPasswordAttempts"], "5"));
            _minRequiredNonAlphanumericCharacters = Convert.ToInt32(GetConfigValue(config["minRequiredNonAlphanumericCharacters"], "1"));
            _minRequiredPasswordLength = Convert.ToInt32(GetConfigValue(config["minRequiredPasswordLength"], "7"));
            _passwordAttemptWindow = Convert.ToInt32(GetConfigValue(config["passwordAttemptWindow"], "10"));
            _passwordStrengthRegularExpression = Convert.ToString(GetConfigValue(config["passwordStrengthRegularExpression"], ""));
            _requiresQuestionAndAnswer = Convert.ToBoolean(GetConfigValue(config["requiresQuestionAndAnswer"], "false"));
            _requiresUniqueEmail = Convert.ToBoolean(GetConfigValue(config["requiresUniqueEmail"], "true"));

            string tempFormat = config["passwordFormat"];
            if (string.IsNullOrEmpty(tempFormat))
            {
                tempFormat = "Hashed";
            }

            switch (tempFormat)
            {
                case "Hashed":
                    _passwordFormat = MembershipPasswordFormat.Hashed;
                    break;
                case "Encrypted":
                    _passwordFormat = MembershipPasswordFormat.Encrypted;
                    break;
                case "Clear":
                    _passwordFormat = MembershipPasswordFormat.Clear;
                    break;
                default:
                    throw new ProviderException("Password format is not supported.");
            }

            try
            {
                // Get encryption and decryption key information from the configuration.  
                Configuration cfg = WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
                _machineKey = (MachineKeySection) cfg.GetSection("system.web/machineKey");
                //_machineKey = (MachineKeySection) WebConfigurationManager.GetSection("system.web/machineKey");

                if (_machineKey.ValidationKey.Contains("AutoGenerate") && PasswordFormat != MembershipPasswordFormat.Clear)
                {
                    throw new ProviderException("Hashed or Encrypted passwords are not supported with auto-generated keys.");
                }
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Debug("Failed to load the machine key.", e);
                }
                throw; // throw this because we want it to break if this fails
            }
        }

        #region Override Properties and Fields

        private string _applicationName;
        private bool _enablePasswordReset;
        private bool _enablePasswordRetrieval;
        private bool _logExceptions;
        private int _maxInvalidPasswordAttempts;
        private int _minRequiredNonAlphanumericCharacters;
        private int _minRequiredPasswordLength;
        private string _passwordStrengthRegularExpression;
        private bool _requiresQuestionAndAnswer;
        private bool _requiresUniqueEmail;
        private int _passwordAttemptWindow;
        private MembershipPasswordFormat _passwordFormat;

        /// <summary>
        /// The name of the application using the custom membership provider.
        /// </summary>
        /// <returns>The name of the application using the custom membership provider.</returns>
        public override string ApplicationName
        {
            get { return _applicationName; }
            set { _applicationName = value; }
        }

        /// <summary>
        /// Indicates whether the membership provider is configured to allow users to reset their passwords.
        /// </summary>
        /// <returns>true if the membership provider supports password reset; otherwise, false. The default is true.</returns>
        public override bool EnablePasswordReset
        {
            get { return _enablePasswordReset; }
        }

        /// <summary>
        /// Indicates whether the membership provider is configured to allow users to retrieve their passwords.
        /// </summary>
        /// <returns>true if the membership provider is configured to support password retrieval; otherwise, false. The default is false.</returns>
        public override bool EnablePasswordRetrieval
        {
            get { return _enablePasswordRetrieval; }
        }

        /// <summary>
        /// Indicates whether to log exceptions
        /// </summary>
        /// <returns>true if the membership provider is configured to log exceptions; otherwise, false. The default is false.</returns>
        public bool LogExceptions
        {
            get { return _logExceptions; }
        }

        /// <summary>
        /// Gets the number of invalid password or password-answer attempts allowed before the membership user is locked out.
        /// </summary>
        /// <returns>The number of invalid password or password-answer attempts allowed before the membership user is locked out.</returns>
        public override int MaxInvalidPasswordAttempts
        {
            get { return _maxInvalidPasswordAttempts; }
        }

        /// <summary>
        /// Gets the minimum number of special characters that must be present in a valid password.
        /// </summary>
        /// <returns>The minimum number of special characters that must be present in a valid password.</returns>
        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return _minRequiredNonAlphanumericCharacters; }
        }

        /// <summary>
        /// Gets the minimum length required for a password.
        /// </summary>
        /// <returns>The minimum length required for a password.</returns>
        public override int MinRequiredPasswordLength
        {
            get { return _minRequiredPasswordLength; }
        }

        /// <summary>
        /// Gets the number of minutes in which a maximum number of invalid password
        //     or password-answer attempts are allowed before the membership user is locked
        //     out.
        /// </summary>
        /// <returns>The number of minutes in which a maximum number of invalid password or password-answer attempts are allowed before the membership user is locked out.</returns>
        public override int PasswordAttemptWindow
        {
            get { return _passwordAttemptWindow; }
        }

        /// <summary>
        /// Gets a value indicating the format for storing passwords in the membership data store.
        /// </summary>
        /// <returns>One of the System.Web.Security.MembershipPasswordFormat values indicating the format for storing passwords in the data store.</returns>
        public override MembershipPasswordFormat PasswordFormat
        {
            get { return _passwordFormat; }
        }

        /// <summary>
        /// Gets the regular expression used to evaluate a password.
        /// </summary>
        /// <returns>A regular expression used to evaluate a password.</returns>
        public override string PasswordStrengthRegularExpression
        {
            get { return _passwordStrengthRegularExpression; }
        }

        /// <summary>
        /// Gets a value indicating whether the membership provider is configured to require the user to answer a password question for password reset and retrieval.
        /// </summary>
        /// <returns>true if a password answer is required for password reset and retrieval; otherwise, false. The default is true.</returns>
        public override bool RequiresQuestionAndAnswer
        {
            get { return _requiresQuestionAndAnswer; }
        }

        /// <summary>
        /// Gets a value indicating whether the membership provider is configured to require a unique e-mail address for each user name.
        /// </summary>
        /// <returns>true if the membership provider requires a unique e-mail address; otherwise, false. The default is true.</returns>
        public override bool RequiresUniqueEmail
        {
            get { return _requiresUniqueEmail; }
        }

        #endregion

        /// <summary>
        /// Processes a request to update the password for a membership user.
        /// </summary>
        /// <param name="username">The user to update the password for.</param>
        /// <param name="oldPassword">The current password for the specified user.</param>
        /// <param name="newPassword">The new password for the specified user.</param>
        /// <returns>true if the password was updated successfully; otherwise, false.</returns>
        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Processes a request to update the password question and answer for a membership user.
        /// </summary>
        /// <param name="username">The user to change the password question and answer for.</param>
        /// <param name="password">The password for the specified user.</param>
        /// <param name="newPasswordQuestion">The new password question for the specified user.</param>
        /// <param name="newPasswordAnswer">The new password answer for the specified user.</param>
        /// <returns>true if the password question and answer are updated successfully; otherwise, false.</returns>
        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds a new membership user to the data source.
        /// </summary>
        /// <param name="username">The user name for the new user.</param>
        /// <param name="password">The password for the new user.</param>
        /// <param name="email">The e-mail address for the new user.</param>
        /// <param name="passwordQuestion">The password question for the new user.</param>
        /// <param name="passwordAnswer">The password answer for the new user</param>
        /// <param name="isApproved">Whether or not the new user is approved to be validated.</param>
        /// <param name="providerUserKey">The unique identifier from the membership data source for the user.</param>
        /// <param name="status">A System.Web.Security.MembershipCreateStatus enumeration value indicating whether the user was created successfully.</param>
        /// <returns>A System.Web.Security.MembershipUser object populated with the information for the newly created user.</returns>
        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            return CreateUser(username, password, email, passwordQuestion, passwordAnswer, isApproved, providerUserKey, out status, null);
        }

        /// <summary>
        /// Adds a new membership user to the data source. Specifies the customer's id in the data source.
        /// </summary>
        /// <param name="username">The user name for the new user.</param>
        /// <param name="password">The password for the new user.</param>
        /// <param name="email">The e-mail address for the new user.</param>
        /// <param name="passwordQuestion">The password question for the new user.</param>
        /// <param name="passwordAnswer">The password answer for the new user</param>
        /// <param name="isApproved">Whether or not the new user is approved to be validated.</param>
        /// <param name="providerUserKey">The unique identifier from the membership data source for the user.</param>
        /// <param name="status">A System.Web.Security.MembershipCreateStatus enumeration value indicating whether the user was created successfully.</param>
        /// <param name="customerId">The id from the membership data source for the customer to which the user belongs.</param>
        /// <returns>A System.Web.Security.MembershipUser object populated with the information for the newly created user.</returns>
        public MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status, long? customerId)
        {
            ValidatePasswordEventArgs args = new ValidatePasswordEventArgs(username, password, true);
            OnValidatingPassword(args);

            if (args.Cancel)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }

            if (RequiresUniqueEmail && GetUserNameByEmail(email) != null)
            {
                status = MembershipCreateStatus.DuplicateEmail;
                return null;
            }

            BlobMembershipUser u = GetUser(username, false) as BlobMembershipUser;

            if (u == null)
            {
                DateTime createDate = DateTime.Now;

                if (providerUserKey == null)
                {
                    providerUserKey = Guid.NewGuid();
                }
                else
                {
                    if (!(providerUserKey is Guid))
                    {
                        status = MembershipCreateStatus.InvalidProviderUserKey;
                        return null;
                    }
                }

                User user = new User
                            {
                                UserId = (Guid)providerUserKey,
                                Username = username,
                                LastActivityDate = createDate
                            };
                UserSecurity userSecurity = new UserSecurity
                                  {
                                      Password = EncodePassword(password),
                                      Email = email,
                                      PasswordQuestion = passwordQuestion,
                                      PasswordAnswer = EncodePassword(passwordAnswer),
                                      PasswordSalt = CreateSalt(),
                                      IsApproved = isApproved,
                                      Comment = string.Empty,
                                      CreateDate = createDate,
                                      LastPasswordChangedDate = createDate,
                                      IsLockedOut = false,
                                      FailedPasswordAttemptCount = 0,
                                      FailedPasswordAnswerAttemptCount = 0,
                                      LastLockoutDate = SqlDateTime.MinValue.Value,
                                      LastLoginDate = SqlDateTime.MinValue.Value,
                                      User = user
                                  };

                try
                {
                    using (BlobDbContext context = new BlobDbContext(_dbConnectionString))
                    {
                        if (customerId != null)
                        {
                            Customer cust = context.Set<Customer>().FirstOrDefault(c => c.Id.Equals(customerId));
                            user.Customer = cust;
                        }

                        context.Entry(user).State = EntityState.Added;
                        context.Entry(userSecurity).State = EntityState.Added;
                        context.SaveChanges();
                        status = MembershipCreateStatus.Success;
                    }
                }
                catch (Exception e)
                {
                    if (LogExceptions)
                    {
                        _log.Warn("Failed to create user.", e);
                    }
                    status = MembershipCreateStatus.ProviderError;
                }
                return GetUser(username, false);
            }
            else
            {
                status = MembershipCreateStatus.DuplicateUserName;
            }
            return null;
        }

        /// <summary>
        /// Removes a user from the membership data source.
        /// </summary>
        /// <param name="username">The name of the user to delete.</param>
        /// <param name="deleteAllRelatedData">true to delete data related to the user from the database; false to leave data related to the user in the database.</param>
        /// <returns>true if the user was successfully deleted; otherwise, false.</returns>
        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a collection of membership users where the e-mail address contains the specified e-mail address to match.
        /// </summary>
        /// <param name="emailToMatch">The e-mail address to search for.</param>
        /// <param name="pageIndex">The index of the page of results to return. pageIndex is zero-based.</param>
        /// <param name="pageSize">The size of the page of results to return.</param>
        /// <param name="totalRecords">The total number of matched users.</param>
        /// <returns>A System.Web.Security.MembershipUserCollection collection that contains a page of pageSizeSystem.Web.Security.MembershipUser objects beginning at the page specified by pageIndex.</returns>
        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a collection of membership users where the user name contains the specified user name to match.
        /// </summary>
        /// <param name="usernameToMatch">The user name to search for.</param>
        /// <param name="pageIndex">The index of the page of results to return. pageIndex is zero-based.</param>
        /// <param name="pageSize">The size of the page of results to return.</param>
        /// <param name="totalRecords">The total number of matched users.</param>
        /// <returns>A System.Web.Security.MembershipUserCollection collection that contains a page of pageSizeSystem.Web.Security.MembershipUser objects beginning at the page specified by pageIndex.</returns>
        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a collection of all the users in the data source in pages of data.
        /// </summary>
        /// <param name="pageIndex">The index of the page of results to return. pageIndex is zero-based.</param>
        /// <param name="pageSize">The size of the page of results to return.</param>
        /// <param name="totalRecords">The total number of matched users.</param>
        /// <returns>A System.Web.Security.MembershipUserCollection collection that contains a page of pageSizeSystem.Web.Security.MembershipUser objects beginning at the page specified by pageIndex.</returns>
        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the number of users currently accessing the application.
        /// </summary>
        /// <returns>The number of users currently accessing the application.</returns>
        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the password for the specified user name from the data source.
        /// </summary>
        /// <param name="username">The user to retrieve the password for.</param>
        /// <param name="answer">The password answer for the user.</param>
        /// <returns>The password for the specified user name.</returns>
        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets user information from the data source based on the unique identifier for the membership user. Provides an option to update the last-activity date/time stamp for the user.
        /// </summary>
        /// <param name="providerUserKey">The unique identifier for the membership user to get information for.</param>
        /// <param name="userIsOnline">true to update the last-activity date/time stamp for the user; false to return user information without updating the last-activity date/time stamp for the user.</param>
        /// <returns>A System.Web.Security.MembershipUser object populated with the specified user's information from the data source.</returns>
        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            Expression<Func<UserSecurity, bool>> predicate = (us => us.User.UserId.Equals((Guid)providerUserKey));
            return GetUser(predicate, userIsOnline);
        }

        /// <summary>
        /// Gets information from the data source for a user. Provides an option to update the last-activity date/time stamp for the user.
        /// </summary>
        /// <param name="username">The name of the user to get information for.</param>
        /// <param name="userIsOnline">true to update the last-activity date/time stamp for the user; false to return user information without updating the last-activity date/time stamp for the user.</param>
        /// <returns>A System.Web.Security.MembershipUser object populated with the specified user's information from the data source.</returns>
        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            Expression<Func<UserSecurity, bool>> predicate = (us => us.User.Username.Equals(username));
            return GetUser(predicate, userIsOnline);
        }

        /// <summary>
        /// Gets information from the data source for a user. Provides an option to update the last-activity date/time stamp for the user.
        /// </summary>
        /// <param name="predicate">An expression used to match the user to return.</param>
        /// <param name="userIsOnline">true to update the last-activity date/time stamp for the user; false to return user information without updating the last-activity date/time stamp for the user.</param>
        /// <returns>A System.Web.Security.MembershipUser object populated with the specified user's information from the data source.</returns>
        public MembershipUser GetUser(Expression<Func<UserSecurity, bool>> predicate, bool userIsOnline)
        {
            BlobMembershipUser user = null;

            try
            {
                using (BlobDbContext context = new BlobDbContext(_dbConnectionString))
                {
                    UserSecurity userSecurity = context.Set<UserSecurity>()
                        .FirstOrDefault(predicate);

                    if (userSecurity != null)
                    {
                        user = new BlobMembershipUser(
                            Name,
                            userSecurity.User.Username,
                            userSecurity.User.UserId,
                            userSecurity.Email,
                            userSecurity.PasswordQuestion,
                            userSecurity.Comment,
                            userSecurity.IsApproved,
                            userSecurity.IsLockedOut,
                            userSecurity.CreateDate,
                            userSecurity.LastLoginDate,
                            userSecurity.User.LastActivityDate,
                            userSecurity.LastPasswordChangedDate,
                            userSecurity.LastLockoutDate,
                            userSecurity.User.CustomerId)
                               {
                                   Id = userSecurity.User.Id
                               };

                        if (userIsOnline)
                        {
                            userSecurity.User.LastActivityDate = DateTime.Now;
                            context.Entry(userSecurity).State = EntityState.Modified;
                            context.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Warn("Failed to get user.", e);
                    throw new ProviderException("Failed to get user.");
                }

                throw;
            }

            return user;
        }

        /// <summary>
        /// Gets the user name associated with the specified e-mail address.
        /// </summary>
        /// <param name="email">The e-mail address to search for.</param>
        /// <returns>The user name associated with the specified e-mail address. If no match is found, return null.</returns>
        public override string GetUserNameByEmail(string email)
        {
            string username = string.Empty;

            try
            {
                using (BlobDbContext context = new BlobDbContext(_dbConnectionString))
                {
                    UserSecurity userSecurity = context.Set<UserSecurity>()
                        .FirstOrDefault(us => us.Email.Equals(email));

                    if (userSecurity != null)
                    {
                        username = userSecurity.User.Username;
                    }
                }
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Warn("Failed to get username.", e);
                    throw new ProviderException("Failed to get username.");
                }

                throw;
            }

            return username;
        }

        /// <summary>
        /// Resets a user's password to a new, automatically generated password.
        /// </summary>
        /// <param name="username">The user to reset the password for.</param>
        /// <param name="answer">The password answer for the specified user.</param>
        /// <returns>The new password for the specified user.</returns>
        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Clears a lock so that the membership user can be validated.
        /// </summary>
        /// <param name="userName">The membership user whose lock status you want to clear.</param>
        /// <returns>true if the membership user was successfully unlocked; otherwise, false.</returns>
        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates information about a user in the data source.
        /// </summary>
        /// <param name="user">A System.Web.Security.MembershipUser object that represents the user to update and the updated information for the user.</param>
        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Verifies that the specified user name and password exist in the data source.
        /// </summary>
        /// <param name="username">The name of the user to validate.</param>
        /// <param name="password">The password for the specified user.</param>
        /// <returns>true if the specified username and password are valid; otherwise, false.</returns>
        public override bool ValidateUser(string username, string password)
        {
            bool isValid = false;

            try
            {
                using (BlobDbContext context = new BlobDbContext(_dbConnectionString))
                {
                    UserSecurity userSecurity = context.Set<UserSecurity>()
                        .FirstOrDefault(us => us.User.Username.Equals(username)
                            && us.IsLockedOut == false);

                    if (userSecurity != null)
                    {
                        userSecurity.LastLoginDate = DateTime.Now;
                        if (userSecurity.IsApproved)
                        {
                            if (CheckPassword(password, userSecurity.Password))
                            {
                                isValid = true;
                                context.Entry(userSecurity).State = EntityState.Modified;
                                context.SaveChanges();
                            }
                            else
                            {
                                UpdateFailureCount(username, "password");
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Warn("Error validating username and password.", e);
                    throw new ProviderException("Error validating username and password.");
                }

                throw;
            }

            return isValid;
        }

        /// <summary>
        /// A helper method that performs the checks and updates associated with password failure tracking.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="failureType"></param>
        private void UpdateFailureCount(string username, string failureType)
        {
            DateTime windowStart = new DateTime();
            int failureCount = 0;

            try
            {
                using (BlobDbContext context = new BlobDbContext(_dbConnectionString))
                {
                    UserSecurity userSecurity = context.Set<UserSecurity>()
                        .FirstOrDefault(us => us.User.Username.Equals(username));

                    if (userSecurity != null)
                    {
                        if (failureType.Equals("password"))
                        {
                            failureCount = userSecurity.FailedPasswordAttemptCount;
                            windowStart = userSecurity.FailedPasswordAttemptWindowStart;
                        }

                        if (failureType == "passwordAnswer")
                        {
                            failureCount = userSecurity.FailedPasswordAnswerAttemptCount;
                            windowStart = userSecurity.FailedPasswordAttemptWindowStart;
                        }

                        DateTime windowEnd = windowStart.AddMinutes(PasswordAttemptWindow);

                        if (failureCount == 0 || DateTime.Now > windowEnd)
                        {
                            // First password failure or outside of PasswordAttemptWindow.   
                            // Start a new password failure count from 1 and a new window starting now.
                            if (failureType == "password")
                            {
                                userSecurity.FailedPasswordAttemptCount = 1;
                                userSecurity.FailedPasswordAttemptWindowStart = DateTime.Now;
                            }

                            if (failureType == "passwordAnswer")
                            {
                                userSecurity.FailedPasswordAnswerAttemptCount = 1;
                                userSecurity.FailedPasswordAnswerAttemptWindowStart = DateTime.Now;
                            }
                        }
                        else
                        {
                            if (failureCount++ >= MaxInvalidPasswordAttempts)
                            {
                                // Password attempts have exceeded the failure threshold. Lock out the user.
                                userSecurity.IsLockedOut = true;
                                userSecurity.LastLockoutDate = DateTime.Now;
                            }
                            else
                            {
                                if (failureType == "password")
                                    userSecurity.FailedPasswordAttemptCount = failureCount;

                                if (failureType == "passwordAnswer")
                                    userSecurity.FailedPasswordAnswerAttemptCount = failureCount;
                            }
                        }
                        context.Entry(userSecurity).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Warn("Error updating failure count.", e);
                    throw new ProviderException("Error updating failure count.");
                }

                throw;
            }
        }

        /// <summary>
        /// Reads a config value from the config file.
        /// </summary>
        /// <param name="configValue">Value name in the config file.</param>
        /// <param name="defaultValue">Default value to return if the specified value does not exist.</param>
        /// <returns>the value of the config element or the default specified if the element is null.</returns>
        private static string GetConfigValue(string configValue, string defaultValue)
        {
            return (string.IsNullOrEmpty(configValue)) ? defaultValue : configValue;
        }

        /// <summary>
        /// Compares password values based on the MembershipPasswordFormat.
        /// </summary>
        /// <param name="password">Password to check against the database.</param>
        /// <param name="dbpassword">The password stored in the dataabase.</param>
        /// <returns>true if the passwords are equal, otherwise false.</returns>
        private bool CheckPassword(string password, string dbpassword)
        {
            string pass1 = password;
            string pass2 = dbpassword;

            switch (PasswordFormat)
            {
                case MembershipPasswordFormat.Encrypted:
                    pass2 = UnEncodePassword(dbpassword);
                    break;
                case MembershipPasswordFormat.Hashed:
                    pass1 = EncodePassword(password);
                    break;
            }

            return pass1.Equals(pass2);
        }

        /// <summary>
        /// Encrypts, Hashes, or leaves the password clear based on the PasswordFormat.  
        /// </summary>
        /// <param name="password">The password to encode.</param>
        /// <returns>the encoded password.</returns>
        private string EncodePassword(string password)
        {
            string encodedPassword = password;

            if (!string.IsNullOrEmpty(password))
            {
                switch (PasswordFormat)
                {
                    case MembershipPasswordFormat.Clear:
                        break;
                    case MembershipPasswordFormat.Encrypted:
                        encodedPassword = Convert.ToBase64String(EncryptPassword(Encoding.Unicode.GetBytes(password)));
                        break;
                    case MembershipPasswordFormat.Hashed:
                        HMACSHA1 hash = new HMACSHA1 { Key = HexToByte(_machineKey.ValidationKey) };
                        encodedPassword = Convert.ToBase64String(hash.ComputeHash(Encoding.Unicode.GetBytes(password)));
                        break;
                    default:
                        throw new ProviderException("Unsupported password format.");
                }
            }

            return encodedPassword;
        }

        /// <summary>
        /// Decrypts or leaves the password clear based on the PasswordFormat.
        /// </summary>
        /// <param name="encodedPassword">The encoded password to unencode.</param>
        /// <returns>the clear text password.</returns>
        private string UnEncodePassword(string encodedPassword)
        {
            string password = encodedPassword;

            switch (PasswordFormat)
            {
                case MembershipPasswordFormat.Clear:
                    break;
                case MembershipPasswordFormat.Encrypted:
                    password = Encoding.Unicode.GetString(DecryptPassword(Convert.FromBase64String(password)));
                    break;
                case MembershipPasswordFormat.Hashed:
                    throw new ProviderException("Cannot unencode a hashed password.");
                default:
                    throw new ProviderException("Unsupported password format.");
            }

            return password;
        }

        /// <summary>
        /// Converts a hexadecimal string to a byte array. Used to convert encryption key values from the configuration.  
        /// </summary>
        /// <param name="hexString">String to convert to bytes.  Ex: (machineKey)</param>
        /// <returns>a byte array of the hex string.</returns>
        private static byte[] HexToByte(string hexString)
        {
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        /// <summary>  
        /// Creates a random 128 password salt  
        /// </summary>  
        /// <returns></returns>
        private static string CreateSalt()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[32];
            rng.GetBytes(buff);

            return Convert.ToBase64String(buff);
        }
    }
}
