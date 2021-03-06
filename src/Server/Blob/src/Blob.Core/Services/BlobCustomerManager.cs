﻿using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Blob.Contracts.Models;
using Blob.Contracts.Models.ViewModels;
using Blob.Contracts.Services;
using Blob.Core.Models;
using EntityFramework.Extensions;
using log4net;

namespace Blob.Core.Services
{
    public class BlobCustomerManager : ICustomerService
    {
        private readonly ILog _log;
        private readonly BlobDbContext _context;
        private readonly ICustomerGroupService _customerGroupManager;
        private readonly IUserService _userManager;

        public BlobCustomerManager(ILog log, BlobDbContext context, ICustomerGroupService customerGroupManager, IUserService userManager)
        {
            _log = log;
            _log.Debug("Constructing BlobCustomerManager");
            _context = context;
            _customerGroupManager = customerGroupManager;
            _userManager = userManager;
        }

        public async Task<BlobResultDto> DisableCustomerAsync(DisableCustomerDto dto)
        {
            _log.Debug(string.Format("DisableCustomerAsync({0})", dto.CustomerId));
            Customer customer = _context.Customers.Find(dto.CustomerId);
            customer.Enabled = false;
            // todo: disable all devices and users for customer ?

            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return BlobResultDto.Success;
        }

        public async Task<BlobResultDto> EnableCustomerAsync(EnableCustomerDto dto)
        {
            _log.Debug(string.Format("EnableCustomerAsync({0})", dto.CustomerId));
            Customer customer = _context.Customers.Find(dto.CustomerId);
            customer.Enabled = true;

            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return BlobResultDto.Success;
        }

        public async Task<BlobResultDto> RegisterCustomerAsync(RegisterCustomerDto dto)
        {
            _log.Debug(string.Format("RegisterCustomerAsync({0})", dto.CustomerName));

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

            
            //// create first user
            //User defaultUser = new User
            //{
            //    AccessFailedCount = 0,
            //    CreateDateUtc = DateTime.UtcNow,
            //    EmailConfirmed = true,
            //    Enabled = true,
            //    LastActivityDate = DateTime.UtcNow,
            //    LockoutEnabled = false,
            //    LockoutEndDateUtc = DateTime.UtcNow.AddDays(-1),
            //};
            //_context.Users.Add(defaultUser);
            await _userManager.CreateUserAsync(new CreateUserDto
            {
                CustomerId = dto.DefaultUser.CustomerId,
                Email = dto.DefaultUser.Email,
                Password = dto.DefaultUser.Password,
                UserId = dto.DefaultUser.UserId,
                UserName = dto.DefaultUser.UserName
            });


            // create admin group
            Guid adminGroupId = Guid.NewGuid();
            await _customerGroupManager.CreateCustomerGroupAsync(new CreateCustomerGroupDto
                                                           {
                                                               CustomerId = customer.Id,
                                                               Description = "Admins",
                                                               GroupId = adminGroupId,
                                                               Name = "Admins"
                                                           });

            // add user to admin group
            await _customerGroupManager.AddUserToCustomerGroupAsync(new AddUserToCustomerGroupDto {GroupId = adminGroupId, UserId = dto.DefaultUser.UserId});
            
            // save stuff
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return BlobResultDto.Success;
        }

        public async Task<BlobResultDto> UpdateCustomerAsync(UpdateCustomerDto dto)
        {
            _log.Debug(string.Format("UpdateCustomerAsync({0})", dto.CustomerId));
            Customer customer = _context.Customers.Find(dto.CustomerId);
            customer.Name = dto.Name;

            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return BlobResultDto.Success;
        }

        public async Task<CustomerDisableVm> GetCustomerDisableVmAsync(Guid customerId)
        {
            _log.Debug(string.Format("GetCustomerDisableVmAsync({0})", customerId));
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
            _log.Debug(string.Format("GetCustomerEnableVmAsync({0})", customerId));
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
            _log.Debug(string.Format("GetCustomerSingleVmAsync({0})", customerId));
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
            _log.Debug(string.Format("GetCustomerUpdateVmAsync({0})", customerId));
            return await (from customer in _context.Customers
                          where customer.Id == customerId
                          select new CustomerUpdateVm
                          {
                              CustomerId = customer.Id,
                              CustomerName = customer.Name
                          }).SingleAsync().ConfigureAwait(false);
        }


        public async Task<CustomerPageVm> GetCustomerPageVmAsync(Guid searchId, int pageNum, int pageSize)
        {
            _log.Debug(string.Format("GetCustomerPageVmAsync({0}, {1}, {2})", searchId, pageNum, pageSize));

            var pNum = pageNum < 1 ? 0 : pageNum - 1;

            var count = _context.Customers.Where(x => true).FutureCount();
            var devices = _context.Customers
                .Where(x => true)
                .OrderByDescending(x => x.Name).ThenBy(x => x.CreateDateUtc)
                .Skip(pNum * pageSize).Take(pageSize).Future();

            // define future queries before any of them execute
            var pCount = ((count / pageSize) + (count % pageSize) == 0 ? 0 : 1);
            var items = await Task.FromResult(new CustomerPageVm
            {
                TotalCount = count,
                PageCount = pCount,
                PageNum = pNum + 1,
                PageSize = pageSize,
                Items = devices.Select(x => new CustomerListItemVm
                {
                    CreateDate = x.CreateDateUtc,
                    CustomerId = x.Id,
                    Enabled = x.Enabled,
                    Name = x.Name
                }),
            }).ConfigureAwait(false);
            return items;
        }
    }
}
