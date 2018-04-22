using System;
using Vedaantees.Framework.Providers.Communications.ServiceBus;
using Vedaantees.Framework.Providers.Storages.Keys;

namespace Vedaantees.Hosts.SingleSignOn.Domain.Commands
{
    public class RegisterUserFromExternalProviderCommand : Command
    {
        [GenerateKey("Users")]
        public string Id { get; set; }
        
        public string Username { get; set; }
        public string Password { get; set; }
        public string EmailId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}