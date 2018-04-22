#region usings

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Vedaantees.Framework.Providers.Communications.ServiceBus;
using Vedaantees.Framework.Providers.Security;
using Vedaantees.Framework.Providers.Storages.Data;
using Vedaantees.Framework.Shell.UserContexts;
using Vedaantees.Hosts.SingleSignOn.Domain.Commands;
using Vedaantees.Hosts.SingleSignOn.Stores.Models;
using IdentityModel;
using Newtonsoft.Json;
using Vedaantees.Framework.Types.Users;

#endregion

namespace Vedaantees.Hosts.SingleSignOn.Domain.Handlers
{
    public class DocumentDbHandler : ICommandHandler<RegisterUserCommand>,
                                     ICommandHandler<SuccessfulSignInCommand>,
                                     ICommandHandler<ResetPasswordAndUnlockUserCommand>,
                                     ICommandHandler<VerifyUserEmailCommand>,
                                     ICommandHandler<FailedSignInAttemptCommand>
    {
        private readonly IDocumentStore _documentStore;
        private readonly ICryptographicService _cryptographicService;
        private readonly IUserContextService _userContextService;

        public DocumentDbHandler(IDocumentStore documentStore, ICryptographicService cryptographicService, IUserContextService userContextService)
        {
            _documentStore = documentStore;
            _documentStore.SetSession("Users");
            _cryptographicService = cryptographicService;
            _userContextService = userContextService;
        }

        public Task Handle(FailedSignInAttemptCommand message)
        {
            var membership = _userContextService.GetContextByUsername<Membership>(message.Username).Result;

            if (membership != null)
            {
                membership.AccessFailCount++;
                if (membership.AccessFailCount == 5)
                    membership.IsLocked = true;
            }
            
            return Task.CompletedTask;
        }

        public Task Handle(RegisterUserCommand message)
        {
            var membership = new Membership(new List<UserClaim>())
            {
                ContextId = message.MembershipId,
                Email = message.EmailId,
                AccessFailCount = 0,
                ConfirmationToken = message.ConfirmationToken,
                IsEmailConfirmed = false,
                IsLocked = true,
                PasswordSalt = Guid.NewGuid().ToString(),
                MemberSinceDate = message.RequestedOn
            };

            membership.Password = _cryptographicService.ComputeHash(string.Concat(message.Password, membership.PasswordSalt));

            var user = new User
            {
                Id = message.Id,
                Username = message.Username
            };

            membership.Claims.Add(new UserClaim(JwtClaimTypes.Email, message.EmailId));
            membership.Claims.Add(new UserClaim(JwtClaimTypes.GivenName, message.Firstname));
            membership.Claims.Add(new UserClaim(JwtClaimTypes.FamilyName, message.Lastname));
            membership.Claims.Add(new UserClaim(JwtClaimTypes.Subject, message.Id));

            user.Contexts = new List<Context>
                            {
                                new Context
                                {
                                    Id = membership.ContextId,
                                    Content = JsonConvert.SerializeObject(membership),
                                    Claims = membership.Claims,
                                    Type = membership.GetType().FullName
                                }
                            };

            _documentStore.Store(user);
            return Task.CompletedTask;
        }

        public Task Handle(ResetPasswordAndUnlockUserCommand message)
        {
            var membership = _userContextService.GetContextByUsername<Membership>(message.Username).Result;
            Debug.Assert(membership != null, "membership != null");

            membership.IsLocked = false;
            membership.AccessFailCount = 0;
            membership.PasswordSalt = Guid.NewGuid().ToString();
            membership.Password = _cryptographicService.ComputeHash(string.Concat(message.NewPassword, membership.PasswordSalt));
            return Task.CompletedTask;
        }

        public Task Handle(SuccessfulSignInCommand message)
        {
            var membership = _userContextService.GetContextByUsername<Membership>(message.Username).Result;
            Debug.Assert(membership != null, "membership != null");
            membership.LastLoginDate = message.RequestedOn;
            membership.AccessFailCount = 0;
            return Task.CompletedTask;
        }

        public Task Handle(VerifyUserEmailCommand message)
        {
            var membership = _userContextService.GetContextByUsername<Membership>(message.Username).Result;
            Debug.Assert(membership != null, "membership != null");
            membership.IsEmailConfirmed = true;
            membership.IsLocked = false;
            return Task.CompletedTask;
        }

        public Task Handle(RegisterUserFromExternalProviderCommand message)
        {
            var membership = new Membership(new List<UserClaim>())
            {
                Email = message.EmailId,
                AccessFailCount = 0,
                IsEmailConfirmed = true,
                IsLocked = false,
                PasswordSalt = Guid.NewGuid().ToString(),
                MemberSinceDate = message.RequestedOn
            };
            membership.Password = _cryptographicService.ComputeHash(string.Concat(message.Password, membership.PasswordSalt));

            var user = new User
            {
                Username = message.Username,
                Id = message.Id,
                Contexts = new List<Context>()
            };

            membership.Claims.Add(new UserClaim(JwtClaimTypes.Email, message.EmailId));

            user.Contexts = new List<Context>
            {
                new Context
                {
                    Id = membership.ContextId,
                    Content = JsonConvert.SerializeObject(membership),
                    Claims = membership.Claims,
                    Type = membership.GetType().FullName
                }
            };
            _documentStore.Store(user);
            return Task.CompletedTask;
        }
    }
}