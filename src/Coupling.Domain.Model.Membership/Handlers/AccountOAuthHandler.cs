
using Coupling.Domain.CQRS.Command;
using Coupling.Domain.Model.Membership.Commands;

namespace Coupling.Domain.Model.Membership.Handlers
{
    public class AccountOAuthHandler : ICommand<AppendOAuthAccountCommand>
    {
        private readonly IAccountRepository _repository;

        public AccountOAuthHandler(IAccountRepository repository)
        {
            _repository = repository;
        }

        public void Execute(AppendOAuthAccountCommand command)
        {
            var acc = _repository.Get(command.Id);
            acc.AppendOAuthMembership(new OAuthMembership(command.Provider, command.ProviderUserId));
            _repository.CommitChanges();
        }
    }
}
