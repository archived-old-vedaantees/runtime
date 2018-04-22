using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Vedaantees.Framework.Providers.Logging;
using Vedaantees.Framework.Providers.Security;
using Vedaantees.Framework.Providers.Users;
using Vedaantees.Framework.Shell.UserContexts;
using Vedaantees.Hosts.SingleSignOn.Stores.Models;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Vedaantees.Framework.Types.Users;

namespace Vedaantees.Hosts.SingleSignOn.Stores
{
    public class UserStore : IResourceOwnerPasswordValidator, IProfileService
    {
        private readonly IUserContextService _contextService;
        private readonly ILogger _logger;
        private readonly ICryptographicService _cryptographicService;

        public UserStore(IUserContextService contextService, ILogger logger, ICryptographicService cryptographicService)
        {
            _contextService = contextService;
            _logger = logger;
            _cryptographicService = cryptographicService;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var membership = await Task.Run(() => _contextService.GetContext<Membership>(context.UserName).Result);
            context.Result = membership.Password == _cryptographicService.ComputeHash(context.Password) ? new GrantValidationResult(context.UserName,"password") 
                                                                                                        : new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid credentials.");
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var membership = await Task.Run(() => _contextService.GetContext<Membership>(context.Subject.GetUserIdentity().Username).Result);
            context.IssuedClaims = membership.Claims
                                             .Where(p => context.RequestedClaimTypes.Contains(p.Type))
                                             .Select(p=> new Claim(p.Type, p.Value))
                                             .ToList();

            if (context.RequestedClaimTypes.ToList().Count == 0)
            {
                context.IssuedClaims.Add(new Claim(JwtClaimTypes.Subject, membership.GetClaim(JwtClaimTypes.Subject)));
                context.IssuedClaims.Add(new Claim(JwtClaimTypes.Email, membership.GetClaim(JwtClaimTypes.Email)));
                context.IssuedClaims.Add(new Claim(JwtClaimTypes.GivenName, membership.GetClaim(JwtClaimTypes.GivenName)));
                context.IssuedClaims.Add(new Claim(JwtClaimTypes.FamilyName, membership.GetClaim(JwtClaimTypes.FamilyName)));
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var membership = await Task.Run(()=> _contextService.GetContext<Membership>(context.Subject.GetUserIdentity().Username).Result);
            context.IsActive = !membership.IsLocked;
        }

        public Membership FindByExternalProvider(string provider, string userId)
        {
            return _contextService.QueryContextsByClaim<Membership>(new UserClaim(provider, userId)).Result.FirstOrDefault();
        }
    }
}