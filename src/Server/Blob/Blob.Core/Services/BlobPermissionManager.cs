//using System;
//using System.Threading.Tasks;
//using Blob.Contracts.Models;
//using Blob.Contracts.Services;
//using log4net;

//namespace Blob.Core.Services
//{
//    public class BlobPermissionManager : IPermissionService
//    {
//        private readonly ILog _log;
//        private BlobDbContext _context;

//        public BlobPermissionManager(ILog log, BlobDbContext context)
//        {
//            _log = log;
//            _log.Debug("Constructing BlobPermissionManager");
//            _context = context;
//        }


//        public Task<bool> CheckAccessAsync(AuthorizationContextDto context)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}