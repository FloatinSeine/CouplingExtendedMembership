
namespace Coupling.Domain.CQRS.Command
{
    public interface IBus
    {
        void Send<T>(T command);
    }
}
