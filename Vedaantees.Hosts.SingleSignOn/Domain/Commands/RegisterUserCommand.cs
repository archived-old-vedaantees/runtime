using Vedaantees.Framework.Providers.Communications.ServiceBus;
using Vedaantees.Framework.Providers.Storages.Keys;

namespace Vedaantees.Hosts.SingleSignOn.Domain.Commands
{
    public class RegisterUserCommand : Command
    {
        [GenerateKey("Users")]
        public string Id { get; set; }

        [GenerateKey("Memberships")]
        public string MembershipId { get; set; }
        public string ConfirmationToken { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string EmailId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public string SiteAddress { get; set; }
    }
}