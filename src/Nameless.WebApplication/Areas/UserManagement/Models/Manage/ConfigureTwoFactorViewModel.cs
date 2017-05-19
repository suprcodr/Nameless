using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Nameless.WebApplication.Areas.UserManagement.Models.Manage {

    public class ConfigureTwoFactorViewModel {

        #region Public Properties

        public string SelectedProvider { get; set; }

        public ICollection<SelectListItem> Providers { get; set; } = new List<SelectListItem>();

        #endregion Public Properties
    }
}