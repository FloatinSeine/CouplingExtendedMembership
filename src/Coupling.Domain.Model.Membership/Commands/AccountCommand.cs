
using Coupling.Domain.CQRS.Command;

namespace Coupling.Domain.Model.Membership.Commands
{
    public abstract class AccountCommand : ICommand
    {
        public string Id { get; private set; }

        protected AccountCommand(string accountId)
        {
            Id = accountId;
        }
    }
}
