using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nameless.Framework.Search.Models {
    public class SearchParameter {
        #region Public Properties

        public IEnumerable<string> Fields { get; set; }

        public string Terms { get; set; }

        #endregion
    }
}
