using System;
using System.Collections.Generic;
using System.Linq;
using Vedaantees.Framework.Shell.UserContexts;
using Vedaantees.Framework.Types.Users;

namespace Vedaantees.Hosts.SingleSignOn.Stores.Models
{
    public class Membership : IUserContext
    {
        public Membership(List<UserClaim> claims)
        {
            Claims = claims;
        }

        public string ContextId  { get; set; }
        public List<UserClaim> Claims { get; set; }
        public string Password { get; set; }
        public bool IsLocked   { get; set; }
        public int AccessFailCount { get; set; }
        public string PasswordSalt { get; set; }
        public string Email { get; set; }
        public string ConfirmationToken { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public DateTime MemberSinceDate { get; set; }
        public DateTime LastLoginDate { get; set; }

        public string GetClaim(string type)
        {
            return Claims.FirstOrDefault(p => p.Type == type)?.Value;
        }
    }
}
