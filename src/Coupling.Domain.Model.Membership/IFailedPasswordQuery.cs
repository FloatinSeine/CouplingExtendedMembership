
using Coupling.Domain.Model.Membership.Dtos;

namespace Coupling.Domain.Model.Membership
{
    public interface IFailedPasswordQuery
    {
        FailedPasswordDetailsDto FailedPasswordDetails(string username);
    }
}
