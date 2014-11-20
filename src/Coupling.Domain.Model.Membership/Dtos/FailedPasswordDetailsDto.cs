using System;

namespace Coupling.Domain.Model.Membership.Dtos
{
    public class FailedPasswordDetailsDto
    {
        public FailedPasswordDetailsDto(string id, int attempts, DateTime lastFailure, DateTime changed)
        {
            Id = id;
            FailedMatchAttempts = attempts;
            LastFailureDate = lastFailure;
            ChangedDate = changed;
        }

        public string Id { get; private set; }
        public int FailedMatchAttempts { get; private set; }
        public DateTime LastFailureDate { get; private set; }
        public DateTime ChangedDate { get; private set; }
    }
}
