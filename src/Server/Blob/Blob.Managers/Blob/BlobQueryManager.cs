using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Blob.Contracts.ViewModels;
using Blob.Data;
using log4net;

namespace Blob.Managers.Blob
{
    public interface IBlobQueryManager
    {
        Task<CustomerDetailsVm> GetCustomerDetailsViewModelByIdAsync(Guid customerId);
    }

    public class BlobQueryManager : IBlobQueryManager
    {
        private readonly ILog _log;

        public BlobQueryManager(BlobDbContext context, ILog log)
        {
            _log = log;
            _log.Debug("Constructing BlobQueryManager");
            Context = context;
        }
        public BlobDbContext Context { get; private set; }


        // Customer
        public async Task<CustomerDetailsVm> GetCustomerDetailsViewModelByIdAsync(Guid customerId)
        {
            return await (from cust in Context.Customers.Include("Devices").Include("DeviceTypes").Include("Users")
                          where cust.Id == customerId
                          select new CustomerDetailsVm
                                 {
                                     CreateDate = cust.CreateDate,
                                     CustomerId = cust.Id,
                                     Name = cust.Name,
                                     Devices = (from d in cust.Devices
                                                select new DeviceListVm
                                                       {
                                                           DeviceName = d.DeviceName,
                                                           DeviceType = d.DeviceType.Value,
                                                           DeviceId = d.Id,
                                                           LastActivityDate = d.LastActivityDate,
                                                           Status = (d.AlertLevel == 0)
                                                               ? "Ok"
                                                               : (d.AlertLevel == 1)
                                                               ? "Warning"
                                                               : (d.AlertLevel == 2)
                                                               ? "Critical"
                                                               : "Unknown"
                                                       }),
                                     Users = (from u in cust.Users
                                              select new UserListVm
                                                     {
                                                         UserId = u.Id,
                                                         UserName = u.UserName
                                                     })
                                 }).SingleAsync();
        }
    }
}
