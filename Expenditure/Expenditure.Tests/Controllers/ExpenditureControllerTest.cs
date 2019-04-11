using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Expenditure.Models;
using Expenditure.Controllers;
using System.Web.Mvc;
using System.Collections.Generic;

using System.Linq;
using System.Text;
namespace Expenditure.Tests.Controllers
{
    [TestClass]
    public class ExpenditureControllerTest
    {
        [TestMethod]
        public void TestIndex()
        {
            var db = new ExpenditureEntities();
            var controller = new ExpenditureController();

            var result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(List<Expend>));
            Assert.AreEqual(db.Expends.Count(),
                (result.Model as List<Expend>).Count);
        }
    }
}
