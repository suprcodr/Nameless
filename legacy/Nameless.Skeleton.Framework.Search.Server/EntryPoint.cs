using System;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace Nameless.Skeleton.Framework.Search.Server {

    internal class EntryPoint {

        #region Private Static Methods

        private static void Main(string[] args) {
            var baseAddress = "http://localhost:51000/";
            var config = new HttpSelfHostConfiguration(baseAddress);

            config.Routes.MapHttpRoute(
                name: "Default",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            using (var server = new HttpSelfHostServer(config)) {
                server.OpenAsync().Wait();

                Console.WriteLine($"Application running at: {baseAddress}");
                Console.WriteLine("Press any key to quit");
                Console.ReadKey();
            }
        }

        #endregion Private Static Methods
    }
}