
namespace Coupling.Domain.Model.Membership.Commands
{
    public class AppendOAuthAccountCommand : AccountCommand
    {
        public string Provider { get; private set; }
        public string ProviderUserId { get; private set; }

        public AppendOAuthAccountCommand(string Id, string provider, string providerId) : base(Id)
        {
            Provider = provider;
            ProviderUserId = providerId;
        }
    }
}
