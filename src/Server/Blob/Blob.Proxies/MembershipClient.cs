using Blob.Contracts.Security;
using log4net;
using System;
using System.Collections.Specialized;
using System.ServiceModel;
using System.Web.Security;

namespace Blob.Proxies
{
    class MembershipClient : MembershipProvider
    {
        private readonly ILog _log;
        private static ChannelFactory<IMembershipService> _channelFactory;

        public MembershipClient()
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            try
            {
                _log.Debug("Initializing membership proxy");
                if (config == null)
                    throw new ArgumentNullException("config");

                Config = config;

                if (string.IsNullOrEmpty(name))
                    name = ApplicationName;

                if (string.IsNullOrEmpty(Description))
                {
                    config.Remove("Description");
                    config.Add("Description", Description);
                }

                base.Initialize(name, config);

                _log.Debug(string.Format("Remote provider type is {0}", ProxyProviderName));
            }
            catch (Exception e)
            {
                _log.Error("Failed to load membership provider client.", e);
                throw;
            }
        }

        /// <summary>
        /// After the caller has completed its work with the object,
        /// it should then pass the object to DisposeProvider to close
        /// the connection.
        /// </summary>
        private IMembershipService RemoteProvider()
        {
            if (_channelFactory == null)
            {
                _channelFactory = new ChannelFactory<IMembershipService>("MembershipService");
            }

            IMembershipService provider = _channelFactory.CreateChannel();
            provider.SetProvider(ProxyProviderName);

            return provider;
        }

        /// <summary>
        /// This method should be called to handle closing the 
        /// connected proxy object.
        /// </summary>
        private void DisposeRemoteProvider(IMembershipService remoteProvider)
        {
            _log.Debug("Disposing of remote provider.");

            // TODO: Add error checking for state of object.
            ((IClientChannel)remoteProvider).Dispose();
        }
        

        private NameValueCollection Config { get; set; }

        public override string ApplicationName
        {
            get
            {
                if (string.IsNullOrEmpty(_applicationName))
                {
                    _applicationName = Config["ApplicationName"];
                    if (string.IsNullOrEmpty(_applicationName))
                        _applicationName = "";
                }
                return _applicationName;
            }
            set { _applicationName = value; }
        }
        private string _applicationName;


        /// <summary>
        /// Indicates whether to log exceptions
        /// </summary>
        /// <returns>true if the membership provider is configured to log exceptions; otherwise, false. The default is false.</returns>
        public bool LogExceptions
        {
            get
            {
                if (_logExceptions.HasValue == false)
                {
                    bool bv;
                    string strv = Config["LogExceptions"];
                    if (!string.IsNullOrEmpty(strv) && bool.TryParse(strv, out bv))
                        _logExceptions = bv;
                    else
                        _logExceptions = true;
                }
                return _logExceptions.Value;
            }
        }
        private bool? _logExceptions;


        public string ProxyProviderName
        {
            get
            {
                if (string.IsNullOrEmpty(_proxyProviderName))
                {
                    _proxyProviderName = Config["ProxyProviderName"];
                    if (string.IsNullOrEmpty(_proxyProviderName))
                        _proxyProviderName = "";
                }
                return _proxyProviderName;
            }
            set { _proxyProviderName = value; }
        }
        private string _proxyProviderName;

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            bool output;

            try
            {
                IMembershipService remoteProvider = RemoteProvider();
                output = remoteProvider.ChangePassword(username, oldPassword, newPassword);
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to change password.", e);
                }
                throw;
            }
            return output;
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            bool output;

            try
            {
                IMembershipService remoteProvider = RemoteProvider();
                output = remoteProvider.ChangePasswordQuestionAndAnswer(username, password, newPasswordQuestion, newPasswordAnswer);
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to change question and answer.", e);
                }
                throw;
            }
            return output;
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            MembershipUser output;

            try
            {
                IMembershipService remoteProvider = RemoteProvider();
                output = remoteProvider.CreateUser(username, password, email, passwordQuestion, passwordAnswer, isApproved, providerUserKey, out status);
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to create user.", e);
                }
                throw;
            }
            return output;
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            bool output;

            try
            {
                IMembershipService remoteProvider = RemoteProvider();
                output = remoteProvider.DeleteUser(username, deleteAllRelatedData);
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to delete user.", e);
                }
                throw;
            }
            return output;
        }

        public override bool EnablePasswordReset
        {
            get
            {
                bool output;

                try
                {
                    IMembershipService remoteProvider = RemoteProvider();
                    output = remoteProvider.EnablePasswordReset;
                    DisposeRemoteProvider(remoteProvider);
                }
                catch (Exception e)
                {
                    if (LogExceptions)
                    {
                        _log.Error("Failed to get property.", e);
                    }
                    throw;
                }
                return output;
            }
        }

        public override bool EnablePasswordRetrieval
        {
            get
            {
                bool output;

                try
                {
                    IMembershipService remoteProvider = RemoteProvider();
                    output = remoteProvider.EnablePasswordRetrieval;
                    DisposeRemoteProvider(remoteProvider);
                }
                catch (Exception e)
                {
                    if (LogExceptions)
                    {
                        _log.Error("Failed to get property.", e);
                    }
                    throw;
                }
                return output;
            }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            MembershipUserCollection output = new MembershipUserCollection();

            try
            {
                IMembershipService remoteProvider = RemoteProvider();

                // We can not serialize a MembershipUserCollection so the proxy
                // interface provides a list of serialized MembershipUser objects
                // which we rebuild a MembershipUserCollection object with.
                foreach (MembershipUser user in remoteProvider.ListUsersByEmail(emailToMatch, pageIndex, pageSize, out totalRecords))
                {
                    output.Add(user);
                }

                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to get users.", e);
                }
                throw;
            }
            return output;
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            MembershipUserCollection output = new MembershipUserCollection();

            try
            {
                IMembershipService remoteProvider = RemoteProvider();

                // We can not serialize a MembershipUserCollection so the proxy
                // interface provides a list of serialized MembershipUser objects
                // which we rebuild a MembershipUserCollection object with.
                foreach (MembershipUser user in remoteProvider.ListUsersByName(usernameToMatch, pageIndex, pageSize, out totalRecords))
                {
                    output.Add(user);
                }

                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to get users.", e);
                }
                throw;
            }
            return output;
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            MembershipUserCollection output = new MembershipUserCollection();

            try
            {
                IMembershipService remoteProvider = RemoteProvider();

                // We can not serialize a MembershipUserCollection so the proxy
                // interface provides a list of serialized MembershipUser objects
                // which we rebuild a MembershipUserCollection object with.
                foreach (MembershipUser user in remoteProvider.ListAllUsers(pageIndex, pageSize, out totalRecords))
                {
                    output.Add(user);
                }

                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to get users.", e);
                }
                throw;
            }
            return output;
        }

        public override int GetNumberOfUsersOnline()
        {
            int output;

            try
            {
                IMembershipService remoteProvider = RemoteProvider();
                output = remoteProvider.GetNumberOfUsersOnline();
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to get property.", e);
                }
                throw;
            }
            return output;
        }

        public override string GetPassword(string username, string answer)
        {
            string output;

            try
            {
                IMembershipService remoteProvider = RemoteProvider();
                output = remoteProvider.GetPassword(username, answer);
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to get password.", e);
                }
                throw;
            }
            return output;
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            MembershipUser output;

            try
            {
                IMembershipService remoteProvider = RemoteProvider();
                output = remoteProvider.GetUser(username, userIsOnline);
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to get user.", e);
                }
                throw;
            }
            return output;
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            MembershipUser output;

            try
            {
                IMembershipService remoteProvider = RemoteProvider();
                output = remoteProvider.GetUser(providerUserKey, userIsOnline);
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to get user.", e);
                }
                throw;
            }
            return output;
        }

        public override string GetUserNameByEmail(string email)
        {
            string output;

            try
            {
                IMembershipService remoteProvider = RemoteProvider();
                output = remoteProvider.GetUserNameByEmail(email);
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to get username.", e);
                }
                throw;
            }
            return output;
        }

        public override int MaxInvalidPasswordAttempts
        {
            get
            {
                int output;

                try
                {
                    IMembershipService remoteProvider = RemoteProvider();
                    output = remoteProvider.MaxInvalidPasswordAttempts;
                    DisposeRemoteProvider(remoteProvider);
                }
                catch (Exception e)
                {
                    if (LogExceptions)
                    {
                        _log.Error("Failed to get property.", e);
                    }
                    throw;
                }
                return output;
            }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get
            {
                int output;

                try
                {
                    IMembershipService remoteProvider = RemoteProvider();
                    output = remoteProvider.MinRequiredNonAlphanumericCharacters;
                    DisposeRemoteProvider(remoteProvider);
                }
                catch (Exception e)
                {
                    if (LogExceptions)
                    {
                        _log.Error("Failed to get property.", e);
                    }
                    throw;
                }
                return output;
            }
        }

        public override int MinRequiredPasswordLength
        {
            get
            {
                int output;

                try
                {
                    IMembershipService remoteProvider = RemoteProvider();
                    output = remoteProvider.MinRequiredPasswordLength;
                    DisposeRemoteProvider(remoteProvider);
                }
                catch (Exception e)
                {
                    if (LogExceptions)
                    {
                        _log.Error("Failed to get property.", e);
                    }
                    throw;
                }
                return output;
            }
        }

        public override int PasswordAttemptWindow
        {
            get
            {
                int output;

                try
                {
                    IMembershipService remoteProvider = RemoteProvider();
                    output = remoteProvider.PasswordAttemptWindow;
                    DisposeRemoteProvider(remoteProvider);
                }
                catch (Exception e)
                {
                    if (LogExceptions)
                    {
                        _log.Error("Failed to get property.", e);
                    }
                    throw;
                }
                return output;
            }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get
            {
                MembershipPasswordFormat output;

                try
                {
                    IMembershipService remoteProvider = RemoteProvider();
                    output = remoteProvider.PasswordFormat;
                    DisposeRemoteProvider(remoteProvider);
                }
                catch (Exception e)
                {
                    if (LogExceptions)
                    {
                        _log.Error("Failed to get property.", e);
                    }
                    throw;
                }
                return output;
            }
        }

        public override string PasswordStrengthRegularExpression
        {
            get
            {
                string output;

                try
                {
                    IMembershipService remoteProvider = RemoteProvider();
                    output = remoteProvider.PasswordStrengthRegularExpression;
                    DisposeRemoteProvider(remoteProvider);
                }
                catch (Exception e)
                {
                    if (LogExceptions)
                    {
                        _log.Error("Failed to get property.", e);
                    }
                    throw;
                }
                return output;
            }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get
            {
                bool output;

                try
                {
                    IMembershipService remoteProvider = RemoteProvider();
                    output = remoteProvider.RequiresQuestionAndAnswer;
                    DisposeRemoteProvider(remoteProvider);
                }
                catch (Exception e)
                {
                    if (LogExceptions)
                    {
                        _log.Error("Failed to get property.", e);
                    }
                    throw;
                }
                return output;
            }
        }

        public override bool RequiresUniqueEmail
        {
            get
            {
                bool output;

                try
                {
                    IMembershipService remoteProvider = RemoteProvider();
                    output = remoteProvider.RequiresUniqueEmail;
                    DisposeRemoteProvider(remoteProvider);
                }
                catch (Exception e)
                {
                    if (LogExceptions)
                    {
                        _log.Error("Failed to get property.", e);
                    }
                    throw;
                }
                return output;
            }
        }

        public override string ResetPassword(string username, string answer)
        {
            string output;

            try
            {
                IMembershipService remoteProvider = RemoteProvider();
                output = remoteProvider.ResetPassword(username, answer);
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to reset password.", e);
                }
                throw;
            }
            return output;
        }

        public override bool UnlockUser(string userName)
        {
            bool output;

            try
            {
                IMembershipService remoteProvider = RemoteProvider();
                output = remoteProvider.UnlockUser(userName);
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to unlock user.", e);
                }
                throw;
            }
            return output;
        }

        public override void UpdateUser(MembershipUser user)
        {
            try
            {
                IMembershipService remoteProvider = RemoteProvider();
                remoteProvider.UpdateUser(user);
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to update user.", e);
                }
                throw;
            }
        }

        public override bool ValidateUser(string username, string password)
        {
            bool output;

            try
            {
                IMembershipService remoteProvider = RemoteProvider();
                output = remoteProvider.ValidateUser(username, password);
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to validate user.", e);
                }
                throw;
            }
            return output;
        }
    }
}