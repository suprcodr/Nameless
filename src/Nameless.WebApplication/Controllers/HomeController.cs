using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Nameless.Framework.Web;

namespace Nameless.WebApplication.Controllers {

    public class HomeController : ApplicationControllerBase {

        #region Public Methods

        public IActionResult Index() {
            return View();
        }

        [HttpGet]
        public IActionResult Tokenfield() {
            return View();
        }

        [HttpPost]
        public IActionResult Tokenfield(string query) {
            var data = new[] {
                new { label= "red", value= "red" },
                new { label= "green", value= "green" },
                new { label= "yellow", value= "yellow" },
                new { label= "blue", value= "blue" },
                new { label= "black", value= "black" },
                new { label= "white", value= "white" },
                new { label= "orange", value= "orange" }
            };

            var result = (query != null
                ? data.Where(_ => _.label.IndexOf(query) >= 0)
                : data).ToArray();

            return Json(result);
        }

        #endregion Public Methods
    }
}