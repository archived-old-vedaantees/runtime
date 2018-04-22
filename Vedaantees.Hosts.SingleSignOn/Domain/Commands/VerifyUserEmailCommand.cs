using System;
using Vedaantees.Framework.Providers.Communications.ServiceBus;

namespace Vedaantees.Hosts.SingleSignOn.Domain.Commands
{
    public class VerifyUserEmailCommand : Command
    {
        public string ConfirmationToken { get; set; }
        public string EmailId { get; set; }
        public string Username { get; set; }
        public string Id { get; set; }

    }
}