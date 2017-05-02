using Xunit;

namespace Nameless.Skeleton.Extensions {

    public class EnumExtensionTest {

        public enum TestEnum {

            [System.ComponentModel.Description("1")]
            One,

            [System.ComponentModel.Description("2")]
            Two,

            [System.ComponentModel.Description("3")]
            Three
        }

        [Fact]
        public void Get_Description_Attribute_From_Enum() {
            // arrange
            var e = TestEnum.Two;

            // act
            var description = EnumExtension.GetDescription(e);

            // assert
            Assert.NotNull(description);
            Assert.Equal("2", description);
        }
    }
}