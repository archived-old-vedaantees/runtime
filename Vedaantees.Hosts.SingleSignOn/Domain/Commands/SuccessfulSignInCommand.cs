using System;
using Vedaantees.Framework.Providers.Communications.ServiceBus;

namespace Vedaantees.Hosts.SingleSignOn.Domain.Commands
{
    public class SuccessfulSignInCommand : Command
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}