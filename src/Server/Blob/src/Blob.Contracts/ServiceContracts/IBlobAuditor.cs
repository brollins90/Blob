using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blob.Contracts.ServiceContracts
{
    public enum AuditLevel
    {
        Create,
        Delete,
        View,
        Edit,
        IssueCommand
    }

    public interface IBlobAuditor
    {
        Task AddAuditEntryAsync(string initiator, AuditLevel level, string operation, string resource, string resourceId);
    }
}
