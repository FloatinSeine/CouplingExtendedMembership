
namespace Coupling.Domain.Model.Membership.Commands
{
    public class RemoveRolesFromUserCommand : AccountCommand
    {
        public string[] Roles { get; private set; }


        public RemoveRolesFromUserCommand(string id, string[] roles)
            : base(id)
        {
            Roles = roles;
        }
    }
}
