
namespace Coupling.Domain.Model.Membership.Commands
{
    public class AddRolesToUserCommand : AccountCommand
    {
        public string[] Roles { get; private set; }


        public AddRolesToUserCommand(string id, string[] roles) : base(id)
        {
            Roles = roles;
        }
    }
}
