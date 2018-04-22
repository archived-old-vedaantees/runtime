using Vedaantees.Framework.Providers.Communications.ServiceBus;
using Vedaantees.Framework.Shell.Rules;
using Vedaantees.Hosts.SingleSignOn.Domain.Commands;
using Vedaantees.Hosts.SingleSignOn.Domain.Queries;

namespace Vedaantees.Hosts.SingleSignOn.Domain.Rules
{
    public class EmailShouldBeRegisteredOnce : IRule<RegisterUserCommand>
    {
        private readonly IQueryService _queryService;

        public EmailShouldBeRegisteredOnce(IQueryService queryService)
        {
            _queryService = queryService;
        }

        public RuleResult Validate(RegisterUserCommand command)
        {
            var membership = _queryService.ExecuteQuery<HasMembershipByEmail,bool>(new HasMembershipByEmail{ Email = command.EmailId }).Result;

            return membership ? new RuleResult
                                {
                                    Message = GetRuleDefinition().ErrorMessage,
                                    IsSuccessful = false,
                                    ErrorLevel = GetRuleDefinition().SeverityLevel
                                } :
                                new RuleResult
                                {
                                    IsSuccessful = true
                                };
        }

        public RuleDefinition GetRuleDefinition()
        {
            return new RuleDefinition
            {
                ErrorMessage = "Email is already in use. Please user a different email id",
                Name = "SingleSignOn.Domain.Rules.EmailShouldBeRegisteredOnce",
                SeverityLevel = RuleMessageType.Error
            };
        }
    }
}
