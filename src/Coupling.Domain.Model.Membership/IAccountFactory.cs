
namespace Coupling.Domain.Model.Membership
{
    public interface IAccountFactory
    {
        Account Create(string username, string salt, string hashPassword, string activationToken);
    }
}
