using System;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Linq;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Security;
using Coupling.Web.ApplicationServices.Extensions;

namespace Coupling.Web.ApplicationServices.Memberships
{
    public class CouplingRoleProvider : RoleProvider
    {

        private static IAccountRolesService RoleService
        {
            get { return DependencyResolver.Current.GetService<IAccountRolesService>(); }
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            if (config == null) throw new ArgumentNullException("config");

            if (string.IsNullOrEmpty(name))
            {
                name = GetType().Name;
            }
            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Coupling Role Provider");
            }

            base.Initialize(name, config);

            ApplicationName = config.GetValue("applicationName", HostingEnvironment.ApplicationVirtualPath);
        }

        public override string ApplicationName { get; set; }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            if (roleNames.Any(rolename => !RoleExists(rolename)))
            {
                throw new ProviderException("Role name not found.");
            }

            foreach (var u in usernames)
            {
                RoleService.AddRolesToUser(u, roleNames);
            }

        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            return RoleService.GetAllRoles();
        }

        public override string[] GetRolesForUser(string username)
        {
            ValidateArgument("username", username);

            return RoleService.GetRolesForUser(username);
        }

        public override string[] GetUsersInRole(string roleName)
        {
            if (!RoleExists(roleName))
            {
                throw new ProviderException("Role name not found.");
            }

            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            ValidateArgument("username", username);
            return !string.IsNullOrEmpty(roleName) && RoleService.IsUserInRole(username, roleName);
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            if (roleNames.Any(rolename => !RoleExists(rolename)))
            {
                throw new ProviderException("Role name not found.");
            }

            foreach (var u in usernames)
            {
                RoleService.RemoveRolesFromUser(u, roleNames);
            }
        }

        public override bool RoleExists(string roleName)
        {
            return RoleService.RoleExists(roleName);
        }

        private static void ValidateArgument(string argumentName, string value)
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentException(string.Format("Invalid Argument {0}", argumentName), argumentName);
        }

    }
}
