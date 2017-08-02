using System.ComponentModel.DataAnnotations;
using Nameless.WebApplication.Core.Validations;
using Nameless.WebApplication.Resources;

namespace Nameless.WebApplication.Areas.UserManagement.Models.Account {

    public class RegisterViewModel {

        #region Public Properties

        [Required(ErrorMessageResourceType = typeof(Validations), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Displays), Name = "FullName")]
        public string FullName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validations), ErrorMessageResourceName = "Required")]
        [EmailAddress(ErrorMessageResourceType = typeof(Validations), ErrorMessageResourceName = "EmailAddress")]
        [Display(ResourceType = typeof(Displays), Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(64, ErrorMessageResourceType = typeof(Validations), ErrorMessageResourceName = "StringLength", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Displays), Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Displays), Name = "ConfirmPassword")]
        [Compare(nameof(Password), ErrorMessageResourceType = typeof(Validations), ErrorMessageResourceName = "ConfirmPassword")]
        public string ConfirmPassword { get; set; }

        [Accept(ErrorMessageResourceType = typeof(Validations), ErrorMessageResourceName = "Accept")]
        public bool TermsAgreement { get; set; }

        #endregion Public Properties
    }
}