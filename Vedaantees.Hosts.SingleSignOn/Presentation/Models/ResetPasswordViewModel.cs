using System;

namespace Vedaantees.Hosts.SingleSignOn.Presentation.Models
{
    public class ResetPasswordViewModel
    {
        public string Username { get; set; }
        public string LoginUrl { get; set; }
    }

    public class MemberNotFoundException : Exception
    {
        public MemberNotFoundException(string v):base(v)
        {
            
        }
    }
}
