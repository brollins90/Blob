using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Blob.Contracts.Models;
using Blob.Contracts.ServiceContracts;
using Blob.Core;
using Blob.Core.Identity;
using Blob.Core.Models;
using log4net;
using Microsoft.AspNet.Identity;

namespace Blob.Security.Identity
{
    // http://typecastexception.com/post/2014/08/10/ASPNET-Identity-20-Implementing-Group-Based-Permissions-Management.aspx

    
    public class BlobCustomerManager 
    {
        private readonly ILog _log;
        private readonly BlobCustomerStore _customerStore;
        private readonly BlobDbContext _context;
        private readonly BlobUserManager _userManager;
        private readonly BlobRoleManager _roleManager;

        public BlobCustomerManager(ILog log, BlobCustomerStore customerStore, BlobDbContext context, BlobUserManager userManager, BlobRoleManager roleManager)
        {
            _log = log;
            _customerStore = customerStore;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IQueryable<Customer> Customers
        {
            get
            {
                return _customerStore.Customers;
            }
        }


        //public async Task<IdentityResult> RemoveUserFromCustomersAsync(Guid userId)
        //{
        //    throw new NotImplementedException();
        //    //return await SetUserGroupsAsync(userId).ConfigureAwait(false);
        //}

        public async Task<IdentityResultDto> RegisterCustomerAsync(RegisterCustomerDto dto)
        {
            //_log.Info(string.Format("Registering new customer {0}", dto.CustomerName));
            
            //// check if customer exists
            //Customer custExist = await _customerStore.FindByIdAsync(dto.CustomerId);
            //if (custExist != null)
            //{
            //    return new IdentityResultDto("The Customer already exists");
            //}

            //// create the customer
            //Customer customer = new Customer
            //{
            //    CreateDateUtc = DateTime.UtcNow,
            //    Enabled = true,
            //    Id = dto.CustomerId,
            //    Name = dto.CustomerName
            //};
            //await _customerStore.CreateAsync(customer).ConfigureAwait(false);
            //var cust = _customerStore.FindByIdAsync(customer.Id);
            //_log.Info(string.Format("Customer {0} created with id {1}", dto.CustomerName, dto.CustomerId));


            //// create the default user
            //if (dto.DefaultUser != null)
            //{
            //    _log.Info(string.Format("Creating default user for {0} with name {1}", dto.CustomerName, dto.DefaultUser.UserName));
            //    dto.DefaultUser.CustomerId = customer.Id;

            //    IdentityResultDto result = await _userManager.CreateAsync(new UserDto {Email = dto.DefaultUser.Email, Id = dto.DefaultUser.UserId.ToString(), UserName = dto.DefaultUser.UserName}, dto.DefaultUser.Password);
                
            //    _log.Info(string.Format("User {0} created with id {1}", dto.DefaultUser.UserName, dto.DefaultUser.UserId));
            //}

            return new IdentityResultDto(true);
        }


        public async Task<IdentityResult> DeleteCustomerAsync(Guid customerId)
        {

            // my code...
            //Customer customer = Context.Customers.Find(dto.CustomerId);
            //customer.Enabled = false;
            //// todo: disable all devices and users for customer ?

            //Context.Entry(customer).State = EntityState.Modified;
            //await Context.SaveChangesAsync().ConfigureAwait(false);

            Customer customer = await this.FindByIdAsync(customerId).ConfigureAwait(true);
            if (customer == null)
            {
                throw new ArgumentNullException("Customer");
            }

            var currentCustomerUsers = (await this.GetCustomerUsersAsync(customerId)).ToList();
            // remove the roles from the group:
            customer.CustomerRoles.Clear();

            // Remove all the users:
            customer.CustomerUsers.Clear();

            // Remove the group itself:
            _context.Customers.Remove(customer);

            await _context.SaveChangesAsync().ConfigureAwait(true);

            // Reset all the user roles:
            foreach (var user in currentCustomerUsers)
            {
                // todo:
                //await this.RefreshUserGroupRolesAsync(user.Id).ConfigureAwait(true);
            }
            return IdentityResult.Success;
        }

        public async Task<Customer> FindByIdAsync(Guid customerId)
        {
            return await _customerStore.FindByIdAsync(customerId).ConfigureAwait(false);
        }


        public async Task<IEnumerable<Role>> GetGroupRolesAsync(Guid customerId)
        {
            Customer customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == customerId).ConfigureAwait(true);
            List<Role> allRoles = await _roleManager.Roles.ToListAsync().ConfigureAwait(true);
            List<Role> customerRoles = (from r in allRoles
                                     where customer.CustomerRoles
                                       .Any(ap => ap.RoleId == r.Id)
                                     select r).ToList();
            return customerRoles;
        }

        public async Task<IEnumerable<User>> GetCustomerUsersAsync(Guid customerId)
        {
            Customer customer = await FindByIdAsync(customerId).ConfigureAwait(true);
            List<User> users = new List<User>();
            foreach (CustomerUser customerUser in customer.CustomerUsers)
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == customerUser.UserId);
                users.Add(user);
            }
            return users;
        }

        public async Task<IEnumerable<Role>> GetCustomerRolesAsync(Guid customerId)
        {
            Customer customer = await FindByIdAsync(customerId).ConfigureAwait(true);
            List<Role> roles = new List<Role>();
            foreach (CustomerRole customerRole in customer.CustomerRoles)
            {
                Role role = await _context.Roles.FirstOrDefaultAsync(u => u.Id == customerRole.RoleId);
                roles.Add(role);
            }
            return roles;
        }

        //public async Task<IEnumerable<CustomerGroup>> GetUserGroupsAsync(Guid userId)
        //{
        //    return await (from g in Groups
        //                  where g.Users
        //                    .Any(u => u.UserId == userId)
        //                  select g).ToListAsync().ConfigureAwait(false);
        //}

        //public async Task<IEnumerable<CustomerRole>> GetCustomerRolesAsync(Guid userId)
        //{
        //    IEnumerable<Customer> customers = await this.GetUserGroupsAsync(userId).ConfigureAwait(true);
        //    List<BlobGroupRole> userGroupRoles = new List<BlobGroupRole>();
        //    foreach (CustomerGroup group in customers)
        //    {
        //        userGroupRoles.AddRange(group.Roles.ToArray());
        //    }
        //    return userGroupRoles;
        //}

        //public async Task<IdentityResult> RefreshUserGroupRolesAsync(Guid userId)
        //{
        //    UserDto user = await _userManager.FindByIdAsync(userId).ConfigureAwait(true);
        //    if (user == null)
        //    {
        //        throw new ArgumentNullException("User");
        //    }

        //    // Remove user from previous roles:
        //    IList<string> oldUserRoles = await _userManager.GetRolesAsync(userId).ConfigureAwait(true);
        //    if (oldUserRoles.Count > 0)
        //    {
        //        await _userManager.RemoveFromRolesAsync(userId, oldUserRoles.ToArray());
        //    }

        //    // Find the roles this user is entitled to from group membership:
        //    var newGroupRoles = await this.GetCustomerRolesAsync(userId).ConfigureAwait(true);

        //    // Get the damn role names:
        //    var allRoles = await _roleManager.Roles.ToListAsync().ConfigureAwait(true);
        //    var addTheseRoles = allRoles.Where(r => newGroupRoles.Any(gr => gr.RoleId == r.Id));
        //    var roleNames = addTheseRoles.Select(n => n.Name).ToArray();

        //    // Add the user to the proper roles
        //    await _userManager.AddToRolesAsync(userId, roleNames).ConfigureAwait(true);

        //    return IdentityResult.Success;
        //}

        //public async Task<IdentityResult> SetGroupRolesAsync(Guid groupId, params string[] roleNames)
        //{
        //    // Clear all the roles associated with this group:
        //    CustomerGroup thisGroup = await FindByIdAsync(groupId);
        //    thisGroup.Roles.Clear();
        //    await _context.SaveChangesAsync();

        //    // Add the new roles passed in:
        //    var newRoles = _roleManager.Roles.Where(r => roleNames.Any(n => n == r.Name));
        //    foreach (var role in newRoles)
        //    {
        //        thisGroup.Roles.Add(new BlobGroupRole
        //                                {
        //                                    GroupId = groupId,
        //                                    RoleId = role.Id
        //                                });
        //    }
        //    await _context.SaveChangesAsync();

        //    // Reset the roles for all affected users:
        //    foreach (var groupUser in thisGroup.Users)
        //    {
        //        await this.RefreshUserGroupRolesAsync(groupUser.UserId);
        //    }
        //    return IdentityResult.Success;
        //}

        //public async Task<IdentityResult> SetUserGroupsAsync(Guid userId, params Guid[] groupIds)
        //{
        //    // Clear current group membership:
        //    var currentGroups = await GetUserGroupsAsync(userId);
        //    foreach (var group in currentGroups)
        //    {
        //        group.Users.Remove(group.Users.FirstOrDefault(gr => gr.UserId == userId));
        //    }
        //    await _context.SaveChangesAsync();

        //    // Add the user to the new groups:
        //    foreach (Guid groupId in groupIds)
        //    {
        //        CustomerGroup newGroup = await this.FindByIdAsync(groupId);
        //        newGroup.Users.Add(new BlobUserGroup
        //        {
        //            UserId = userId,
        //            GroupId = groupId
        //        });
        //    }
        //    await _context.SaveChangesAsync();

        //    await this.RefreshUserGroupRolesAsync(userId);
        //    return IdentityResult.Success;
        //}

        //public async Task<IdentityResult> UpdateGroupAsync(CustomerGroup group)
        //{
        //    await _customerStore.UpdateAsync(group).ConfigureAwait(true);
        //    foreach (var groupUser in group.Users)
        //    {
        //        await this.RefreshUserGroupRolesAsync(groupUser.UserId).ConfigureAwait(true);
        //    }
        //    return IdentityResult.Success;
        //}
    }
}
