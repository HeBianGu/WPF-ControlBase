using System;
using HeBianGu.Applications.ControlBase.LinkWindow.Controler;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            TreeListController controller = new TreeListController();

            var uri = controller.TreeList();

            var content = controller.TreeList();
        }
    }
}
