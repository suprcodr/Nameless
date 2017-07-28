using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Nameless.Framework.Data.Generic {

    public sealed class NameValueParameter {

        #region Public Properties

        public string Name { get; }
        public object Value { get; }

        #endregion Public Properties

        #region Public Constructors

        public NameValueParameter(string name, object value) {
            Prevent.ParameterNullOrWhiteSpace(name, nameof(name));

            Name = name;
            Value = value;
        }

        #endregion Public Constructors

        #region Public Methods

        public TResult GetValue<TResult>() {
            return (Value != null)
                ? (TResult)Convert.ChangeType(Value, typeof(TResult))
                : default(TResult);
        }

        public bool Equals(NameValueParameter obj) {
            return obj != null && string.Equals(obj.Name, Name, StringComparison.CurrentCultureIgnoreCase);
        }

        #endregion Public Methods

        #region Public Override Methods

        public override bool Equals(object obj) {
            return Equals(obj as NameValueParameter);
        }

        public override int GetHashCode() {
            return Name.GetHashCode();
        }

        #endregion Public Override Methods
    }

    public sealed class NameValueParameterSet : IEnumerable<NameValueParameter> {

        #region Private Read-Only Fields

        private readonly ISet<NameValueParameter> _items;

        #endregion Private Read-Only Fields

        #region Private Properties

        private ISet<NameValueParameter> Items {
            get { return _items; }
        }

        #endregion Private Properties

        #region Public Properties

        public NameValueParameter this[string name] {
            get { return GetParameter(name); }
            set { SetParameter(name, value); }
        }

        #endregion Public Properties

        #region Public Constructors

        public NameValueParameterSet(IEnumerable<NameValueParameter> collection) {
            _items = new HashSet<NameValueParameter>(collection ?? Enumerable.Empty<NameValueParameter>());
        }

        #endregion Public Constructors

        #region Public Methods
        
        public bool Add(string name, object value) {
            return Items.Add(new NameValueParameter(name, value));
        }

        public bool Add(NameValueParameter parameter) {
            return Items.Add(parameter);
        }

        public bool Remove(string name) {
            var item = Items.SingleOrDefault(_ => string.Equals(_.Name, name, StringComparison.CurrentCultureIgnoreCase));

            return (item != null)
                ? Items.Remove(item)
                : false;
        }

        public bool Remove(NameValueParameter parameter) {
            return Items.Remove(parameter);
        }

        #endregion Public Methods

        #region Private Methods

        private NameValueParameter GetParameter(string name) {
            return Items.SingleOrDefault(_ => string.Equals(_.Name, name, StringComparison.CurrentCultureIgnoreCase));
        }

        private void SetParameter(string name, NameValueParameter parameter) {
            var currentParameter = GetParameter(name);

            if (currentParameter != null) {
                Items.Remove(currentParameter);
            }

            Items.Add(parameter);
        }

        #endregion Private Methods

        #region IEnumerable Members

        public IEnumerator<NameValueParameter> GetEnumerator() {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return ((IEnumerable)Items).GetEnumerator();
        }

        #endregion IEnumerable Members
    }
}