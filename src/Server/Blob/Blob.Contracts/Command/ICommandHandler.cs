
namespace Blob.Contracts.Command
{
    public interface ICommandHandler<TCommand>
    {
        void Handle(TCommand command);
    }
}
