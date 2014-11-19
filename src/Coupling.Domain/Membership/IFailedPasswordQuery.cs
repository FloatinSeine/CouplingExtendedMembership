
using Coupling.Domain.Membership.Dtos;

namespace Coupling.Domain.Membership
{
    public interface IFailedPasswordQuery
    {
        FailedPasswordDetailsDto FailedPasswordDetails(string username);
    }
}
