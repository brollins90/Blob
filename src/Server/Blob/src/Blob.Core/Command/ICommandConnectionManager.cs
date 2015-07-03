namespace Blob.Core.Command
{
    using System;
    using System.Collections.Generic;
    using Contracts.ServiceContracts;

    public interface ICommandConnectionManager : IDisposable
    {
        void AddCallback(Guid deviceId, IDeviceConnectionServiceCallback callback);
        IEnumerable<Guid> GetActiveDeviceIds();
        IDeviceConnectionServiceCallback GetCallback(Guid deviceId);
        bool HasCallback(Guid deviceId);
        void RemoveCallback(Guid deviceId);
    }
}