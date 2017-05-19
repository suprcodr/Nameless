﻿using Lucene.Net.Analysis.Standard;

namespace Nameless.Framework.Search {

    /// <summary>
    /// Default implementation of <see cref="IAnalyzerSelector"/>.
    /// </summary>
    public class AnalyzerSelector : IAnalyzerSelector {

        #region ILuceneAnalyzerSelector Members

        /// <inheritdoc />
        public AnalyzerSelectorResult GetLuceneAnalyzer(string indexName) {
            return new AnalyzerSelectorResult {
                Priority = -5,
                Analyzer = new StandardAnalyzer(IndexProvider.Version)
            };
        }

        #endregion ILuceneAnalyzerSelector Members
    }
}