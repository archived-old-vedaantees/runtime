using System.Threading.Tasks;
using Vedaantees.Framework.Providers.Communications.ServiceBus;
using Vedaantees.Framework.Providers.Mailing;
using Vedaantees.Framework.Providers.Mailing.Models;
using Vedaantees.Hosts.SingleSignOn.Domain.Commands;

namespace Vedaantees.Hosts.SingleSignOn.Domain.Handlers
{
    public class EmailHandler : ICommandHandler<RegisterUserCommand>,
                                ICommandHandler<ResetPasswordAndUnlockUserCommand>
    {
        private readonly ITemplateBuilder _templateBuilder;
        private readonly IEmailProvider _emailProvider;

        public EmailHandler(IEmailProvider emailProvider, ITemplateBuilder templateBuilder)
        {
            _templateBuilder = templateBuilder;
            _emailProvider = emailProvider;
        }

        public Task Handle(RegisterUserCommand command)
        {
            var emailMessage = new EmailMessage
            {
                Content = _templateBuilder.Build(Template.Get(), new TemplateModel
                {
                    Title = "Email Confirmation",
                    Content = "Please confirm your email by clicking like below.",
                    Link = $"{command.SiteAddress}/Account/ConfirmEmail?emailId={command.EmailId}&token={command.ConfirmationToken}",
                    LinkName = "Confirm"
                })
            };

            emailMessage.To.Add(command.EmailId);
            emailMessage.Subject = "Email Confirmation";
            _emailProvider.Send(1,emailMessage);

            return Task.CompletedTask;
        }

        public Task Handle(ResetPasswordAndUnlockUserCommand command)
        {
            var emailMessage = new EmailMessage
            {
                Content = _templateBuilder.Build(Template.Get(), new TemplateModel
                {
                    Title = "Reset Password",
                    Content = $"New password = {command.NewPassword}"
                })
            };

            emailMessage.To.Add(command.EmailId);
            emailMessage.Subject = "Reset Password";
            _emailProvider.Send(1, emailMessage);
            return Task.CompletedTask;
        }
    }
}
