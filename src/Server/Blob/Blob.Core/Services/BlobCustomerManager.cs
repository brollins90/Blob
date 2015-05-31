using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Blob.Contracts.Models;
using Blob.Contracts.Models.ViewModels;
using Blob.Contracts.Services;
using Blob.Core.Models;
using log4net;

namespace Blob.Core.Services
{
    public class BlobCustomerManager : ICustomerService
    {
        private readonly ILog _log;
        private readonly BlobDbContext _context;

        public BlobCustomerManager(ILog log, BlobDbContext context)
        {
            _log = log;
            _context = context;
        }


        //public async Task<BlobResultDto> DeleteCustomerAsync(Guid customerId)
        //{
        //    Customer customer = await _customerStore.FindCustomerByIdAsync(customerId).ConfigureAwait(true);
        //    _context.Customers.Remove(customer);
        //    await _context.SaveChangesAsync().ConfigureAwait(true);
        //    return BlobResultDto.Success;
        //}


        public async Task<BlobResultDto> DisableCustomerAsync(DisableCustomerDto dto)
        {
            _log.Info(string.Format("Disabling customer {0}", dto.CustomerId));
            Customer customer = _context.Customers.Find(dto.CustomerId);
            customer.Enabled = false;
            
            
            // todo: disable all devices and users for customer ?



            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync().ConfigureAwait(false);

            _log.Info(string.Format("Disabled customer {0}", dto.CustomerId));
            return BlobResultDto.Success;
        }

        public async Task<BlobResultDto> EnableCustomerAsync(EnableCustomerDto dto)
        {
            Customer customer = _context.Customers.Find(dto.CustomerId);
            customer.Enabled = true;

            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return BlobResultDto.Success;
        }

        public async Task<BlobResultDto> RegisterCustomerAsync(RegisterCustomerDto dto)
        {

            // check if customer exists
            Customer customer = _context.Customers.Find(dto.CustomerId);
            if (customer != null)
            {
                return new BlobResultDto("Customer id already exists");
            }

            // create the customer
            customer = new Customer
            {
                CreateDateUtc = DateTime.UtcNow,
                Enabled = true,
                Id = dto.CustomerId,
                Name = dto.CustomerName
            };

            _context.Customers.Add(customer);
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
            _context.Users.Add(defaultUser);


            // create admin group
            CustomerGroup adminGroup = new CustomerGroup
                                       {
                                           CustomerId = customer.Id,
                                           Description = "Admins",
                                           Id = Guid.NewGuid(),
                                           Name = "Admins"
                                       };
            _context.Set<CustomerGroup>().Add(adminGroup);


            // add user to admin group
            var ur = new CustomerGroupUser { GroupId = adminGroup.Id, UserId = defaultUser.Id };
            _context.Set<CustomerGroupUser>().Add(ur);
            
            // save stuff
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return BlobResultDto.Success;
        }

        public async Task<BlobResultDto> UpdateCustomerAsync(UpdateCustomerDto dto)
        {
            Customer customer = _context.Customers.Find(dto.CustomerId);
            customer.Name = dto.Name;

            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return BlobResultDto.Success;
        }

        public async Task<CustomerDisableVm> GetCustomerDisableVmAsync(Guid customerId)
        {
            return await (from customer in _context.Customers
                          where customer.Id == customerId
                          select new CustomerDisableVm
                          {
                              CustomerId = customer.Id,
                              CustomerName = customer.Name,
                              Enabled = customer.Enabled
                          }).SingleAsync().ConfigureAwait(false);
        }

        public async Task<CustomerEnableVm> GetCustomerEnableVmAsync(Guid customerId)
        {
            return await (from customer in _context.Customers
                          where customer.Id == customerId
                          select new CustomerEnableVm
                          {
                              CustomerId = customer.Id,
                              CustomerName = customer.Name,
                              Enabled = customer.Enabled
                          }).SingleAsync().ConfigureAwait(false);
        }

        public async Task<CustomerSingleVm> GetCustomerSingleVmAsync(Guid customerId)
        {
            return await _context.Customers
                .Include("Devices").Include("Users")
                                .Where(x => x.Id == customerId)
                                .Select(cust => new CustomerSingleVm
                                {
                                    CreateDate = cust.CreateDateUtc,
                                    CustomerId = cust.Id,
                                    Name = cust.Name,
                                    DeviceCount = cust.Devices.Count,
                                }).SingleAsync();
        }

        public async Task<CustomerUpdateVm> GetCustomerUpdateVmAsync(Guid customerId)
        {
            return await (from customer in _context.Customers
                          where customer.Id == customerId
                          select new CustomerUpdateVm
                          {
                              CustomerId = customer.Id,
                              CustomerName = customer.Name
                          }).SingleAsync().ConfigureAwait(false);
        }

        public Task<IEnumerable<CustomerGroupRoleListItem>> GetCustomerRolesAsync(Guid customerId)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerGroupPageVm> GetCustomerGroupPageVmAsync(Guid customerId, int pageNum = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }



    }
}
