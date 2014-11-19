
namespace Coupling.Domain.CQRS.Query
{
    public interface IQuery
    {
    }

    public interface IQuery<TEntity> : IQuery where TEntity : class
    {
        
    }
}
