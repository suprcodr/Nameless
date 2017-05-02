using System;
using System.Collections;
using System.Linq;
using Xunit;

namespace Nameless.Skeleton {

    public class EnumerableExtensionTest {

        [Fact]
        public void Can_Iterate_Througth_Array() {
            var array = new[] { 1, 2, 3, 4, 5 };
            var expected = array.Sum();
            int actual = 0;
            EnumerableExtension.Each(array, _ => {
                actual += _;
            });
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Null_Source_Does_Not_Affect_Execution() {
            var ex = Record.Exception(() => EnumerableExtension.Each(null, _ => { }));

            Assert.Null(ex);
        }

        [Fact]
        public void Null_Action_Throws_Exception() {
            Assert.Throws<ArgumentNullException>(() => EnumerableExtension.Each(new[] { 1 }, (Action<int>)null));
        }

        [Fact]
        public void Can_Distinct_By_Property() {
            var array = new[] {
                new { ID = 1, Name = "Test 1" },
                new { ID = 2, Name = "Test 2" },
                new { ID = 3, Name = "Test 2" },
                new { ID = 4, Name = "Test 4" },
                new { ID = 5, Name = "Test 4" },
                new { ID = 6, Name = "Test 6" }
            };

            var actual = EnumerableExtension.DistinctBy(array, _ => _.Name);

            Assert.NotEmpty(actual);
            Assert.Equal("Test 1", actual.ElementAt(0).Name);
            Assert.Equal("Test 2", actual.ElementAt(1).Name);
            Assert.Equal("Test 4", actual.ElementAt(2).Name);
            Assert.Equal("Test 6", actual.ElementAt(3).Name);
        }

        [Fact]
        public void Can_Order_By_Property_Name() {
            var array = new[] {
                new { ID = 2, Name = "Test 2" },
                new { ID = 1, Name = "Test 1" },
                new { ID = 4, Name = "Test 4" },
                new { ID = 3, Name = "Test 2" },
                new { ID = 6, Name = "Test 6" },
                new { ID = 5, Name = "Test 4" }
            };

            var actual = EnumerableExtension.OrderBy(array, "ID");

            Assert.NotEmpty(actual);
            Assert.Equal(new[] { 1, 2, 3, 4, 5, 6 }, actual.Select(_ => _.ID));
        }

        [Fact]
        public void Can_Order_By_Descending_Property_Name() {
            var array = new[] {
                new { ID = 2, Name = "Test 2" },
                new { ID = 1, Name = "Test 1" },
                new { ID = 4, Name = "Test 4" },
                new { ID = 3, Name = "Test 2" },
                new { ID = 6, Name = "Test 6" },
                new { ID = 5, Name = "Test 4" }
            };

            var actual = EnumerableExtension.OrderByDescending(array, "ID");

            Assert.NotEmpty(actual);
            Assert.Equal(new[] { 6, 5, 4, 3, 2, 1 }, actual.Select(_ => _.ID));
        }

        [Theory]
        [InlineData(data: (IEnumerable)null)]
        [InlineData(data: new object[] { new object[] { } } )]
        public void Can_Tell_If_Collection_Is_Null_Or_Empty(IEnumerable data) {
            // arrange, act, assert
            Assert.True(EnumerableExtension.IsNullOrEmpty(data));
        }
    }
}