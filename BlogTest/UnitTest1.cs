using Microsoft.VisualStudio.TestTools.UnitTesting;
using Narojay.Blog.Configs;

namespace BlogTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            new PositionOption().Configure<Test>(x => x.X = "1");
        }
    }
}