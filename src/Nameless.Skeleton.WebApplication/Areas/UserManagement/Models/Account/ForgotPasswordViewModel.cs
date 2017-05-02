using System.ComponentModel.DataAnnotations;
using Nameless.Skeleton.WebApplication.Resources;

namespace Nameless.Skeleton.WebApplication.Areas.UserManagement.Models.Account {

    public class ForgotPasswordViewModel {

        #region Public Properties

        [Required(ErrorMessageResourceType = typeof(Validations), ErrorMessageResourceName = "Required")]
        [EmailAddress(ErrorMessageResourceType = typeof(Validations), ErrorMessageResourceName = "EmailAddress")]
        [Display(ResourceType = typeof(Displays), Name = "Email")]
        public string Email { get; set; }

        #endregion Public Properties
    }
}