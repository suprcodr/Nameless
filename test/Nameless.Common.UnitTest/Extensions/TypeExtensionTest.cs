using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Nameless {

    public class TypeExtensionTest {

        [Fact]
        public void IsAssignableFromGenericType() {
            Assert.True(TypeExtension.IsAssignableFromGenericType(typeof(List<>), typeof(IEnumerable<>)));
        }

        [Fact]
        public void Although_Assignable_IsAssignableFromGenericType_Returns_False() {
            Assert.False(TypeExtension.IsAssignableFromGenericType(typeof(IEnumerable), typeof(IEnumerable<>)));
        }

        [Fact]
        public void IsNullable() {
            Assert.True(TypeExtension.IsNullable(typeof(int?)));

            Assert.False(TypeExtension.IsNullable(typeof(int)));
            Assert.False(TypeExtension.IsNullable(typeof(object)));
        }

        [Fact]
        public void AllowNull() {
            Assert.True(TypeExtension.AllowNull(typeof(object)));
            Assert.True(TypeExtension.AllowNull(typeof(int?)));

            Assert.False(TypeExtension.AllowNull(typeof(int)));
        }


    }
}