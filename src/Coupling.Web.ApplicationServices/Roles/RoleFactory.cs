using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Coupling.Web.ApplicationServices.Roles
{
    public class RoleFactory : IRoleFactory
    {
        public IList<Role> GetRoles()
        {
            var assembly = Assembly.GetAssembly(GetType());
            var types = GetRegisteredRoleTypes();

            return types.Select(t => assembly.CreateInstance(t.FullName, true) as Role).ToList();
        }

        public Role CreateRole(string type)
        {
            return type.StartsWith("Administrator", StringComparison.InvariantCultureIgnoreCase) ? (Role) new AdministratorRole() : new GuestRole();
        }

        public bool IsValidRole(string type)
        {
            return GetRegisteredRoleTypes().Any(x=>x.Name.Equals(type, StringComparison.InvariantCultureIgnoreCase));
        }

        private IEnumerable<Type> GetRegisteredRoleTypes()
        {
            var assembly = Assembly.GetAssembly(GetType());
            return assembly.GetTypes().Where(t => t.IsClass && t.BaseType != null && t.BaseType.Name == "Role");
        }
    }
}
