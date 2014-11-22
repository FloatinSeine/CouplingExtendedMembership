using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Coupling.Domain.DDD;

namespace Coupling.Domain.Model.Membership.Events
{
    public class PasswordChangedEvent : IDomainEvent
    {
        public string Id { get; private set; }

        public PasswordChangedEvent(string id)
        {
            Id = id;
        }
    }
}
