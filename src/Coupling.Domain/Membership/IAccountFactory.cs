
namespace Coupling.Domain.Membership
{
    public interface IAccountFactory
    {
        Account Create(string username, string salt, string hashPassword, string activationToken);
    }
}
