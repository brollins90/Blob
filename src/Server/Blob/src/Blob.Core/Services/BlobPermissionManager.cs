namespace Blob.Core.Services
{
    using System;
    using System.Threading.Tasks;
    using Common.Services;
    using Contracts.Models;
    using log4net;

    public class BlobPermissionManager : IPermissionService
    {
        private readonly ILog _log;
        private BlobDbContext _context;

        public BlobPermissionManager(ILog log, BlobDbContext context)
        {
            _log = log;
            _log.Debug("Constructing BlobPermissionManager");
            _context = context;
        }


        public Task<bool> CheckAccessAsync(AuthorizationContextDto context)
        {
            throw new NotImplementedException();
        }
    }
}