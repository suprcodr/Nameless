using System.ComponentModel.DataAnnotations;
using Nameless.Skeleton.WebApplication.Resources;

namespace Nameless.Skeleton.WebApplication.Areas.UserManagement.Models.Manage {

    public class SetPasswordViewModel {

        #region Public Properties

        [Required(ErrorMessageResourceType = typeof(Validations), ErrorMessageResourceName = "Required")]
        [StringLength(64, ErrorMessageResourceType = typeof(Validations), ErrorMessageResourceName = "StringLength", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Displays), Name = "NewPassword")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Displays), Name = "ConfirmNewPassword")]
        [Compare(nameof(NewPassword), ErrorMessageResourceType = typeof(Validations), ErrorMessageResourceName = "ConfirmNewPassword")]
        public string ConfirmPassword { get; set; }

        #endregion Public Properties
    }
}