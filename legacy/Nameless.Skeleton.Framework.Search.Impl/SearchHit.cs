﻿using System;
using Lucene.Net.Documents;

namespace Nameless.Skeleton.Framework.Search {

    /// <summary>
    /// Default implementation of <see cref="ISearchHit"/>.
    /// </summary>
    public class SearchHit : ISearchHit {

        #region Private Read-Only Fields

        private readonly Document _document;
        private readonly float _score;

        #endregion Private Read-Only Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="SearchHit"/>
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="score">The score.</param>
        public SearchHit(Document document, float score) {
            if (document == null) {
                throw new ArgumentNullException(nameof(document));
            }

            _document = document;
            _score = score;
        }

        #endregion Public Constructors

        #region ISearchHit Members

        /// <inheritdoc />
        public string DocumentID {
            get { return GetString(nameof(ISearchHit.DocumentID)); }
        }

        /// <inheritdoc />
        public float Score {
            get { return _score; }
        }

        /// <inheritdoc />
        public int GetInt(string name) {
            var field = _document.GetField(name);

            return field != null ? int.Parse(field.StringValue) : 0;
        }

        /// <inheritdoc />
        public double GetDouble(string name) {
            var field = _document.GetField(name);

            return field != null ? double.Parse(field.StringValue) : 0d;
        }

        /// <inheritdoc />
        public bool GetBoolean(string name) {
            return GetInt(name) > 0;
        }

        /// <inheritdoc />
        public string GetString(string name) {
            var field = _document.GetField(name);

            return field != null ? field.StringValue : null;
        }

        /// <inheritdoc />
        public DateTimeOffset GetDateTimeOffset(string name) {
            var field = _document.GetField(name);

            return field != null ? DateTools.StringToDate(field.StringValue) : DateTimeOffset.MinValue;
        }

        #endregion ISearchHit Members
    }
}