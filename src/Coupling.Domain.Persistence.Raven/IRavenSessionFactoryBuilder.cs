
namespace Coupling.Domain.Persistence.Raven
{
    public interface IRavenSessionFactoryBuilder
    {
        IRavenSessionFactory GetSessionFactory();
    }
}
