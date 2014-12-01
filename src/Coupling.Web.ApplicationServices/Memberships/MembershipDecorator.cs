using System;
using System.Web.Security;
using Coupling.Domain.Model.Membership;

namespace Coupling.Web.ApplicationServices.Memberships
{
    internal class MembershipDecorator
    {
        public static MembershipUser Decorate(Account account)
        {

            var membership = new MembershipUser(Membership.Provider.Name, 
                                                account.Username, 
                                                account.UserId, 
                                                string.Empty, 
                                                string.Empty, 
                                                string.Empty,
                                                account.AccountStatus == AccountStatus.Activated, 
                                                false,
                                                account.Created, 
                                                DateTime.UtcNow, 
                                                account.Version,
                                                account.Membership.PasswordChangeDate, 
                                                DateTime.MaxValue);

            return membership;
        }
    }
}
