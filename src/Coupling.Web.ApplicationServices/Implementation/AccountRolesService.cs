using System;
using System.Linq;
using Coupling.Domain.CQRS.Command;
using Coupling.Domain.Model.Membership;
using Coupling.Domain.Model.Membership.Commands;

namespace Coupling.Web.ApplicationServices.Implementation
{
    public class AccountRolesService : IAccountRolesService
    {
        private readonly IBus _bus;
        private readonly IRoleFactory _factory;
        private readonly IFindAccountQuery _query;

        public AccountRolesService(IBus bus, IRoleFactory factory, IFindAccountQuery query)
        {
            _bus = bus;
            _factory = factory;
            _query = query;
        }

        public string[] GetAllRoles()
        {
            return _factory.GetRoles()
                        .Select(x => x.Name)
                        .ToArray();
        }

        public bool RoleExists(string roleName)
        {
            return _factory.IsValidRole(roleName);
        }

        public string[] GetRolesForUser(string username)
        {
            var acc = _query.FindByUserName(username);
            return acc.Roles.ToArray();
        }

        public bool IsUserInRole(string username, string roleName)
        {
            var acc = _query.FindByUserName(username);
            return acc!= null && acc.Roles.Contains(roleName);
        }

        public void AddRolesToUser(string username, string[] roleNames)
        {
            var acc = _query.FindByUserName(username);
            if (acc != null)
            {
                _bus.Send(new AddRolesToUserCommand(acc.Id, roleNames));
            }
        }

        public void RemoveRolesFromUser(string username, string[] roleNames)
        {
            var acc = _query.FindByUserName(username);
            if (acc != null)
            {
                _bus.Send(new RemoveRolesFromUserCommand(acc.Id, roleNames));
            }
        }
    }
}
