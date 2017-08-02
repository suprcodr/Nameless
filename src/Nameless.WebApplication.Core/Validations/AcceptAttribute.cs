using System.ComponentModel.DataAnnotations;

namespace Nameless.WebApplication.Core.Validations {

    public sealed class AcceptAttribute : ValidationAttribute {

        #region Public Override Methods

        public override bool IsValid(object value) {
            return value is bool && (bool)value;
        }

        #endregion Public Override Methods
    }
}