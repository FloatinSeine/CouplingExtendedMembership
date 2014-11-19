using Coupling.Domain.CQRS.Query;

namespace Coupling.Domain.Membership
{
    public interface IFindAccountQuery : IQuery<Account>
    {
        Account FindById(string id);
        Account FindByUserName(string username);
        Account FindByConfirmationToken(string confirmationToken);
        bool IsValidCredentials(string username, string passwordHash);
        string GetUserIdFromPasswordResetToken(string token);
    }
}
