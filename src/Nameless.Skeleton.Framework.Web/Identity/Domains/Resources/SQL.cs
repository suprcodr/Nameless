using System.Reflection;

namespace Nameless.Skeleton.Framework.Web.Identity.Domains.Resources {

    public sealed class SQL : DynamicResourceAccessor {

        #region Private Static Read-Only Fields

        private static readonly SQL _instance = new SQL();

        #endregion Private Static Read-Only Fields

        #region Public Static Properties

        public static dynamic Instance { get; } = _instance;

        #endregion Public Static Properties

        #region Private Constructors

        private SQL()
            : base(typeof(SQL).GetTypeInfo().Assembly, ".{0}.sql") { }

        #endregion Private Constructors
    }
}