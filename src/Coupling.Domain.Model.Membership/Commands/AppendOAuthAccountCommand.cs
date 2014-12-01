
namespace Coupling.Domain.Model.Membership.Commands
{
    public class AppendOAuthAccountCommand : AccountCommand
    {
        public string Provider { get; private set; }
        public string ProviderUserId { get; private set; }

        public AppendOAuthAccountCommand(string id, string provider, string providerId) : base(id)
        {
            Provider = provider;
            ProviderUserId = providerId;
        }
    }
}
