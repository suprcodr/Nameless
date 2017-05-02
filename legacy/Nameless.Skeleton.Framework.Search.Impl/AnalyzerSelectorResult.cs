using Lucene.Net.Analysis;

namespace Nameless.Skeleton.Framework.Search {

    /// <summary>
    /// Represents a Lucene analyzer selector result.
    /// </summary>
    public class AnalyzerSelectorResult {

        #region Public Properties

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// Gets or sets the analyzer instance.
        /// </summary>
        public Analyzer Analyzer { get; set; }

        #endregion Public Properties
    }
}