using System;
using System.Collections.Generic;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Blob.Core.Models;
using log4net;
using Microsoft.AspNet.Identity;

namespace Blob.Security.Identity
{
    public class BlobUserValidator : IIdentityValidator<User>
    {
        private readonly ILog _log;

        public BlobUserValidator(BlobUserManager manager)
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _log.Debug("Constructing BlobUserValidator");
            if (manager == null)
            {
                throw new ArgumentNullException("manager");
            }
            AllowOnlyAlphanumericUserNames = true;
            Manager = manager;
        }

        public bool AllowOnlyAlphanumericUserNames { get; set; }
        public bool RequireUniqueEmail { get; set; }

        private BlobUserManager Manager { get; set; }

        public virtual async Task<IdentityResult> ValidateAsync(User item)
        {
            _log.Debug("Constructing ValidateAsync");
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            List<string> errors = new List<string>();
            await ValidateUserName(item, errors).WithCurrentCulture();
            if (RequireUniqueEmail)
            {
                await ValidateEmailAsync(item, errors).WithCurrentCulture();
            }
            return errors.Count > 0 
                ? IdentityResult.Failed(errors.ToArray()) 
                : IdentityResult.Success;
        }

        private async Task ValidateUserName(User user, ICollection<string> errors)
        {
            if (string.IsNullOrWhiteSpace(user.UserName))
            {
                errors.Add(String.Format(CultureInfo.CurrentCulture, Resources.PropertyTooShort, "Name"));
            }
            else if (AllowOnlyAlphanumericUserNames && !Regex.IsMatch(user.UserName, @"^[A-Za-z0-9@_\.]+$"))
            {
                // If any characters are not letters or digits, its an illegal user name
                errors.Add(String.Format(CultureInfo.CurrentCulture, Resources.InvalidUserName, user.UserName));
            }
            else
            {
                User owner = await Manager.FindByNameAsync2(user.UserName).WithCurrentCulture();
                if (owner != null && !EqualityComparer<Guid>.Default.Equals(owner.Id, user.Id))
                {
                    errors.Add(String.Format(CultureInfo.CurrentCulture, Resources.DuplicateName, user.UserName));
                }
            }
        }

        private async Task ValidateEmailAsync(User user, ICollection<string> errors)
        {
            string email = await Manager.GetEmailAsync(user.Id).WithCurrentCulture();
            if (string.IsNullOrWhiteSpace(email))
            {
                errors.Add(String.Format(CultureInfo.CurrentCulture, Resources.PropertyTooShort, "Email"));
                return;
            }
            try
            {
                MailAddress m = new MailAddress(email);
            }
            catch (FormatException)
            {
                errors.Add(String.Format(CultureInfo.CurrentCulture, Resources.InvalidEmail, email));
                return;
            }
            User owner = await Manager.FindByEmailAsync2(email).WithCurrentCulture();
            if (owner != null && !EqualityComparer<Guid>.Default.Equals(owner.Id, user.Id))
            {
                errors.Add(String.Format(CultureInfo.CurrentCulture, Resources.DuplicateEmail, email));
            }
        }
    }
}
