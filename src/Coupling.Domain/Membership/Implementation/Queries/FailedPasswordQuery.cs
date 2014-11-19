using Coupling.Domain.Membership.Dtos;

namespace Coupling.Domain.Membership.Implementation.Queries
{
    public class FailedPasswordQuery : IFailedPasswordQuery
    {
        private readonly IAccountRepository _repository;

        public FailedPasswordQuery(IAccountRepository repository)
        {
            _repository = repository;
        }

        public FailedPasswordDetailsDto FailedPasswordDetails(string username)
        {
            var acc = _repository.GetByUsername(username);
            var dto = new FailedPasswordDetailsDto(acc.Id, 
                                                    acc.Membership.FailedPasswordMatchAttempts,
                                                    acc.Membership.LastPasswordFailureDate, 
                                                    acc.Membership.PasswordChangeDate);
            return dto;
        }
    }
}
