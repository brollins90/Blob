namespace Blob.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Common.Services;
    using Contracts.Commands;
    using Contracts.Request;
    using Contracts.Response;
    using Contracts.ViewModel;
    using Contracts.ServiceContracts;
    using Command;
    using log4net;

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


        public IEnumerable<DeviceCommandViewModel> GetDeviceCommandVmList()
        {
            _log.Debug(string.Format("GetDeviceCommandVmList()"));
            IList<Type> commandTypes = KnownCommandsMap.GetKnownCommandTypes();
            return commandTypes.Select(t => new DeviceCommandViewModel
            {
                CommandType = t.FullName,
                ShortName = t.Name,
                CommandParamters = t.GetProperties()//BindingFlags.Public & BindingFlags.Instance)
                .Select(p => new DeviceCommandParameterPair
                {
                    Key = p.Name,
                    Value = ""
                })
            });
        }

        public DeviceCommandIssueViewModel GetDeviceCommandIssueVm(Guid deviceId, string commandType)
        {
            _log.Debug(string.Format("GetDeviceCommandIssueVm({0}, {1})", deviceId, commandType));
            var commandTypes = GetDeviceCommandVmList();
            var command = commandTypes.Single(type => type.CommandType.Equals(commandType));

            DeviceCommandIssueViewModel result = new DeviceCommandIssueViewModel
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

        public async Task<BlobResult> IssueCommandAsync(IssueDeviceCommandRequest dto)
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