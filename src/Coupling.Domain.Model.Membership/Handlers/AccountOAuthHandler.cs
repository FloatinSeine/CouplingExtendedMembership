
using System;
using Coupling.Domain.CQRS.Command;
using Coupling.Domain.Model.Membership.Commands;

namespace Coupling.Domain.Model.Membership.Handlers
{
    public class AccountOAuthHandler : ICommand<AppendOAuthAccountCommand>, IDisposable
    {
        private IAccountRepository _repository;

        public AccountOAuthHandler(IAccountRepository repository)
        {
            _repository = repository;
        }

        public void Execute(AppendOAuthAccountCommand command)
        {
            _repository.AppendOAuthAccount(command.Id, command.Provider, command.ProviderUserId);
        }

        public void Dispose()
        {
            _repository.Dispose();
            _repository = null;
        }
    }
}
