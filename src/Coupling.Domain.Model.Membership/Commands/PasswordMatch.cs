
namespace Coupling.Domain.Model.Membership.Commands
{
    public class PasswordMatch : AccountCommand
    {
        public bool Matched { get; private set; }

        public PasswordMatch(string id, bool match) : base(id)
        {
            Matched = match;
        }
    }
}
