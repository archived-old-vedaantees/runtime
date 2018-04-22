using System.Linq;
using Vedaantees.Framework.Providers.Communications.ServiceBus;
using Vedaantees.Framework.Shell.UserContexts;
using Vedaantees.Framework.Types.Results;
using Vedaantees.Hosts.SingleSignOn.Stores.Models;
using IdentityModel;
using Vedaantees.Framework.Types.Users;
using Vedaantees.Hosts.SingleSignOn.Presentation.Models;

namespace Vedaantees.Hosts.SingleSignOn.Domain.Queries
{
    public class QueryService : IQuery<GetMembershipByUsername, Membership>,
                                IQuery<GetMembershipByEmail, Membership>,
                                IQuery<HasMembershipByEmail, bool>,
                                IQuery<HasMembershipByUsername,bool>
    {
        private readonly IUserContextService _userContextService;

        public QueryService(IUserContextService userContextService)
        {
            _userContextService = userContextService;
        }


        public Membership Handle(GetMembershipByUsername request)
        {
            var response = _userContextService.GetContextByUsername<Membership>(request.Username);

            if (response.MethodResultState != MethodResultStates.Successful)
                throw new MemberNotFoundException($"No membership details found for user: {request.Username}");
            
            return response.Result;
        }

        public bool Handle(HasMembershipByEmail request)
        {
            var response = _userContextService.QueryContextsByClaim<Membership>(new UserClaim(JwtClaimTypes.Email, request.Email));
            return response.MethodResultState != MethodResultStates.Successful && response.Result.Any();
        }

        public bool Handle(HasMembershipByUsername request)
        {
            var response = _userContextService.GetContextByUsername<Membership>(request.Username);

            return response.MethodResultState == MethodResultStates.Successful;
        }

        public Membership Handle(GetMembershipByEmail request)
        {
            var response = _userContextService.QueryContextsByClaim<Membership>(new UserClaim(JwtClaimTypes.Email, request.Email));
            return response.Result.FirstOrDefault();
        }
    }

    public class HasMembershipByEmail : QueryRequest<bool>
    {
        public string Email { get; set; }   
    }

    public class GetMembershipByUsername : QueryRequest<Membership>
    {
        public string Username { get; set; }
    }

    public class GetMembershipByEmail : QueryRequest<Membership>
    {
        public string Email { get; set; }
    }

    public class HasMembershipByUsername : QueryRequest<bool>
    {
        public string Username { get; set; }
    }
}
