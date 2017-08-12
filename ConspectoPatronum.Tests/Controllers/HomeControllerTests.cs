using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConspectoPatronum.Controllers;

namespace ConspectoPatronum.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        private HomeController _controller;

        [TestInitialize]
        public void HomeControllerTestsInitialize()
        {
            _controller = new HomeController();
        }

        [TestMethod]
        public void HomeTest()
        {
            var result = _controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }
    }
}
