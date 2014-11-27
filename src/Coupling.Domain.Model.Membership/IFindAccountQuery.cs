using Coupling.Domain.CQRS.Query;

namespace Coupling.Domain.Model.Membership
{
    public interface IFindAccountQuery : IQuery<Account>
    {
        Account FindById(string id);
        Account FindByUserName(string username);
        Account FindByConfirmationToken(string confirmationToken);
        Account FindByOAuthProvider(string provider, string providerUserId);
        Account FindByUserId(int userId);
        string GetUserIdFromPasswordResetToken(string token);
    }
}
