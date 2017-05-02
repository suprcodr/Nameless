using System.Threading.Tasks;

namespace Nameless.Skeleton.Framework.Web {

    public interface IEmailSender {

        #region Methods

        Task SendEmailAsync(string email, string subject, string message);

        #endregion Methods
    }
}