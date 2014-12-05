using System;
using Coupling.Domain.CQRS.Command;
using Coupling.Domain.Model.Membership.Commands;

namespace Coupling.Domain.Model.Membership.Handlers
{
    public class AccountRolesHandler : ICommand<AddRolesToUserCommand>, ICommand<RemoveRolesFromUserCommand>, IDisposable
    {
        private IAccountRepository _repository;

        public AccountRolesHandler(IAccountRepository repository)
        {
            _repository = repository;
        }

        public void Execute(AddRolesToUserCommand command)
        {
            _repository.AppendRoles(command.Id, command.Roles);
        }

        public void Execute(RemoveRolesFromUserCommand command)
        {
            _repository.RemoveRoles(command.Id, command.Roles);
        }

        public void Dispose()
        {
            _repository.Dispose();
            _repository = null;
        }
    }
}
