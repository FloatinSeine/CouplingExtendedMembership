
namespace Coupling.Domain.Model.Membership.Commands
{
    public class ChangePasswordCommand : AccountCommand
    {
        public string Salt { get; private set; }
        public string Password { get; private set; }

        public ChangePasswordCommand(string accountId, string salt, string password) : base(accountId)
        {
            Salt = salt;
            Password = password;
        }
    }
}
