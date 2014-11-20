using System.Collections.Generic;
using Coupling.Domain.DDD;

namespace Coupling.Domain.Tests.DDD.Fakes
{
    internal class FakeValueObject : ValueObject
    {
        private readonly List<object> _fakeValues;

        public FakeValueObject(params object[] fakeValues)
        {
            _fakeValues = new List<object>(fakeValues);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            return _fakeValues;
        }

        public static bool operator ==(FakeValueObject left, FakeValueObject right)
        {
            return EqualOperator(left, right);
        }

        public static bool operator !=(FakeValueObject left, FakeValueObject right)
        {
            return NotEqualOperator(left, right);
        }
    }
}
