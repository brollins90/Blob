﻿using System;
using System.Threading.Tasks;
using Blob.Contracts.ServiceContracts;
using Blob.Core.Models;
using log4net;

namespace Blob.Core.Audit
{
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
