using System;
using Xunit;

namespace Nameless {

    public class DateTimeExtensionTest {

        [Fact]
        public void Returns_Years_From_Dates() {
            var initial = new DateTime(2000, 1, 1);
            var final = new DateTime(2010, 1, 1);
            var years = 10;

            Assert.Equal(years, DateTimeExtension.GetYears(initial, final));
        }

        [Fact]
        public void Returns_Years_From_Today() {
            var initial = new DateTime(2000, 1, 1);
            var final = DateTime.Today;
            var expected = final.Year - initial.Year;

            Assert.InRange(DateTimeExtension.GetYears(initial, final), expected -= 1, expected += 1);
        }
    }
}