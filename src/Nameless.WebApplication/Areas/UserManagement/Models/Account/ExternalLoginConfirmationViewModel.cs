using System.ComponentModel.DataAnnotations;
using Nameless.WebApplication.Resources;

namespace Nameless.WebApplication.Areas.UserManagement.Models.Account {

    public class ExternalLoginConfirmationViewModel {

        #region Public Properties

        [Required(ErrorMessageResourceType = typeof(Validations), ErrorMessageResourceName = "Required")]
        [EmailAddress(ErrorMessageResourceType = typeof(Validations), ErrorMessageResourceName = "EmailAddress")]
        [Display(ResourceType = typeof(Displays), Name = "Email")]
        public string Email { get; set; }

        #endregion Public Properties
    }
}