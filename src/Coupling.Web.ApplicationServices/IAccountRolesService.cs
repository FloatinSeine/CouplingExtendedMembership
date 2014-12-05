
namespace Coupling.Web.ApplicationServices
{
    public interface IAccountRolesService
    {
        void AddRolesToUser(string username, string[] roleNames);
        string[] GetAllRoles();
        string[] GetRolesForUser(string username);
        bool IsUserInRole(string username, string roleName);
        bool RoleExists(string roleName);
        void RemoveRolesFromUser(string username, string[] roleNames);
    }
}
