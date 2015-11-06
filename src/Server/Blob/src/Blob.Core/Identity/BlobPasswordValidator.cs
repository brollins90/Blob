using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using log4net;
using Microsoft.AspNet.Identity;

namespace Blob.Core.Identity
{
    public class BlobPasswordValidator : IIdentityValidator<string>
    {
        private readonly ILog _log;

        public BlobPasswordValidator()
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _log.Debug("Constructing BlobPasswordValidator");
        }

        public int RequiredLength { get; set; }

        public bool RequireNonLetterOrDigit { get; set; }
        
        public bool RequireLowercase { get; set; }

        public bool RequireUppercase { get; set; }

        public bool RequireDigit { get; set; }
        
        public virtual Task<IdentityResult> ValidateAsync(string item)
        {
            _log.Debug("ValidateAsync");
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            List<string> errors = new List<string>();
            if (string.IsNullOrWhiteSpace(item) || item.Length < RequiredLength)
            {
                errors.Add(String.Format(CultureInfo.CurrentCulture, "Resources.PasswordTooShort", RequiredLength));
            }
            if (RequireNonLetterOrDigit && item.All(IsLetterOrDigit))
            {
                errors.Add("Resources.PasswordRequireNonLetterOrDigit");
            }
            if (RequireDigit && item.All(c => !IsDigit(c)))
            {
                errors.Add("Resources.PasswordRequireDigit");
            }
            if (RequireLowercase && item.All(c => !IsLower(c)))
            {
                errors.Add("Resources.PasswordRequireLower");
            }
            if (RequireUppercase && item.All(c => !IsUpper(c)))
            {
                errors.Add("Resources.PasswordRequireUpper");
            }
            if (errors.Count == 0)
            {
                return Task.FromResult(IdentityResult.Success);
            }
            return Task.FromResult(IdentityResult.Failed(String.Join(" ", errors)));
        }

        public virtual bool IsDigit(char c)
        {
            return c >= '0' && c <= '9';
        }

        public virtual bool IsLower(char c)
        {
            return c >= 'a' && c <= 'z';
        }

        public virtual bool IsUpper(char c)
        {
            return c >= 'A' && c <= 'Z';
        }

        public virtual bool IsLetterOrDigit(char c)
        {
            return IsUpper(c) || IsLower(c) || IsDigit(c);
        }
    }
}
