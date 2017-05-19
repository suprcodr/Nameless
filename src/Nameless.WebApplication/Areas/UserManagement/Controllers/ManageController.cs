using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nameless.WebApplication.Areas.UserManagement.Models.Manage;
using Nameless.Framework.Web;
using Nameless.Framework.Web.Identity;
using Nameless.Framework.Web.Identity.Models;

namespace Nameless.WebApplication.Areas.UserManagement.Controllers {

    [Area("UserManagement")]
    public class ManageController : ApplicationControllerBase {

        #region Private Read-Only Fields

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ICommunicationService _communicationService;

        #endregion Private Read-Only Fields

        #region Public Constructors

        public ManageController(UserManager<User> userManager, SignInManager<User> signInManager, ICommunicationService communicationService) {
            Prevent.ParameterNull(userManager, nameof(userManager));
            Prevent.ParameterNull(signInManager, nameof(signInManager));
            Prevent.ParameterNull(communicationService, nameof(communicationService));

            _userManager = userManager;
            _signInManager = signInManager;
            _communicationService = communicationService;
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpGet]
        public async Task<IActionResult> Index(ManageMessageId message = ManageMessageId.None) {
            var statusMessage = string.Empty;
            switch (message) {
                case ManageMessageId.AddPhoneSuccess:
                    statusMessage = "Your phone number was added.";
                    break;

                case ManageMessageId.ChangePasswordSuccess:
                    statusMessage = "Your password has been changed.";
                    break;

                case ManageMessageId.SetTwoFactorSuccess:
                    statusMessage = "Your two-factor authentication provider has been set.";
                    break;

                case ManageMessageId.SetPasswordSuccess:
                    statusMessage = "Your password has been set.";
                    break;

                case ManageMessageId.RemovePhoneSuccess:
                    statusMessage = "Your phone number was removed.";
                    break;

                case ManageMessageId.Error:
                    statusMessage = "An error has occurred.";
                    break;
            }
            ViewData["StatusMessage"] = statusMessage;

            var user = await _userManager.GetCurrentUserAsync(HttpContext);
            if (user == null) {
                return View("Error");
            }
            var model = new IndexViewModel {
                HasPassword = await _userManager.HasPasswordAsync(user),
                PhoneNumber = await _userManager.GetPhoneNumberAsync(user),
                TwoFactor = await _userManager.GetTwoFactorEnabledAsync(user),
                Logins = await _userManager.GetLoginsAsync(user),
                BrowserRemembered = await _signInManager.IsTwoFactorClientRememberedAsync(user)
            };
            return View(model);
        }

        [Route(nameof(RemoveLogin))]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveLogin(RemoveLoginViewModel account) {
            var message = ManageMessageId.None;
            var user = await _userManager.GetCurrentUserAsync(HttpContext);
            if (user != null) {
                var result = await _userManager.RemoveLoginAsync(user, account.LoginProvider, account.ProviderKey);
                if (result.Succeeded) {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    message = ManageMessageId.RemoveLoginSuccess;
                }
            }
            return RedirectToAction(nameof(ManageLogins), new { Message = message });
        }

        [Route(nameof(AddPhoneNumber))]
        public IActionResult AddPhoneNumber() {
            return View();
        }

        [Route(nameof(AddPhoneNumber))]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPhoneNumber(AddPhoneNumberViewModel model) {
            if (!ModelState.IsValid) {
                return View(model);
            }
            // Generate the token and send it
            var user = await _userManager.GetCurrentUserAsync(HttpContext);
            if (user == null) {
                return View("Error");
            }
            var code = await _userManager.GenerateChangePhoneNumberTokenAsync(user, model.PhoneNumber);
            await _communicationService.SendSmsAsync(model.PhoneNumber, $"Your security code is: {code}");
            return RedirectToAction(nameof(VerifyPhoneNumber), new { PhoneNumber = model.PhoneNumber });
        }

        [Route(nameof(EnableTwoFactorAuthentication))]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EnableTwoFactorAuthentication() {
            var user = await _userManager.GetCurrentUserAsync(HttpContext);
            if (user != null) {
                await _userManager.SetTwoFactorEnabledAsync(user, true);
                await _signInManager.SignInAsync(user, isPersistent: false);
            }
            return RedirectToAction(nameof(Index), "Manage");
        }

        [Route(nameof(DisableTwoFactorAuthentication))]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DisableTwoFactorAuthentication() {
            var user = await _userManager.GetCurrentUserAsync(HttpContext);
            if (user != null) {
                await _userManager.SetTwoFactorEnabledAsync(user, false);
                await _signInManager.SignInAsync(user, isPersistent: false);
            }
            return RedirectToAction(nameof(Index), "Manage");
        }

        [Route(nameof(VerifyPhoneNumber))]
        [HttpGet]
        public async Task<IActionResult> VerifyPhoneNumber(string phoneNumber) {
            var user = await _userManager.GetCurrentUserAsync(HttpContext);
            if (user == null) {
                return View("Error");
            }
            var code = await _userManager.GenerateChangePhoneNumberTokenAsync(user, phoneNumber);
            // Send an SMS to verify the phone number
            return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        }

        [Route(nameof(VerifyPhoneNumber))]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model) {
            if (!ModelState.IsValid) {
                return View(model);
            }
            var user = await _userManager.GetCurrentUserAsync(HttpContext);
            if (user != null) {
                var result = await _userManager.ChangePhoneNumberAsync(user, model.PhoneNumber, model.Code);
                if (result.Succeeded) {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction(nameof(Index), new { Message = ManageMessageId.AddPhoneSuccess });
                }
            }
            // If we got this far, something failed, redisplay the form
            ModelState.AddModelError(string.Empty, "Failed to verify phone number");
            return View(model);
        }

        [Route(nameof(RemovePhoneNumber))]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemovePhoneNumber() {
            var user = await _userManager.GetCurrentUserAsync(HttpContext);
            if (user != null) {
                var result = await _userManager.SetPhoneNumberAsync(user, null);
                if (result.Succeeded) {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction(nameof(Index), new { Message = ManageMessageId.RemovePhoneSuccess });
                }
            }
            return RedirectToAction(nameof(Index), new { Message = ManageMessageId.Error });
        }

        [Route(nameof(ChangePassword))]
        [HttpGet]
        public IActionResult ChangePassword() {
            return View();
        }

        [Route(nameof(ChangePassword))]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model) {
            if (!ModelState.IsValid) {
                return View(model);
            }
            var user = await _userManager.GetCurrentUserAsync(HttpContext);
            if (user != null) {
                var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                if (result.Succeeded) {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction(nameof(Index), new { Message = ManageMessageId.ChangePasswordSuccess });
                }
                result.FetchErrors(ModelState);
                return View(model);
            }
            return RedirectToAction(nameof(Index), new { Message = ManageMessageId.Error });
        }

        [Route(nameof(SetPassword))]
        [HttpGet]
        public IActionResult SetPassword() {
            return View();
        }

        [Route(nameof(SetPassword))]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetPassword(SetPasswordViewModel model) {
            if (!ModelState.IsValid) {
                return View(model);
            }

            var user = await _userManager.GetCurrentUserAsync(HttpContext);
            if (user != null) {
                var result = await _userManager.AddPasswordAsync(user, model.NewPassword);
                if (result.Succeeded) {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction(nameof(Index), new { Message = ManageMessageId.SetPasswordSuccess });
                }
                result.FetchErrors(ModelState);
                return View(model);
            }
            return RedirectToAction(nameof(Index), new { Message = ManageMessageId.Error });
        }

        [Route(nameof(ManageLogins))]
        [HttpGet]
        public async Task<IActionResult> ManageLogins(ManageMessageId message = ManageMessageId.None) {
            var statusMessage = string.Empty;
            switch (message) {
                case ManageMessageId.RemoveLoginSuccess:
                    statusMessage = "The external login was removed.";
                    break;

                case ManageMessageId.AddLoginSuccess:
                    statusMessage = "The external login was added.";
                    break;

                case ManageMessageId.Error:
                    statusMessage = "An error has occurred.";
                    break;
            }
            ViewData["StatusMessage"] = statusMessage;

            var user = await _userManager.GetCurrentUserAsync(HttpContext);
            if (user == null) {
                return View("Error");
            }
            var userLogins = await _userManager.GetLoginsAsync(user);
            var otherLogins = _signInManager.GetExternalAuthenticationSchemes().Where(auth => userLogins.All(ul => auth.AuthenticationScheme != ul.LoginProvider)).ToList();
            ViewData["ShowRemoveButton"] = user.PasswordHash != null || userLogins.Count > 1;
            return View(new ManageLoginsViewModel {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }

        [Route(nameof(LinkLogin))]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LinkLogin(string provider) {
            // Request a redirect to the external login provider to link a login for the current user
            var redirectUrl = Url.Action(nameof(LinkLoginCallback), "Manage");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl, _userManager.GetUserId(User));
            return Challenge(properties, provider);
        }

        [Route(nameof(LinkLogin))]
        [HttpGet]
        public async Task<ActionResult> LinkLoginCallback() {
            var user = await _userManager.GetCurrentUserAsync(HttpContext);
            if (user == null) {
                return View("Error");
            }
            var info = await _signInManager.GetExternalLoginInfoAsync(await _userManager.GetUserIdAsync(user));
            if (info == null) {
                return RedirectToAction(nameof(ManageLogins), new { Message = ManageMessageId.Error });
            }
            var result = await _userManager.AddLoginAsync(user, info);
            var message = result.Succeeded ? ManageMessageId.AddLoginSuccess : ManageMessageId.Error;
            return RedirectToAction(nameof(ManageLogins), new { Message = message });
        }

        #endregion Public Methods

        #region Public Enumerators

        public enum ManageMessageId {
            None,
            AddPhoneSuccess,
            AddLoginSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

        #endregion Public Enumerators
    }
}