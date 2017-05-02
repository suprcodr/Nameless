using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Nameless.Skeleton.WebApplication {

    public class EntryPoint {

        #region Public Static Methods

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