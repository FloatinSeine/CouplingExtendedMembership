using System;

namespace Coupling.Domain.DDD
{
    [Serializable]
    public abstract class AggregateRoot : Entity
    {
        [NonSerialized]
        private IDomainEventPublisher _eventPublisher;

        protected AggregateRoot()
        {
            UpdateVersion();
        }

        public DateTime Version { get; private set; }

        protected void UpdateVersion()
        {
            Version = DateTime.UtcNow;
        }

        protected internal virtual IDomainEventPublisher EventPublisher
        {
            get { return _eventPublisher; }
            set
            {
                if (_eventPublisher != null)
                    throw new InvalidOperationException("Publisher is already set. Probably You have logical error in code");
                _eventPublisher = value;
            }
        }
    }
}
