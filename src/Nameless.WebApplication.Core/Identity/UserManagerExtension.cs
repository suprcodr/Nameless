using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Nameless.WebApplication.Core.Identity {

    public static class UserManagerExtension {

        #region Public Static Methods

        public static Task<TUser> GetCurrentUserAsync<TUser>(this UserManager<TUser> source, HttpContext context)
            where TUser : class {
            if (source == null) { return Task.FromResult(default(TUser)); }

            return source.GetUserAsync(context.User);
        }

        #endregion Public Static Methods
    }
}