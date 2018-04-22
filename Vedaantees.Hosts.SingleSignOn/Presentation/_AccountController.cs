using System;
using System.IO;
using System.Threading.Tasks;
using Vedaantees.Framework.Providers.Communications.ServiceBus;
using Vedaantees.Framework.Providers.Security;
using Vedaantees.Framework.Types.Results;
using Vedaantees.Hosts.SingleSignOn.Domain.Commands;
using Vedaantees.Hosts.SingleSignOn.Domain.Queries;
using Vedaantees.Hosts.SingleSignOn.Presentation.Models;
using Vedaantees.Hosts.SingleSignOn.Stores.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using IdentityModel;
using System.Linq;

namespace Vedaantees.Hosts.SingleSignOn.Presentation
{
    public sealed class AccountController : Controller
    {
        private readonly ICommandService _commandService;
        private readonly IQueryService _queryService;
        private readonly ICryptographicService _cryptographicService;
        private readonly IIdentityServerInteractionService _interactionService;

        public AccountController(ICommandService commandService, 
                                 IQueryService queryService, 
                                 ICryptographicService cryptographicService, 
                                 IIdentityServerInteractionService interactionService)
        {
            _commandService = commandService;
            _queryService = queryService;
            _cryptographicService = cryptographicService;
            _interactionService = interactionService;
        }

        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel
                        {
                            ReturnUrl = returnUrl
                        });
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginInputViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(new LoginViewModel(viewModel));

            Membership membership = null;

            try
            {
                membership = (_queryService.ExecuteQuery<GetMembershipByUsername, Membership>(new GetMembershipByUsername { Username = viewModel.Username }).Result);
            }
            catch(MemberNotFoundException)
            {
                //Let this exception pass.
            }
            catch(Exception)
            {
                //Need a way to redirect here.
            }
            
            var password = _cryptographicService.ComputeHash(string.Concat(viewModel.Password, membership?.PasswordSalt));
            
            if (membership == null || password == null || membership?.Password != password || !membership.IsEmailConfirmed || membership.IsLocked)
            {
                ModelState.AddModelError("InvalidPassword","Invalid Username/Password combination OR you are yet to confirm your email OR account is locked.");
                await _commandService.ExecuteCommand(new FailedSignInAttemptCommand { Username = viewModel.Username });
                return View(new LoginViewModel(viewModel));
            }


            AuthenticationProperties props = null;
            if (viewModel.RememberMe)
            {
                props = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMonths(1)
                };
            };

            var result = await _commandService.ExecuteCommand(new SuccessfulSignInCommand { Username = viewModel.Username });
            await HttpContext.SignInAsync(membership.GetClaim(JwtClaimTypes.Subject), viewModel.Username,props);
            return Redirect(_interactionService.IsValidReturnUrl(viewModel.ReturnUrl) ? viewModel.ReturnUrl : "~/");
        }
        

        public IActionResult Register(string loginUrl)
        {
            return View(new RegisterViewModel{ LoginUrl = loginUrl ?? Url.Action("Login", "Account") }); //
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var result = await _commandService.ExecuteCommand(new RegisterUserCommand
                                                              {
                                                                    Username = viewModel.Username,
                                                                    Firstname = viewModel.Firstname,
                                                                    Lastname = viewModel.Lastname,
                                                                    ConfirmationToken = Guid.NewGuid().ToString(),
                                                                    EmailId = viewModel.Email,
                                                                    Password = viewModel.Password,
                                                                    SiteAddress = $"{Url.ActionContext.HttpContext.Request.Scheme}://{Url.ActionContext.HttpContext.Request.Host}"
                                                              });

            if (result.MethodResultState == MethodResultStates.Successful)
                return View("RegistrationConfirmation");
            else
            {
                ModelState.AddModelError("Validation-Error", result.Message);
                return View(viewModel);
            }
        }
        
        public IActionResult ResetPassword(string loginUrl)
        {
            return View(new ResetPasswordViewModel{ LoginUrl = loginUrl ?? Url.Action("Login","Account") });
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var membership = _queryService.ExecuteQuery<GetMembershipByUsername, Membership>(new GetMembershipByUsername { Username = viewModel.Username }).Result;

            if (membership != null)
                await _commandService.ExecuteCommand(new ResetPasswordAndUnlockUserCommand
                {
                    Username = viewModel.Username,
                    EmailId = membership.Email,
                    NewPassword = Path.GetRandomFileName(),
                    SiteAddress =$"{Url.ActionContext.HttpContext.Request.Scheme}://{Url.ActionContext.HttpContext.Request.Host}"
                });

            return View("ResetPasswordNextStep", viewModel);
        }
        

        public async Task<IActionResult> ConfirmEmail(string emailId, string token)
        {
            var membership = _queryService.ExecuteQuery<GetMembershipByEmail, Membership>(new GetMembershipByEmail { Email = emailId }).Result;
            await _commandService.ExecuteCommand(new VerifyUserEmailCommand { ConfirmationToken = token, EmailId = emailId, Id = membership.GetClaim(JwtClaimTypes.Subject) });
            return View();
        }
    }
}
