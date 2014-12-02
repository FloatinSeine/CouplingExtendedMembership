using System;
using System.Collections.Generic;
using System.Text;
using Coupling.Domain.DDD;
using Coupling.Domain.Tests.DDD.Fakes;
using NUnit.Framework;

namespace Coupling.Domain.Tests.DDD
{
    [TestFixture]
    public class EntityTests
    {
        [Test]
        public void NewEntity_Id_ReturnsStringStartingWithEntityClassName()
        {
            var entity = new FakeEntity();
            var className = "FakeEntity";

            Assert.IsTrue(entity.Id.StartsWith(className));
        }

        [Test]
        public void NewEntity_Id_ReturnsStringEndingWithSlashGuid()
        {
            var entity = new FakeEntity();
            var parts = entity.Id.Split('/');
            var guid = new Guid();

            Assert.IsTrue(Guid.TryParse(parts[1], out guid));
        }

        [Test]
        public void NewEntity_HasActiveStatus()
        {
            var entity = new FakeEntity();

            Assert.IsTrue(entity.Status == EntityStatus.Active);
        }

        [Test]
        public void Entity_WhenMarkedAsRemoved_HasStatusArchived()
        {
            var entity = new FakeEntity();
            entity.MarkAsRemoved();

            Assert.IsTrue(entity.Status == EntityStatus.Archived);
        }
    }
}
