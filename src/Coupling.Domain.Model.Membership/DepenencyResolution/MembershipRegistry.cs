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
            ForSingletonOf<IFindAccountQuery>().Use<AccountFinder>();
            ForSingletonOf<IFailedPasswordQuery>().Use<FailedPasswordQuery>();

            ForSingletonOf<ICommand<ChangePasswordCommand>>().Use<PasswordChangeHandler>();
            ForSingletonOf<ICommand<FailedPasswordMatch>>().Use<PasswordChangeHandler>();
            ForSingletonOf<ICommand<ActivateAccountCommand>>().Use<ActivateAccountHandler>();
        }
    }
}
