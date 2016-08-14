using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameOfLife;
using GameOfLife.Controllers;

namespace GameOfLife.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            // TODO: fix
            //ViewResult result = controller.Index() as ViewResult;

            // Assert
            // TODO: fix
            //Assert.IsNotNull(result);
        }
    }
}
