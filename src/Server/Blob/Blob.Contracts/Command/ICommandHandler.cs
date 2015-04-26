
namespace Blob.Contracts.Command
{
    public interface ICommandHandler<in TCmd>
        where TCmd : ICommand
    {
        void Handle(TCmd command);
    }
}
