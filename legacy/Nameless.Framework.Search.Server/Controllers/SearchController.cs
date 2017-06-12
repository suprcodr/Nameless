using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Nameless.Framework.Search.Controllers {
    public class SearchController : ApiController {
        #region Private Read-Only Fields

        private readonly IIndexProvider _indexProvider;

        #endregion

        #region Public Constructors

        public SearchController(IIndexProvider indexProvider) {
            Prevent.ParameterNull(indexProvider, nameof(indexProvider));

            _indexProvider = indexProvider;
        }

        #endregion

        #region Public Methods

        [HttpGet]
        public IHttpActionResult Search(SearchModel model) {

        }
        
        #endregion
    }
}
