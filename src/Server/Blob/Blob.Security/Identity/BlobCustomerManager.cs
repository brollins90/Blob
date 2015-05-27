using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Blob.Contracts.Models;
using Blob.Core;
using Blob.Core.Identity;
using Blob.Core.Identity.Store;
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

        public IQueryable<Customer> Customers { get { return _customerStore.Customers; } }



        public async Task<BlobResultDto> DeleteCustomerAsync(Guid customerId)
        {
            Customer customer = await _customerStore.FindCustomerByIdAsync(customerId).ConfigureAwait(true);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync().ConfigureAwait(true);
            return BlobResultDto.Success;
        }

        public async Task<BlobResultDto> RegisterCustomerAsync(RegisterCustomerDto dto)
        {

            // check if customer exists
            Customer custExist = await _customerStore.FindCustomerByIdAsync(dto.CustomerId);
            if (custExist != null)
            {
                return new BlobResultDto("Customer id already exists");
            }

            // create the customer
            Customer customer = new Customer
            {
                CreateDateUtc = DateTime.UtcNow,
                Enabled = true,
                Id = dto.CustomerId,
                Name = dto.CustomerName
            };

            await _customerStore.CreateCustomerAsync(customer);
            _log.Info(string.Format("Customer {0} created with id {1}", dto.CustomerName, dto.CustomerId));

            // create first user
            User defaultUser = new User
            {
                AccessFailedCount = 0,
                CreateDateUtc = DateTime.UtcNow,
                CustomerId = dto.DefaultUser.CustomerId,
                Email = dto.DefaultUser.Email,
                EmailConfirmed = true,
                Enabled = true,
                Id = dto.DefaultUser.UserId,
                LastActivityDate = DateTime.UtcNow,
                LockoutEnabled = false,
                LockoutEndDateUtc = DateTime.UtcNow.AddDays(-1),
                PasswordHash = dto.DefaultUser.Password,
                UserName = dto.DefaultUser.UserName
            };
            await _userManager.CreateBlobUserAsync(defaultUser);


            // create admin group
            CustomerGroup adminGroup = new CustomerGroup
                                       {
                                           CustomerId = customer.Id,
                                           Description = "Admins",
                                           Id = Guid.NewGuid(),
                                           Name = "Admins"
                                       };
            await _customerStore.CreateGroupAsync(adminGroup);

            // add user to admin group
            await _customerStore.AddUserToGroupAsync(adminGroup, defaultUser.Id);

            return BlobResultDto.Success;
        }
    }
}
