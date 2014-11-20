
using Coupling.Domain.CQRS.Command;
using Coupling.Domain.Model.Membership.Commands;

namespace Coupling.Domain.Model.Membership.Handlers
{
    public class PasswordChangeHandler : ICommand<ChangePasswordCommand>, ICommand<FailedPasswordMatch>
    {
        private readonly IAccountRepository _repository;

        public PasswordChangeHandler(IAccountRepository repository)
        {
            _repository = repository;
        }

        public void Execute(ChangePasswordCommand command)
        {
            var acc = _repository.Get(command.Id);
            acc.ResetPassword(command.Salt, command.Password);
            _repository.CommitChanges();
        }

        public void Execute(FailedPasswordMatch command)
        {
            var acc = _repository.Get(command.Id);
            acc.Membership.FailedPasswordMatch();
        }
    }
}
