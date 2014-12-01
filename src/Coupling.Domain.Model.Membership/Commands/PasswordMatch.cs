
namespace Coupling.Domain.Model.Membership.Commands
{
    public class PasswordMatch : AccountCommand
    {
        public bool Matched { get; private set; }
        public Account Account { get; private set; }

        public PasswordMatch(Account account, bool match) : base(account.Id)
        {
            Matched = match;
            Account = account;
        }
    }
}
