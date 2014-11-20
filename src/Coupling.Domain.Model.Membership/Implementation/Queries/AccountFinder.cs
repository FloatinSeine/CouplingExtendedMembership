using System;


namespace Coupling.Domain.Model.Membership.Implementation.Queries
{
    public class AccountFinder : IFindAccountQuery
    {
        private readonly IAccountRepository _repository;

        public AccountFinder(IAccountRepository repository)
        {
            _repository = repository;
        }

        public Account FindById(string id)
        {
            return _repository.Get(id);
        }

        public Account FindByUserName(string username)
        {
            return _repository.GetByUsername(username);
        }

        public Account FindByConfirmationToken(string confirmationToken)
        {
            return _repository.GetByConfirmationToken(confirmationToken);
        }

        public bool IsValidCredentials(string username, string passwordHash)
        {
            return _repository.AccountExists(username, passwordHash);
        }

        public string GetUserIdFromPasswordResetToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}
