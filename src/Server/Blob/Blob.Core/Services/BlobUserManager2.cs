using System;
using System.Collections.Generic;
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
    public class BlobUserManager2 : IUserService
    {
        private readonly ILog _log;
        private readonly BlobDbContext _context;
        private readonly BlobNotificationScheduleManager _notificationScheduleService;

        private readonly DateTime _oldestDateTime = DateTime.Parse("2010-01-01").ToUniversalTime();

        public BlobUserManager2(ILog log, BlobDbContext context, BlobNotificationScheduleManager notificationScheduleService)
        {
            _log = log;
            _log.Debug("Constructing BlobUserManager2");
            _context = context;
            _notificationScheduleService = notificationScheduleService;
        }

        public async Task<BlobResultDto> CreateUserAsync(CreateUserDto dto)
        {
            User newUser = new User
            {
                AccessFailedCount = 0,
                CreateDateUtc = DateTime.UtcNow,
                CustomerId = dto.CustomerId,
                Email = dto.Email,
                EmailConfirmed = false,
                Enabled = true,
                Id = dto.UserId,
                LastActivityDate = _oldestDateTime,
                LockoutEnabled = false,
                LockoutEndDateUtc = _oldestDateTime,
                PasswordHash = dto.Password,
                UserName = dto.UserName
            };
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            IEnumerable<NotificationScheduleListItemVm> schedules = await _notificationScheduleService.GetAllNotificationSchedules();

            UserProfile up = new UserProfile
                             {
                                 EmailNotificationScheduleId = schedules.First().ScheduleId,
                                 SendEmailNotifications = false,
                                 UserId = newUser.Id
                             };

            _context.Set<UserProfile>().Add(up);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return BlobResultDto.Success;
        }

        public async Task<BlobResultDto> DisableUserAsync(DisableUserDto dto)
        {
            User user = _context.Users.Find(dto.UserId);
            user.Enabled = false;

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return BlobResultDto.Success;
        }

        public async Task<BlobResultDto> EnableUserAsync(EnableUserDto dto)
        {
            User user = _context.Users.Find(dto.UserId);
            user.Enabled = true;

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return BlobResultDto.Success;
        }

        public async Task<BlobResultDto> UpdateUserAsync(UpdateUserDto dto)
        {
            User user = _context.Users.Find(dto.UserId);
            // todo: can username change?
            //user.UserName = dto.UserName;
            if (!string.IsNullOrEmpty(dto.Email) && !user.Email.Equals(dto.Email))
            {
                user.Email = dto.Email;
                user.EmailConfirmed = false;
                user.LastActivityDate = DateTime.UtcNow;
            }

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return BlobResultDto.Success;
        }

        public async Task<UserDisableVm> GetUserDisableVmAsync(Guid userId)
        {
            return await (from user in _context.Users
                          where user.Id == userId
                          select new UserDisableVm
                          {
                              Email = user.Email,
                              Enabled = user.Enabled,
                              UserId = user.Id,
                              UserName = user.UserName
                          }).SingleAsync().ConfigureAwait(false);
        }

        public async Task<UserEnableVm> GetUserEnableVmAsync(Guid userId)
        {
            return await (from user in _context.Users
                          where user.Id == userId
                          select new UserEnableVm
                          {
                              Email = user.Email,
                              Enabled = user.Enabled,
                              UserId = user.Id,
                              UserName = user.UserName
                          }).SingleAsync().ConfigureAwait(false);
        }

        public async Task<UserPageVm> GetUserPageVmAsync(Guid customerId, int pageNum = 1, int pageSize = 10)
        {
            var pNum = pageNum < 1 ? 0 : pageNum - 1;

            var count = _context.Users.Where(x => x.CustomerId.Equals(customerId)).FutureCount();
            var devices = _context.Users
                .Where(x => x.CustomerId.Equals(customerId))
                .OrderBy(x => x.UserName).ThenBy(x => x.Email)
                .Skip(pNum * pageSize).Take(pageSize).Future();

            // define future queries before any of them execute
            var pCount = ((count / pageSize) + (count % pageSize) == 0 ? 0 : 1);
            return await Task.FromResult(new UserPageVm
            {
                TotalCount = count,
                PageCount = pCount,
                PageNum = pNum + 1,
                PageSize = pageSize,
                Items = devices.Select(x => new UserListItemVm
                {
                    Email = x.Email,
                    Enabled = x.Enabled,
                    UserId = x.Id,
                    UserName = x.UserName
                }),
            }).ConfigureAwait(false);
        }

        public async Task<UserSingleVm> GetUserSingleVmAsync(Guid userId)
        {
            // NotificationSchedule
            return await (from user in _context.Users.Include("Customers")
                          join p in _context.UserProfiles on user.Id equals p.UserId
                          where user.Id == userId
                          select new UserSingleVm
                          {
                              CreateDate = user.CreateDateUtc,
                              CustomerName = user.Customer.Name,
                              Email = user.Email,
                              EmailConfirmed = user.EmailConfirmed,
                              EmailNotificationsEnabled = p.SendEmailNotifications,
                              Enabled = user.Enabled,
                              HasPassword = (user.PasswordHash != null),
                              HasSecurityStamp = (user.SecurityStamp != null),
                              LastActivityDate = user.LastActivityDate,
                              UserId = user.Id,
                              UserName = user.UserName,
                              NotificationSchedule = new NotificationScheduleListItemVm { Name = p.EmailNotificationSchedule.Name, ScheduleId = p.EmailNotificationSchedule.Id },
                          }).SingleAsync().ConfigureAwait(false);
        }

        public async Task<UserUpdateVm> GetUserUpdateVmAsync(Guid userId)
        {
            var availableSchedules = await _notificationScheduleService.GetAllNotificationSchedules();
            var u = await (from p in _context.UserProfiles.Include("User")
                           where p.UserId == userId
                           select new UserUpdateVm
                           {
                               UserId = p.User.Id,
                               Email = p.User.Email,
                               UserName = p.User.UserName,
                               ScheduleId = p.EmailNotificationScheduleId
                           }).SingleAsync().ConfigureAwait(false);
            u.AvailableSchedules = availableSchedules;
            return u;
        }
    }
}
