using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Blob.Contracts.Commands;
using Blob.Contracts.Models;
using Blob.Contracts.Models.ViewModels;
using Blob.Contracts.ServiceContracts;
using Blob.Contracts.Services;
using Blob.Core.Command;
using Blob.Core.Models;
using log4net;

namespace Blob.Core.Services
{
    public class BlobDeviceCommandManager : IDeviceCommandService
    {
        private readonly ILog _log;
        private readonly BlobDbContext _context;
        private readonly ICommandConnectionManager _connectionManager;
        private readonly ICommandQueueManager _queueManager;

        public BlobDeviceCommandManager(ILog log, BlobDbContext context)
        {
            _log = log;
            _log.Debug("Constructing BlobDeviceCommandManager");
            _context = context;
            // todo: inject these
            _connectionManager = CommandConnectionManager.Instance;
            _queueManager = CommandQueueManager.Instance;
        }


        public IEnumerable<DeviceCommandVm> GetDeviceCommandVmList()
        {
            _log.Debug(string.Format("GetDeviceCommandVmList()"));
            IList<Type> commandTypes = KnownCommandsMap.GetKnownCommandTypes();
            return commandTypes.Select(t => new DeviceCommandVm
            {
                CommandType = t.FullName,
                ShortName = t.Name,
                CommandParamters = t.GetProperties()//BindingFlags.Public & BindingFlags.Instance)
                .Select(p => new DeviceCommandParameterPairVm
                {
                    Key = p.Name,
                    Value = ""
                })
            });
        }

        public DeviceCommandIssueVm GetDeviceCommandIssueVm(Guid deviceId, string commandType)
        {
            _log.Debug(string.Format("GetDeviceCommandIssueVm({0}, {1})", deviceId, commandType));
            var commandTypes = GetDeviceCommandVmList();
            var command = commandTypes.Single(type => type.CommandType.Equals(commandType));

            DeviceCommandIssueVm result = new DeviceCommandIssueVm
            {
                DeviceId = deviceId,
                CommandType = command.CommandType,
                CommandParameters = command.CommandParamters.ToList(),
                ShortName = command.ShortName
            };
            return result;
        }

        public IEnumerable<Guid> GetActiveDeviceIds()
        {
            _log.Debug(string.Format("GetActiveDeviceIds()"));
            return _connectionManager.GetActiveDeviceIds();
        }

        public async Task<BlobResult> IssueCommandAsync(IssueDeviceCommandDto dto)
        {
            _log.Debug(string.Format("IssueCommandAsync({0})", dto.DeviceId));
            string assemblyName = KnownCommandsMap.GetCommandHandlerInterfaceAssembly().FullName;
            Type commandType = Type.GetType(dto.Command + ", " + assemblyName);
            var cmdInstance = Activator.CreateInstance(commandType);
            PropertyInfo[] properties = commandType.GetProperties();

            foreach (var property in properties)
            {
                if (dto.CommandParameters.ContainsKey(property.Name))
                {
                    property.SetValue(cmdInstance, dto.CommandParameters[property.Name], null);
                }
            }

            // todo: add to issue table

            Guid commandId = Guid.NewGuid();

            bool queued = await _queueManager.QueueCommandAsync(dto.DeviceId, commandId, (cmdInstance as IDeviceCommand)).ConfigureAwait(false);
            if (!queued)
            {
                // todo: remove from table, or mark as not queued
            }
            return BlobResult.Success;
        }
    }
}
