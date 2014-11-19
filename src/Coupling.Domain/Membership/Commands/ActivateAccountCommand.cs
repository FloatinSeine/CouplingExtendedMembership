
namespace Coupling.Domain.Membership.Commands
{
    public class ActivateAccountCommand : AccountCommand
    {
        public string Token { get; private set; }

        public ActivateAccountCommand(string id, string token) : base(id)
        {
            Token = token;
        }
    }
}
