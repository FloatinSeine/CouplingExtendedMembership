using System.Collections.Generic;
using Coupling.Web.ApplicationServices.Roles;

namespace Coupling.Web.ApplicationServices
{
    public interface IRoleFactory
    {
        IList<Role> GetRoles();
        Role CreateRole(string type);
        bool IsValidRole(string type);
    }
}
