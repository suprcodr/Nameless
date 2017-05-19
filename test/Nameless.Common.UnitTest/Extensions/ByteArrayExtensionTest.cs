using System.Text;
using Xunit;

namespace Nameless {

    public class ByteArrayExtensionTest {

        [Fact]
        public void Can_Transform_ByteArray_To_HexString() {
            // arrange
            var array = Encoding.UTF8.GetBytes("I'm Batman!");
            var expected = "49276D204261746D616E21";

            // act
            var actual = ByteArrayExtension.ToHexString(array);

            // assert
            Assert.Equal(expected, actual);
        }
    }
}