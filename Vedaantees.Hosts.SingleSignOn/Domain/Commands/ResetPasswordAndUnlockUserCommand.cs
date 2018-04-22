using System;
using Vedaantees.Framework.Providers.Communications.ServiceBus;

namespace Vedaantees.Hosts.SingleSignOn.Domain.Commands
{
    public class ResetPasswordAndUnlockUserCommand: Command
    {
		 public string Username { get; set; }
         public string EmailId { get; set; }
         public string NewPassword { get; set; }

        public string SiteAddress { get; set; }
    }
}