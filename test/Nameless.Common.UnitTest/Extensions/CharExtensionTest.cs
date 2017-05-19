using Xunit;

namespace Nameless {

    public class CharExtensionTest {

        [Fact]
        public void Returns_True_On_Letter() {
            Assert.True(CharExtension.IsLetter('a'));
        }

        [Fact]
        public void Returns_False_On_Digit() {
            Assert.False(CharExtension.IsLetter('1'));
        }

        [Theory]
        [InlineData('\r')] // Carriage return
        [InlineData('\n')] // New line
        [InlineData('\t')] // Tab
        [InlineData('\f')] // Form feed
        [InlineData(' ')] // Space
        public void Can_Detect_Space(char c) {
            Assert.True(CharExtension.IsSpace(c));
        }
    }
}