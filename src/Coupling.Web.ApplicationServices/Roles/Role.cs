
namespace Coupling.Web.ApplicationServices.Roles
{
    public class Role
    {
        public string Name { get { return GetType().Name; } }
        public virtual bool IsAdministrator { get { return false; }}
    }
}
