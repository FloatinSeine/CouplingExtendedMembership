
namespace Coupling.Domain.CQRS.Command
{
    public interface ICommand
    {
    }

    public interface ICommand<T> : ICommand
    {
        void Execute(T command);
    }
}
