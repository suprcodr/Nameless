using System;
using System.Linq;
using System.Reflection;
using Nameless.Skeleton.Framework.Properties;

namespace Nameless.Skeleton.Framework.Data.Generic {

    /// <summary>
    /// Attributes that defines if the property or field will be an entity ID.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class IDAttribute : Attribute {

        #region Private Static Read-Only Fields

        private static readonly string[] Conventions = { "_id", "id", "iD", "Id", "ID" };

        #endregion Private Static Read-Only Fields

        #region Public Static Methods

        /// <summary>
        /// Extract the ID from the object passed as parameter.
        /// </summary>
        /// <param name="obj">The instance of the object.</param>
        /// <returns>The eligible ID from the object.</returns>
        /// <remarks>
        /// Eligible ID's are those members who are instance and public or non-public (private, protected, internal)
        /// properties or fields. Includes readonly properties or fields.
        /// </remarks>
        public static object GetID(object obj) {
            var members = obj.GetType()
                .GetTypeInfo()
                .GetMembers(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(member =>
                    (member.MemberType == MemberTypes.Field || (member.MemberType == MemberTypes.Property && ((PropertyInfo)member).CanRead)) &&
                    (member.GetCustomAttribute<IDAttribute>(inherit: true) != null || Conventions.Any(convention => member.Name == convention)))
                .OrderByDescending(member =>
                    member.GetCustomAttribute<IDAttribute>(inherit: true) != null)
                .ToArray();

            // Ok, no eligible members, sorry, not doughnuts for you.
            if (members.Length == 0) {
                throw new InvalidOperationException(string.Format(Resources.IDMemberNotFoundMessageFormatter, string.Join(", ", Conventions)));
            }

            return members[0].MemberType == MemberTypes.Property
                ? ((PropertyInfo)members[0]).GetValue(obj, null)
                : ((FieldInfo)members[0]).GetValue(obj);
        }

        #endregion Public Static Methods
    }
}