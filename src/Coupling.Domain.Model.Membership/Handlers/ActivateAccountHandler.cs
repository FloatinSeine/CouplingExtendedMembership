using Coupling.Domain.CQRS.Command;
using Coupling.Domain.Model.Membership.Commands;

namespace Coupling.Domain.Model.Membership.Handlers
{
    public class ActivateAccountHandler : ICommand<ActivateAccountCommand>
    {
        private readonly IAccountRepository _repository;

        public ActivateAccountHandler(IAccountRepository repository)
        {
            _repository = repository;
        }

        public void Execute(ActivateAccountCommand command)
        {
            var acc = _repository.Get(command.Id);
            acc.Activate(command.Token);
            _repository.CommitChanges();
        }
    }
}
