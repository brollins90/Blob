using Blob.Core.Domain;
using Blob.Data;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.Data.Entity;
using System.Linq;
using System.Web.Security;

namespace Blob.Security
{
    public class BlobRoleProvider : RoleProvider
    {
        private readonly ILog _log;
        private readonly string _dbConnectionString;

        public BlobRoleProvider()
        {
            _log = LogManager.GetLogger("MembershipLogger");
            _dbConnectionString = ConfigurationManager.ConnectionStrings["BlobDbContext"].ConnectionString;
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            _log.Debug("Initializing BlobRoleProvider");
            if (config == null)
                throw new ArgumentNullException("config");

            if (string.IsNullOrEmpty(name))
                name = "BlobRoleProvider";

            if (string.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Blob role manager");
            }

            base.Initialize(name, config);

            _applicationName = GetConfigValue(config["applicationName"], System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
            _logExceptions = Convert.ToBoolean(GetConfigValue(config["logExceptions"], "false"));
        }

        #region Override Properties and Fields

        private string _applicationName;
        private bool _logExceptions;

        /// <summary>
        /// Gets or sets the name of the application to store and retrieve role information for.
        /// </summary>
        /// <returns>The name of the application to store and retrieve role information for.</returns>
        public override string ApplicationName
        {
            get { return _applicationName; }
            set { _applicationName = value; }
        }

        /// <summary>
        /// Indicates whether to log exceptions
        /// </summary>
        /// <returns>true if the membership provider is configured to log exceptions; otherwise, false. The default is false.</returns>
        public bool LogExceptions
        {
            get { return _logExceptions; }
        }

        #endregion

        /// <summary>
        /// Adds the specified user names to the specified roles for the configured applicationName.
        /// </summary>
        /// <param name="usernames">A string array of user names to be added to the specified roles.</param>
        /// <param name="roleNames">A string array of the role names to add the specified user names to.</param>
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            if (roleNames.Any(roleName => !RoleExists(roleName)))
                throw new ProviderException("Role name not found.");

            foreach (string username in usernames)
            {
                if (username.Contains(","))
                    throw new ArgumentException("User names cannot contain commas.");

                if (roleNames.Any(roleName => IsUserInRole(username, roleName)))
                {
                    throw new ProviderException("User is already in role.");
                }
            }

            try
            {
                using (BlobDbContext context = new BlobDbContext(_dbConnectionString))
                {
                    DbSet<User> users = context.Set<User>();
                    DbSet<Role> roles = context.Set<Role>();
                    foreach (User u in usernames.Select(username => users.FirstOrDefault(x => x.Username.Equals(username))))
                    {
                        foreach (Role r in roleNames.Select(rolename => roles.FirstOrDefault(x => x.Name.Equals(rolename))))
                        {
                            u.Roles.Add(r);
                            context.Entry(u).State = EntityState.Modified;
                            context.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to add users to roles.", e);
                }
                throw new ProviderException("Failed to add users to roles.", e);
            }
        }

        /// <summary>
        /// Adds a new role to the data source for the configured applicationName.
        /// </summary>
        /// <param name="roleName">The name of the role to create.</param>
        public override void CreateRole(string roleName)
        {
            if (roleName.Contains(","))
                throw new ArgumentException("Role names cannot contain commas.");

            if (RoleExists(roleName))
                throw new ProviderException("Role name already exists.");

            try
            {
                using (BlobDbContext context = new BlobDbContext(_dbConnectionString))
                {
                    context.Set<Role>().Add(new Role
                                            {
                                                Id = Guid.NewGuid(),
                                                Name = roleName
                                            });
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to create role.", e);
                }
                throw new ProviderException("Failed to create role.", e);
            }
        }

        /// <summary>
        /// Removes a role from the data source for the configured applicationName.
        /// </summary>
        /// <param name="roleName">The name of the role to delete.</param>
        /// <param name="throwOnPopulatedRole">If true, throw an exception if roleName has one or more members and do not delete roleName.</param>
        /// <returns>true if the role was successfully deleted; otherwise, false.</returns>
        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            try
            {
                using (BlobDbContext context = new BlobDbContext(_dbConnectionString))
                {
                    Role role = context.Set<Role>().Include("Users").FirstOrDefault(x => x.Name.Equals(roleName));
                    
                    if (role != null)
                    {
                        if (throwOnPopulatedRole && role.Users.Any())
                        {
                            throw new ProviderException("Unable to delete role because it is populated.");
                        }

                        List<User> usersToTouch = role.Users.ToList();
                        usersToTouch.ForEach(x => x.Roles.Remove(role));
                        context.SaveChanges();

                        context.Entry(role).State = EntityState.Deleted;
                        context.SaveChanges();
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to delete role.", e);
                }
                throw new ProviderException("Failed to delete role.", e);
            }
            return false; 
        }

        /// <summary>
        /// Gets an array of user names in a role where the user name contains the specified user name to match.
        /// </summary>
        /// <param name="roleName">The role to search in.</param>
        /// <param name="usernameToMatch">The user name to search for.</param>
        /// <returns>A string array containing the names of all the users where the user name matches usernameToMatch and the user is a member of the specified role.</returns>
        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            try
            {
                using (BlobDbContext context = new BlobDbContext(_dbConnectionString))
                {
                    Role role = context.Set<Role>().Include("Users").FirstOrDefault(x => x.Name.Equals(roleName));

                    if (role != null)
                    {
                        IEnumerable<string> usersInRoleThatMatch = role.Users
                            .Where(x => x.Username.Equals(usernameToMatch))
                            .Select(x=>x.Username);
                        return usersInRoleThatMatch.ToArray();
                    }
                }
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to find users in role.", e);
                }
                throw new ProviderException("Failed to find users in role.", e);
            }
            return null; 
        }

        /// <summary>
        /// Gets a list of all the roles for the configured applicationName.
        /// </summary>
        /// <returns>A string array containing the names of all the roles stored in the data source for the configured applicationName.</returns>
        public override string[] GetAllRoles()
        {
            try
            {
                using (BlobDbContext context = new BlobDbContext(_dbConnectionString))
                {
                    DbSet<Role> roles = context.Set<Role>();
                    return roles.Select(x => x.Name).ToArray();
                }
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to get all roles.", e);
                }
                throw new ProviderException("Failed to get all roles.", e);
            }
        }

        /// <summary>
        /// Gets a list of the roles that a specified user is in for the configured applicationName.
        /// </summary>
        /// <param name="username">The user to return a list of roles for.</param>
        /// <returns>A string array containing the names of all the roles that the specified user is in for the configured applicationName.</returns>
        public override string[] GetRolesForUser(string username)
        {
            try
            {
                using (BlobDbContext context = new BlobDbContext(_dbConnectionString))
                {
                    User user = context.Set<User>().Include("Roles").FirstOrDefault(x => x.Username.Equals(username));

                    if (user != null)
                    {
                        IEnumerable<string> rolesForUser = user.Roles.Select(x => x.Name);
                        return rolesForUser.ToArray();
                    }
                }
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to find roles for user.", e);
                }
                throw new ProviderException("Failed to find roles for user.", e);
            }
            return null; 
        }

        /// <summary>
        /// Gets a list of users in the specified role for the configured applicationName.
        /// </summary>
        /// <param name="roleName">The name of the role to get the list of users for.</param>
        /// <returns>A string array containing the names of all the users who are members of the specified role for the configured applicationName.</returns>
        public override string[] GetUsersInRole(string roleName)
        {
            try
            {
                using (BlobDbContext context = new BlobDbContext(_dbConnectionString))
                {
                    Role role = context.Set<Role>().Include("Users").FirstOrDefault(x => x.Name.Equals(roleName));

                    if (role != null)
                    {
                        IEnumerable<string> usersForRole = role.Users.Select(x => x.Username);
                        return usersForRole.ToArray();
                    }
                }
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to find users in role.", e);
                }
                throw new ProviderException("Failed to find users in role.", e);
            }
            return null;
        }

        /// <summary>
        /// Gets a value indicating whether the specified user is in the specified role for the configured applicationName.
        /// </summary>
        /// <param name="username">The user name to search for.</param>
        /// <param name="roleName">The role to search in.</param>
        /// <returns>true if the specified user is in the specified role for the configured applicationName; otherwise, false.</returns>
        public override bool IsUserInRole(string username, string roleName)
        {
            try
            {
                using (BlobDbContext context = new BlobDbContext(_dbConnectionString))
                {
                    User user = context.Set<User>().Include("Roles").FirstOrDefault(x => x.Username.Equals(username));

                    if (user != null)
                    {
                        return user.Roles.Any(x => x.Name.Equals(roleName));
                    }
                }
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to find if user is in role.", e);
                }
                throw new ProviderException("Failed to find if user is in role.", e);
            }
            return false;
        }

        /// <summary>
        /// Removes the specified user names from the specified roles for the configured applicationName.
        /// </summary>
        /// <param name="usernames">A string array of user names to be removed from the specified roles.</param>
        /// <param name="roleNames">A string array of role names to remove the specified user names from.</param>
        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            try
            {
                using (BlobDbContext context = new BlobDbContext(_dbConnectionString))
                {
                    IQueryable<User> users = context.Set<User>().Where(u => usernames.Contains(u.Username));

                    foreach (string roleName in roleNames)
                    {
                        Role role = context.Set<Role>().Include(r => r.Users).SingleOrDefault(r => r.Name.Equals(roleName));
                        if (role != null)
                        {
                            foreach (User user in users)
                            {
                                role.Users.Remove(user);
                            }
                        }
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to remove users from roles.", e);
                }
                throw new ProviderException("Failed to remove users from roles.", e);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the specified role name already exists in the role data source for the configured applicationName.
        /// </summary>
        /// <param name="roleName">The name of the role to search for in the data source.</param>
        /// <returns>true if the role name already exists in the data source for the configured applicationName; otherwise, false.</returns>
        public override bool RoleExists(string roleName)
        {
            try
            {
                using (BlobDbContext context = new BlobDbContext(_dbConnectionString))
                {
                    return context.Set<Role>().Any(x=>x.Name.Equals(roleName));
                }
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to get role.", e);
                }
                throw new ProviderException("Failed to get role.", e);
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
    }
}
