﻿using System;
using System.IO;
using System.Linq;
using Lucene.Net.Analysis;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using LuceneDirectory = Lucene.Net.Store.Directory;

namespace Nameless.Framework.Search {

    public class Index : IIndex, IDisposable {

        #region Private Read-Only Fields

        private readonly Analyzer _analyzer;
        private readonly string _basePath;

        #endregion Private Read-Only Fields

        #region Private Fields

        private LuceneDirectory _directory;
        private bool _disposed;

        #endregion Private Fields

        #region Public Static Read-Only Fields

        /// <summary>
        /// GEts the default minimun date time.
        /// </summary>
        public static readonly DateTime DefaultMinDateTime = new DateTime(1980, 1, 1);

        /// <summary>
        /// Gets the batch size.
        /// </summary>
        public static readonly int BatchSize = BooleanQuery.MaxClauseCount;

        #endregion Public Static Read-Only Fields

        #region Public Constructors

        public Index(Analyzer analyzer, string basePath, string name) {
            if (analyzer == null) {
                throw new ArgumentNullException(nameof(analyzer));
            }
            if (string.IsNullOrWhiteSpace(basePath)) {
                throw new ArgumentException("Parameter cannot be null, empty or white spaces.", nameof(basePath));
            }
            if (string.IsNullOrWhiteSpace(name)) {
                throw new ArgumentException("Parameter cannot be null, empty or white spaces.", nameof(name));
            }

            _analyzer = analyzer;
            _basePath = basePath;

            Name = name;

            Initialize();
        }

        #endregion Public Constructors

        #region Destructor

        ~Index() {
            Dispose(disposing: false);
        }

        #endregion Destructor

        #region Private Static Methods

        private static Document CreateDocument(IDocumentIndex documentIndex) {
            var documentIndexImpl = documentIndex as DocumentIndex;
            if (documentIndexImpl == null) {
                throw new InvalidCastException($"Parameter {nameof(documentIndex)} must be of type {nameof(DocumentIndex)}");
            }

            var document = new Document();

            foreach (var entry in documentIndexImpl.Entries) {
                if (entry.Value.Value == null) { continue; }
                var fieldName = entry.Key;
                var fieldValue = entry.Value.Value;

                var store = entry.Value.Options.HasFlag(DocumentIndexOptions.Store) ? Field.Store.YES : Field.Store.NO;
                var analyze = entry.Value.Options.HasFlag(DocumentIndexOptions.Analyze);
                var sanitize = entry.Value.Options.HasFlag(DocumentIndexOptions.Sanitize);

                switch (entry.Value.Type) {
                    case DocumentIndex.IndexableType.Integer:
                        document.Add(new IntField(fieldName, Convert.ToInt32(fieldValue), store));
                        break;

                    case DocumentIndex.IndexableType.Text:
                        var textValue = sanitize ? Convert.ToString(fieldValue).RemoveHtmlTags() : Convert.ToString(fieldValue);
                        
                        if (analyze) {
                            document.Add(new TextField(fieldName, textValue, store));
                        } else {
                            document.Add(new StringField(fieldName, textValue, store));
                        }
                        break;

                    case DocumentIndex.IndexableType.DateTime:
                        var dateValue = string.Empty;
                        if (fieldValue is DateTimeOffset) {
                            dateValue = ((DateTimeOffset)fieldValue).ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ");
                        } else {
                            dateValue = ((DateTime)fieldValue).ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ");
                        }
                        document.Add(new StringField(fieldName, dateValue, store));

                        break;

                    case DocumentIndex.IndexableType.Boolean:
                        document.Add(new StringField(fieldName, Convert.ToString(fieldValue).ToLower(), store));
                        break;

                    case DocumentIndex.IndexableType.Number:
                        document.Add(new DoubleField(fieldName, Convert.ToDouble(fieldValue), store));
                        break;

                    default:
                        break;
                }
            }
            return document;
        }

        #endregion Private Static Methods

        #region Private Methods

        private void Initialize() {
            _directory = Lucene.Net.Store.FSDirectory.Open(new DirectoryInfo(Path.Combine(_basePath, Name)));

            // Creates the index directory
            using (CreateIndexWriter()) { }
        }

        private bool IndexDirectoryExists() {
            return Directory.Exists(Path.Combine(_basePath, Name));
        }

        private IndexWriter CreateIndexWriter() {
            return new IndexWriter(_directory, new IndexWriterConfig(IndexProvider.Version, _analyzer));
        }

        private void Dispose(bool disposing) {
            if (_disposed) { return; }
            if (disposing) {
                if (_directory != null) {
                    _directory.Dispose();
                }
            }

            _directory = null;
            _disposed = true;
        }

        #endregion Private Methods

        #region IIndex Members

        public string Name { get; }

        public bool IsEmpty() {
            return TotalDocuments() <= 0;
        }

        public int TotalDocuments() {
            if (!IndexDirectoryExists()) {
                return -1;
            }

            using (var reader = DirectoryReader.Open(_directory)) {
                return reader.NumDocs;
            }
        }

        public IDocumentIndex NewDocument(string documentID) {
            return new DocumentIndex(documentID);
        }

        public void StoreDocuments(params IDocumentIndex[] documents) {
            if (documents == null) { return; }
            if (documents.Length == 0) { return; }

            DeleteDocuments(documents.OfType<DocumentIndex>().Select(_ => _.ID).ToArray());

            using (var writer = CreateIndexWriter()) {
                foreach (var document in documents) {
                    writer.AddDocument(CreateDocument(document));
                }
            }
        }

        public void DeleteDocuments(params string[] documentIDs) {
            if (documentIDs == null) { return; }
            if (documentIDs.Length == 0) { return; }

            using (var writer = CreateIndexWriter()) {
                // Process documents by batch as there is a max number of terms a query can contain (1024 by default).

                var pageCount = documentIDs.Length / (BatchSize + 1);
                for (var page = 0; page < pageCount; page++) {
                    var query = new BooleanQuery();
                    try {
                        var batch = documentIDs.Skip(page * BatchSize).Take(BatchSize);
                        foreach (var id in batch) {
                            query.Add(new BooleanClause(new TermQuery(new Term(nameof(ISearchHit.DocumentID), id.ToString())), BooleanClause.Occur.SHOULD));
                        }
                        writer.DeleteDocuments(query);
                    } catch (Exception) { /* Just skip error */ }
                }
            }
        }

        public ISearchBuilder CreateSearchBuilder() {
            return new SearchBuilder(_directory, _analyzer);
        }

        #endregion IIndex Members

        #region IDisposable Members

        public void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Members
    }
}