using System.Threading.Tasks;

namespace Nameless.Skeleton.Framework.Web {

    public interface ISmsSender {

        #region Methods

        Task SendSmsAsync(string number, string message);

        #endregion Methods
    }
}