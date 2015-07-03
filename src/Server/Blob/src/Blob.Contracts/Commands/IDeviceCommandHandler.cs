namespace Blob.Contracts.Commands
{
    public interface IDeviceCommandHandler<in TCmd>
        where TCmd : IDeviceCommand
    {
        void Handle(TCmd command);
    }
}