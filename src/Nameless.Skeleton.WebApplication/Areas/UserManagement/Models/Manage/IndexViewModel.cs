using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Nameless.Skeleton.WebApplication.Areas.UserManagement.Models.Manage {

    public class IndexViewModel {

        #region Public Properties

        public bool HasPassword { get; set; }

        public IList<UserLoginInfo> Logins { get; set; } = new List<UserLoginInfo>();

        public string PhoneNumber { get; set; }

        public bool TwoFactor { get; set; }

        public bool BrowserRemembered { get; set; }

        #endregion Public Properties
    }
}