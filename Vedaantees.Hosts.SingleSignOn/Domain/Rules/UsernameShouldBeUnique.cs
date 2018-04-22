using Vedaantees.Framework.Providers.Communications.ServiceBus;
using Vedaantees.Framework.Shell.Rules;
using Vedaantees.Hosts.SingleSignOn.Domain.Commands;
using Vedaantees.Hosts.SingleSignOn.Domain.Queries;

namespace Vedaantees.Hosts.SingleSignOn.Domain.Rules
{
    public class UsernameShouldBeUnique : IRule<RegisterUserCommand>
    {
        private readonly IQueryService _queryService;

        public UsernameShouldBeUnique(IQueryService queryService)
        {
            _queryService = queryService;
        }

        public RuleResult Validate(RegisterUserCommand command)
        {
            var membership = _queryService.ExecuteQuery<HasMembershipByUsername, bool>(new HasMembershipByUsername{ Username = command.Username }).Result;

            return membership ? new RuleResult
                                {
                                    Message = GetRuleDefinition().ErrorMessage,
                                    IsSuccessful = false,
                                    ErrorLevel = GetRuleDefinition().SeverityLevel
                                } 
                              : new RuleResult
                                {
                                    IsSuccessful = true
                                };
        }

        public RuleDefinition GetRuleDefinition()
        {
            return new RuleDefinition
            {
                ErrorMessage = "Username is already in use. Please select a different username",
                Name = "SingleSignOn.Domain.Rules.UsernameShouldBeUnique",
                SeverityLevel = RuleMessageType.Error
            };
        }
    }
}
