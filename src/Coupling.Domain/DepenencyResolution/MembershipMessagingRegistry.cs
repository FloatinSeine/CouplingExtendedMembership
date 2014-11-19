using Coupling.Domain.CQRS.Command;
using Coupling.Domain.Membership.Commands;
using Coupling.Domain.Membership.Handlers;
using StructureMap.Configuration.DSL;

namespace Coupling.Domain.DepenencyResolution
{
    public class MembershipMessagingRegistry : Registry
    {
        public MembershipMessagingRegistry()
        {
            ForSingletonOf<ICommand<ChangePasswordCommand>>().Use<PasswordChangeHandler>();
            ForSingletonOf<ICommand<FailedPasswordMatch>>().Use<PasswordChangeHandler>();
            ForSingletonOf<ICommand<ActivateAccountCommand>>().Use<ActivateAccountHandler>();
        }
    }
}
