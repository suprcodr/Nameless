using Microsoft.CSharp.RuntimeBinder;
using Xunit;

namespace Nameless.Skeleton.Dynamic {

    public class DynamicHashtableTest {

        [Fact]
        public void Can_Add_New_Member() {
            dynamic hashtable = new DynamicHashtable();

            hashtable.Value = 1;

            Assert.Equal(1, hashtable.Value);
        }

        [Fact]
        public void Retrieve_Value_Not_Setted_Throw_Exception() {
            dynamic hashtable = new DynamicHashtable();

            Assert.Throws<RuntimeBinderException>(() => hashtable.Value);
        }

        [Fact]
        public void Can_Get_Item_By_Index() {
            dynamic hashtable = new DynamicHashtable();

            hashtable.Value = 1;

            Assert.Equal(1, hashtable["Value"]);
        }

        [Fact]
        public void Can_Set_Item_By_Index() {
            dynamic hashtable = new DynamicHashtable();

            hashtable["Value"] = 1;

            Assert.Equal(1, hashtable.Value);
        }
    }
}