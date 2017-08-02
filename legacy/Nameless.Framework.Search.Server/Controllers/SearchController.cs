using System.Web.Http;
using Nameless.Framework.Search.Models;

namespace Nameless.Framework.Search.Controllers {

    public class SearchController : ApiController {

        #region Private Read-Only Fields

        private readonly IIndexProvider _indexProvider;

        #endregion Private Read-Only Fields

        #region Public Constructors

        public SearchController(IIndexProvider indexProvider) {
            Prevent.ParameterNull(indexProvider, nameof(indexProvider));

            _indexProvider = indexProvider;
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpGet]
        public IHttpActionResult Search(QueryBindingModel query) {
            throw new System.NotImplementedException();
        }

        #endregion Public Methods
    }
}