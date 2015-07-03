namespace Blob.Contracts.ServiceContracts
{
    using System.Threading.Tasks;

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