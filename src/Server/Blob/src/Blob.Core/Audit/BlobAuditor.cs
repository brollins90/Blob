namespace Blob.Core.Audit
{
    using System;
    using System.Threading.Tasks;
    using Contracts.ServiceContracts;
    using log4net;
    using Models;

    public class BlobAuditor : IBlobAuditor
    {
        private readonly ILog _log;

        public BlobAuditor(BlobDbContext context, ILog log)
        {
            _log = log;
            _log.Debug("Constructing BlobAuditor");
            Context = context;
        }

        protected BlobDbContext Context { get; private set; }

        public async Task AddAuditEntryAsync(string initiator, AuditLevel level, string operation, string resource, string resourceId)
        {
            _log.Debug("Adding audit entry");
            AuditRecord ae = new AuditRecord
            {
                AuditLevel = (int)level,
                Initiator = initiator,
                Operation = operation,
                Resource = resourceId,
                ResourceType = resource,
                RecordTimeUtc = DateTime.UtcNow
            };
            Context.AuditLog.Add(ae);
            await Context.SaveChangesAsync();
        }
    }
}