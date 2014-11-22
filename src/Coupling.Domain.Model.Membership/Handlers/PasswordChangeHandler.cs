
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
            var acc = _repository.Get(command.Id);
            acc.ChangePassword(command.Salt, command.Password);
            _repository.CommitChanges();
        }

        public void Execute(PasswordMatch command)
        {
            var acc = _repository.Get(command.Id);
            if (command.Matched) acc.Membership.ResetPasswordMatches();
            else acc.Membership.FailedPasswordMatch();
            _repository.CommitChanges();
        }
    }
}
