using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nameless.WebApplication.Resources;

namespace Nameless.WebApplication.Areas.UserManagement.Models.Account {

    public class SendCodeViewModel {

        #region Public Properties

        [Required(ErrorMessageResourceType = typeof(Validations), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Displays), Name = "SelectedProvider")]
        public string SelectedProvider { get; set; }

        public ICollection<SelectListItem> Providers { get; set; } = new List<SelectListItem>();

        public string ReturnUrl { get; set; }

        public bool RememberMe { get; set; }

        #endregion Public Properties
    }
}