using System.ComponentModel.DataAnnotations;
using Nameless.Skeleton.WebApplication.Resources;

namespace Nameless.Skeleton.WebApplication.Areas.UserManagement.Models.Account {

    public class VerifyCodeViewModel {

        #region Public Properties

        [Required(ErrorMessageResourceType = typeof(Validations), ErrorMessageResourceName = "Required")]
        public string Provider { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validations), ErrorMessageResourceName = "Required")]
        public string Code { get; set; }

        public string ReturnUrl { get; set; }

        [Display(ResourceType = typeof(Displays), Name = "RememberThisBrowser")]
        public bool RememberBrowser { get; set; }

        [Display(ResourceType = typeof(Displays), Name = "RememberMe")]
        public bool RememberMe { get; set; }

        #endregion Public Properties
    }
}