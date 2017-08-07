using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Nameless.WebApplication.Resources;

namespace Nameless.WebApplication.Areas.UserManagement.Models.Account {

    public class SignInViewModel {

        #region Public Properties

        [Required(ErrorMessageResourceType = typeof(Validations), ErrorMessageResourceName = "Required")]
        [EmailAddress(ErrorMessageResourceType = typeof(Validations), ErrorMessageResourceName = "EmailAddress")]
        [Display(ResourceType = typeof(Displays), Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validations), ErrorMessageResourceName = "Required")]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Displays), Name = "Password")]
        public string Password { get; set; }

        [Display(ResourceType = typeof(Displays), Name = "RememberMe")]
        public bool RememberMe { get; set; }

        public IEnumerable<LoginProviderViewModel> LoginProviders { get; set; }

        #endregion Public Properties
    }
}