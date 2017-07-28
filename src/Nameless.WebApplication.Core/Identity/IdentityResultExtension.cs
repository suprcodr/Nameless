using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Nameless.WebApplication.Core.Identity {

    public static class IdentityResultExtension {

        #region Public Static Methods

        public static void FetchErrors(this IdentityResult source, ModelStateDictionary modelState) {
            if (source == null) { return; }

            source.Errors.Each(_ => modelState.AddModelError(string.Empty, _.Description));
        }

        #endregion Public Static Methods
    }
}