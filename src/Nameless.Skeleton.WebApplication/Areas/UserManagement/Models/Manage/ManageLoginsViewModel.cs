using System.Collections.Generic;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Nameless.Skeleton.WebApplication.Areas.UserManagement.Models.Manage {

    public class ManageLoginsViewModel {

        #region Public Properties

        public IList<UserLoginInfo> CurrentLogins { get; set; } = new List<UserLoginInfo>();

        public IList<AuthenticationDescription> OtherLogins { get; set; } = new List<AuthenticationDescription>();

        #endregion Public Properties
    }
}