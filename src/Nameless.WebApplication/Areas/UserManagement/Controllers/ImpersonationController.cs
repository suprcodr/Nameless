using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Nameless.WebApplication.Core;
using Nameless.WebApplication.Core.Identity;
using Nameless.WebApplication.Core.Identity.Models;

namespace Nameless.WebApplication.Areas.UserManagement.Controllers {

    [Area("UserManagement")]
    public sealed class ImpersonationController : ApplicationControllerBase {

        #region Private Constants

        private const string ORIGINAL_USER_ID_KEY = "OriginalUserID";

        #endregion Private Constants

        #region Private Read-Only Fields

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IdentityCookieOptions _cookieOptions;

        #endregion Private Read-Only Fields

        #region Public Constructors

        public ImpersonationController(UserManager<User> userManager, SignInManager<User> signInManager, IOptions<IdentityCookieOptions> cookieOptions) {
            _userManager = userManager;
            _signInManager = signInManager;
            _cookieOptions = cookieOptions.Value;
        }

        #endregion Public Constructors

        #region Public Methods

        [Authorize]
        public IActionResult ListUsers() {
            var users = _userManager.Users.ToList();

            return View(users);
        }

        [Authorize(Roles = Constants.Identity.IMPERSONATOR_ROLE_NAME)]
        public async Task<IActionResult> Impersonate(string userID) {
            var currentUserID = User.GetUserID();
            var impersonatedUser = await _userManager.FindByIdAsync(userID);
            var userPrincipal = await _signInManager.CreateUserPrincipalAsync(impersonatedUser);

            userPrincipal.Identities.First().AddClaim(new Claim(ORIGINAL_USER_ID_KEY, currentUserID));
            userPrincipal.Identities.First().AddClaim(new Claim(ClaimsPrincipalExtension.IsImpersonatingKey, ClaimsPrincipalExtension.IsImpersonatingValue));

            // sign out the current user
            await _signInManager.SignOutAsync();

            await HttpContext.Authentication.SignInAsync(_cookieOptions.ApplicationCookieAuthenticationScheme, userPrincipal);

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = Constants.Identity.IMPERSONATOR_ROLE_NAME)]
        public async Task<IActionResult> Return() {
            if (!User.IsImpersonating()) {
                throw new Exception("You are not impersonating now. Can't stop impersonation");
            }

            var originalUserID = User.FindFirst(ORIGINAL_USER_ID_KEY).Value;

            var originalUser = await _userManager.FindByIdAsync(originalUserID);

            await _signInManager.SignOutAsync();

            await _signInManager.SignInAsync(originalUser, isPersistent: true);

            return RedirectToAction("Index", "Home");
        }

        #endregion Public Methods
    }
}