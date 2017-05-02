using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Nameless.Skeleton.WebApplication.Areas.UserManagement.Models.Account;
using Nameless.Skeleton.WebApplication.Controllers;
using Nameless.Skeleton.Framework.Web;
using Nameless.Skeleton.Framework.Web.Identity;
using Nameless.Skeleton.Framework.Web.Identity.Models;

namespace Nameless.Skeleton.WebApplication.Areas.UserManagement.Controllers {

    [Area("UserManagement")]
    public class AccountController : ApplicationControllerBase {

        #region Private Read-Only Fields

        private readonly IStringLocalizer<AccountController> _localizer;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ICommunicationService _communicationService;

        #endregion Private Read-Only Fields

        #region Public Constructors

        public AccountController(IStringLocalizer<AccountController> localizer, UserManager<User> userManager, SignInManager<User> signInManager, ICommunicationService communicationService) {
            Prevent.ParameterNull(localizer, nameof(localizer));
            Prevent.ParameterNull(userManager, nameof(userManager));
            Prevent.ParameterNull(signInManager, nameof(signInManager));
            Prevent.ParameterNull(communicationService, nameof(communicationService));

            _localizer = localizer;
            _userManager = userManager;
            _signInManager = signInManager;
            _communicationService = communicationService;
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpGet]
        [AllowAnonymous]
        public IActionResult SignIn(string returnUrl = null) {
            ViewData["ReturnUrl"] = returnUrl;

            return View(new LoginViewModel {
                LoginProviders = _signInManager.GetExternalAuthenticationSchemes().Select(_ => new LoginProviderViewModel {
                    AuthenticationScheme = _.AuthenticationScheme,
                    DisplayName = _.DisplayName
                })
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(LoginViewModel model, string returnUrl = null) {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid) {
                // To disable password failures to trigger account lockout, set lockoutOnFailure: false
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);

                if (result.Succeeded) {
                    return RedirectToLocal(returnUrl);
                }

                if (result.RequiresTwoFactor) {
                    return RedirectToAction(nameof(SendCode), new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                }

                if (result.IsLockedOut) {
                    return View("Lockout");
                }

                ModelState.AddModelError(string.Empty, _localizer["Invalid login attempt."]);

                return View(model);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult SignUp(string returnUrl = null) {
            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(RegisterViewModel model, string returnUrl = null) {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid) {
                var user = new User {
                    FullName = model.FullName,
                    UserName = model.Email,
                    Email = model.Email
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded) {
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                    // Send an email with this link
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action(nameof(ConfirmEmail), "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);

                    await _communicationService.SendEmailAsync(model.Email, "Confirm your account", $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>");
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToLocal(returnUrl);
                }
                result.FetchErrors(ModelState);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignOut() {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, string returnUrl = null) {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { ReturnUrl = returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null) {
            if (remoteError != null) {
                ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");
                return View(nameof(SignIn));
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null) {
                return RedirectToAction(nameof(SignIn));
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
            if (result.Succeeded) {
                return RedirectToLocal(returnUrl);
            }

            if (result.RequiresTwoFactor) {
                return RedirectToAction(nameof(SendCode), new { ReturnUrl = returnUrl });
            }

            if (result.IsLockedOut) {
                return View("Lockout");
            }

            // If the user does not have an account, then ask the user to create an account.
            ViewData["ReturnUrl"] = returnUrl;
            ViewData["LoginProvider"] = info.LoginProvider;
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            return View(nameof(ExternalLoginConfirmation), new ExternalLoginConfirmationViewModel { Email = email });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl = null) {
            if (ModelState.IsValid) {
                // Get the information about the user from the external login provider
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null) {
                    return View("ExternalLoginFailure");
                }
                var user = new User { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded) {
                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded) {
                        await _signInManager.SignInAsync(user, isPersistent: false);

                        return RedirectToLocal(returnUrl);
                    }
                }
                result.FetchErrors(ModelState);
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code) {
            if (userId == null || code == null) {
                return View("Error");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) {
                return View("Error");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword() {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model) {
            if (ModelState.IsValid) {
                var user = await _userManager.FindByNameAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user))) {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View(nameof(ForgotPasswordConfirmation));
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                // Send an email with this link
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action(nameof(ResetPassword), "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                await _communicationService.SendEmailAsync(model.Email, "Reset Password", $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");
                return View(nameof(ForgotPasswordConfirmation));
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation() {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null) {
            return code == null ? View("Error") : View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model) {
            if (!ModelState.IsValid) {
                return View(model);
            }
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null) {
                // Don't reveal that the user does not exist
                return RedirectToAction(nameof(ResetPasswordConfirmation), "Account");
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded) {
                return RedirectToAction(nameof(ResetPasswordConfirmation), "Account");
            }
            result.FetchErrors(ModelState);
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation() {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl = null, bool rememberMe = false) {
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null) {
                return View("Error");
            }
            var userFactors = await _userManager.GetValidTwoFactorProvidersAsync(user);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendCode(SendCodeViewModel model) {
            if (!ModelState.IsValid) {
                return View();
            }

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null) {
                return View("Error");
            }

            // Generate the token and send it
            var code = await _userManager.GenerateTwoFactorTokenAsync(user, model.SelectedProvider);
            if (string.IsNullOrWhiteSpace(code)) {
                return View("Error");
            }

            var message = $"Your security code is: {code}";
            switch (model.SelectedProvider) {
                case "Email":
                    await _communicationService.SendEmailAsync(await _userManager.GetEmailAsync(user), "Security Code", message);
                    break;

                case "Phone":
                    await _communicationService.SendSmsAsync(await _userManager.GetPhoneNumberAsync(user), message);
                    break;
            }

            return RedirectToAction(nameof(VerifyCode), new {
                Provider = model.SelectedProvider,
                ReturnUrl = model.ReturnUrl,
                RememberMe = model.RememberMe
            });
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyCode(string provider, bool rememberMe, string returnUrl = null) {
            // Require that the user has already logged in via username/password or external login
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null) {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerifyCode(VerifyCodeViewModel model) {
            if (!ModelState.IsValid) {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes.
            // If a user enters incorrect codes for a specified amount of time then the user account
            // will be locked out for a specified amount of time.
            var result = await _signInManager.TwoFactorSignInAsync(model.Provider, model.Code, model.RememberMe, model.RememberBrowser);
            if (result.Succeeded) {
                return RedirectToLocal(model.ReturnUrl);
            }
            if (result.IsLockedOut) {
                return View("Lockout");
            }

            ModelState.AddModelError(string.Empty, _localizer["Invalid code."]);
            return View(model);
        }

        #endregion Public Methods

        #region Private Methods

        private IActionResult RedirectToLocal(string returnUrl) {
            if (Url.IsLocalUrl(returnUrl)) {
                return Redirect(returnUrl);
            }

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        #endregion Private Methods
    }
}