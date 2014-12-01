
using System;
using Coupling.Domain.CQRS.Command;
using Coupling.Domain.Model.Membership.Commands;

namespace Coupling.Domain.Model.Membership.Handlers
{
    public class PasswordChangeHandler : ICommand<ChangePasswordCommand>, ICommand<PasswordMatch>
    {
        private readonly IAccountRepository _repository;

        public PasswordChangeHandler(IAccountRepository repository)
        {
            _repository = repository;
        }

        public void Execute(ChangePasswordCommand command)
        {
            _repository.ChangePassword(command.Id, command.Salt, command.Password);
        }

        public void Execute(PasswordMatch command)
        {
            throw new NotImplementedException();
            //_repository.PasswordMatched(command.Account, command.Matched);
        }
    }
}
