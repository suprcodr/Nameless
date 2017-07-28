using System.Threading.Tasks;

namespace Nameless.WebApplication.Core {

    public interface ISmsSender {

        #region Methods

        Task SendSmsAsync(string number, string message);

        #endregion Methods
    }
}