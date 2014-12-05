using System.Linq;
using Coupling.Web.ApplicationServices.Roles;
using NUnit.Framework;

namespace Coupling.Web.ApplicationServices.Tests.Roles
{
    [TestFixture]
    public class RoleFactoryTests
    {
        private RoleFactory _roleFactory;

        [TestFixtureSetUp]
        public void Setup()
        {
            _roleFactory = new RoleFactory();
        }

        [Test]
        public void Factory_GetRoles_ReturnsListContainingGuestRole()
        {
            var roles = _roleFactory.GetRoles();

            var role = roles.Single(x => x.Name == "GuestRole");

            Assert.IsInstanceOf<GuestRole>(role);
        }

        [Test]
        public void Factory_GetRoles_ReturnsListContainingAdministratorRole()
        {
            var roles = _roleFactory.GetRoles();

            var role = roles.Single(x => x.Name == "AdministratorRole");

            Assert.IsInstanceOf<AdministratorRole>(role);
        }

        [Test]
        public void Factory_CreateRoleTypeAdministrator_ReturnsAdministratorRole()
        {
            var role = _roleFactory.CreateRole("Administrator");

            Assert.IsInstanceOf<AdministratorRole>(role);
        }

        [Test]
        public void Factory_CreateRoleTypeGuest_ReturnsGuestRole()
        {
            var role = _roleFactory.CreateRole("Guest");

            Assert.IsInstanceOf<GuestRole>(role);
        }

        [Test]
        public void Factory_CreateRoleTypeNotRecognised_ReturnsGuestRole()
        {
            var role = _roleFactory.CreateRole("NotRecognised");

            Assert.IsInstanceOf<GuestRole>(role);
        }

        [TestCase("GuestRole")]
        [TestCase("guestrole")]
        [TestCase("AdministratorRole")]
        [TestCase("administratorRole")]
        public void Factory_ExistsRole_ReturnsTrue(string roleType)
        {
            Assert.IsTrue(_roleFactory.IsValidRole(roleType));
        }

        [TestCase("EmployeeRole")]
        [TestCase("Customer")]
        public void Factory_DoesNotExistRole_ReturnsFalse(string roleType)
        {
            Assert.IsFalse(_roleFactory.IsValidRole(roleType));
        }
    }
}
