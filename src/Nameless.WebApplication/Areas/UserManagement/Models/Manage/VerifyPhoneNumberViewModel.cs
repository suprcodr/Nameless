using System.ComponentModel.DataAnnotations;
using Nameless.WebApplication.Resources;

namespace Nameless.WebApplication.Areas.UserManagement.Models.Manage {

    public class VerifyPhoneNumberViewModel {

        #region Public Properties

        [Required(ErrorMessageResourceType = typeof(Validations), ErrorMessageResourceName = "Required")]
        public string Code { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validations), ErrorMessageResourceName = "Required")]
        [Phone(ErrorMessageResourceType = typeof(Validations), ErrorMessageResourceName = "Phone")]
        [Display(ResourceType = typeof(Displays), Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }

        #endregion Public Properties
    }
}