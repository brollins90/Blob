using Blob.Contracts.Security;
using log4net;
using System;
using System.Collections.Specialized;
using System.ServiceModel;
using System.Web.Security;

namespace Blob.Proxies
{
    public class RoleClient : RoleProvider
    {
        private readonly ILog _log;
        private static ChannelFactory<IRoleService> _channelFactory;

        public RoleClient()
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
                    config.Remove("description");
                    config.Add("description", Description);
                }

                base.Initialize(name, config);

                _log.Debug(string.Format("Remote provider type is {0}", ProxyProviderName));
            }
            catch (Exception e)
            {
                _log.Error("Failed to load role provider client.", e);
                throw;
            }
        }

        /// <summary>
        /// After the caller has completed its work with the object,
        /// it should then pass the object to DisposeProvider to close
        /// the connection.
        /// </summary>
        private IRoleService RemoteProvider()
        {
            if (_channelFactory == null)
            {
                _channelFactory = new ChannelFactory<IRoleService>("RoleService");
            }

            IRoleService provider = _channelFactory.CreateChannel();
            provider.SetProvider(ProxyProviderName);

            return provider;
        }

        /// <summary>
        /// This method should be called to handle closing the 
        /// connected proxy object.
        /// </summary>
        private void DisposeRemoteProvider(IRoleService remoteProvider)
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


        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            try
            {
                IRoleService remoteProvider = RemoteProvider();
                remoteProvider.AddUsersToRoles(usernames, roleNames);
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to add users to roles.", e);
                }
                throw;
            }
        }

        public override void CreateRole(string roleName)
        {
            try
            {
                IRoleService remoteProvider = RemoteProvider();
                remoteProvider.CreateRole(roleName);
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to create role.", e);
                }
                throw;
            }
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            bool output;

            try
            {
                IRoleService remoteProvider = RemoteProvider();
                output = remoteProvider.DeleteRole(roleName, throwOnPopulatedRole);
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to delete.", e);
                }
                throw;
            }
            return output;
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            string[] output;

            try
            {
                IRoleService remoteProvider = RemoteProvider();
                output = remoteProvider.FindUsersInRole(roleName, usernameToMatch);
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to find users in role.", e);
                }
                throw;
            }
            return output;
        }

        public override string[] GetAllRoles()
        {
            string[] output;

            try
            {
                IRoleService remoteProvider = RemoteProvider();
                output = remoteProvider.GetAllRoles();
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to get all roles.", e);
                }
                throw;
            }
            return output;
        }

        public override string[] GetRolesForUser(string username)
        {
            string[] output;

            try
            {
                IRoleService remoteProvider = RemoteProvider();
                output = remoteProvider.GetRolesForUser(username);
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to get roles for user.", e);
                }
                throw;
            }
            return output;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            string[] output;

            try
            {
                IRoleService remoteProvider = RemoteProvider();
                output = remoteProvider.GetUsersInRole(roleName);
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to get users in role.", e);
                }
                throw;
            }
            return output;
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            bool output;

            try
            {
                IRoleService remoteProvider = RemoteProvider();
                output = remoteProvider.IsUserInRole(username, roleName);
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to see if user is in role.", e);
                }
                throw;
            }
            return output;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            try
            {
                IRoleService remoteProvider = RemoteProvider();
                remoteProvider.RemoveUsersFromRoles(usernames, roleNames);
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to remove users from roles.", e);
                }
                throw;
            }
        }

        public override bool RoleExists(string roleName)
        {
            bool output;

            try
            {
                IRoleService remoteProvider = RemoteProvider();
                output = remoteProvider.RoleExists(roleName);
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to see if role exists.", e);
                }
                throw;
            }
            return output;
        }
    }
}