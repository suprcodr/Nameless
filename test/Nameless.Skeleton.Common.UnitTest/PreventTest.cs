using System;
using Xunit;

namespace Nameless.Skeleton {

    public class PreventTest {

        [Fact]
        public void ParameterNull_Must_Throw_ArgumentNullException_On_Null_Parameter() {
            Assert.Throws<ArgumentNullException>(() => Prevent.ParameterNull(null, "null"));
        }

        [Fact]
        public void ParameterNull_Should_Not_Throw_On_Not_Null_Parameter() {
            bool thrown = false;
            try {
                Prevent.ParameterNull(new object(), "new object()");
            } catch (Exception) {
                thrown = true;
            } finally {
                Assert.False(thrown, "Should not have thrown if parameter was not null.");
            }
        }

        [Fact]
        public void ParameterNullOrWhiteSpace_Must_Thrown_ArgumentNullException_On_Parameter_Null() {
            Assert.Throws<ArgumentNullException>(() => Prevent.ParameterNullOrWhiteSpace(null, "null"));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void ParameterNullOrWhiteSpace_Must_Thrown_ArgumentException_On_Parameter_Empty_Or_WhiteSpace(string data) {
            Assert.Throws<ArgumentException>(() => Prevent.ParameterNullOrWhiteSpace(data, nameof(data)));
        }
    }
}