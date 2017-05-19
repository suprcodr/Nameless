using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Nameless.WebApplication {

    /// <summary>
    /// Application entry point class.
    /// </summary>
    public class EntryPoint {

        #region Public Static Methods

        /// <summary>
        /// Application entry point method.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        public static void Main(string[] args) {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<StartUp>()
                .Build();

            host.Run();
        }

        #endregion Public Static Methods
    }
}