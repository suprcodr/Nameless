using System.ComponentModel.DataAnnotations;
using Nameless.Skeleton.WebApplication.Resources;

namespace Nameless.Skeleton.WebApplication.Areas.UserManagement.Models.Manage {

    public class AddPhoneNumberViewModel {

        #region Public Properties

        [Required(ErrorMessageResourceType = typeof(Validations), ErrorMessageResourceName = "Required")]
        [Phone(ErrorMessageResourceType = typeof(Validations), ErrorMessageResourceName = "Phone")]
        [Display(ResourceType = typeof(Displays), Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }

        #endregion Public Properties
    }
}