using Coupling.Domain.CQRS.Command;
using Coupling.Domain.Model.Membership.Commands;
using Coupling.Domain.Model.Membership.Handlers;
using Coupling.Domain.Model.Membership.Implementation;
using Coupling.Domain.Model.Membership.Implementation.Queries;
using StructureMap.Configuration.DSL;

namespace Coupling.Domain.Model.Membership.DepenencyResolution
{
    public class MembershipRegistry : Registry
    {
        public MembershipRegistry()
        {
            ForSingletonOf<IAccountFactory>().Use<AccountFactory>();
            For<IFindAccountQuery>().Use<AccountFinder>();
            For<IFailedPasswordQuery>().Use<FailedPasswordQuery>();

            For<ICommand<ChangePasswordCommand>>().Use<PasswordChangeHandler>();
            For<ICommand<PasswordMatch>>().Use<PasswordChangeHandler>();
            For<ICommand<ActivateAccountCommand>>().Use<ActivateAccountHandler>();
            For<ICommand<AppendOAuthAccountCommand>>().Use<AccountOAuthHandler>();
            For<ICommand<AddRolesToUserCommand>>().Use<AccountRolesHandler>();
            For<ICommand<RemoveRolesFromUserCommand>>().Use<AccountRolesHandler>();
        }
    }
}
