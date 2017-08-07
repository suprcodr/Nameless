using System;

namespace Nameless.WebApplication.Areas.UserManagement.Models.Account {

    public class UserAccountViewModel {

        #region Public Properties

        public Guid UserID { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public string JobRole { get; set; }

        public string ProfilePicturePath { get; set; }

        public byte[] ProfilePictureBlob { get; set; }

        public DateTimeOffset RegisterDate { get; set; }

        #endregion Public Properties
    }
}