using System.Net;
using System.Web.Http.Results;
using API.Controllers;
using NUnit.Framework;
using WebLib.AppCentext;
using WebLib.DbSet;
using WebLib.Models;

namespace API.Tests.Unit.Controllers
{
    public class TestCaseController
    {
        [TestFixture]
        public class TestProductController
        {
            [Test]
            public void PostProduct_ShouldReturnSameProduct()
            {
                var controller = new CaseController(new TestCaseAppContext());

                var item = GetDemoProduct();

                var result =
                    controller.PostCase(item) as CreatedAtRouteNegotiatedContentResult<Case>;

                Assert.IsNotNull(result);
                Assert.AreEqual(result.RouteName, "DefaultApi");
                Assert.AreEqual(result.RouteValues["id"], result.Content.Id);
                Assert.AreEqual(result.Content.Worker, item.Worker);
            }

            [Test]
            public void PutProduct_ShouldReturnStatusCode()
            {
                var controller = new CaseController(new TestCaseAppContext());

                var item = GetDemoProduct();

                var result = controller.PutCase(item.Id, item) as StatusCodeResult;
                Assert.IsNotNull(result);
                Assert.IsInstanceOf(typeof(StatusCodeResult), result);
                Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
            }

            [Test]
            public void PutProduct_ShouldFail_WhenDifferentID()
            {
                var controller = new CaseController(new TestCaseAppContext());

                var badresult = controller.PutCase(999, GetDemoProduct());
                Assert.IsInstanceOf(typeof(BadRequestResult), badresult);
            }

            [Test]
            public void GetProduct_ShouldReturnProductWithSameID()
            {
                var context = new TestCaseAppContext();
                context.Cases.Add(GetDemoProduct());

                var controller = new CaseController(context);
                var result = controller.GetCase(3) as OkNegotiatedContentResult<Case>;

                Assert.IsNotNull(result);
                Assert.AreEqual(3, result.Content.Id);
            }

            [Test]
            public void GetProducts_ShouldReturnAllProducts()
            {
                var context = new TestCaseAppContext();
                context.Cases.Add(new Case { Id = 1, Worker = "Demo1", Observer = 20 });
                context.Cases.Add(new Case { Id = 2, Worker = "Demo2", Observer = 30 });
                context.Cases.Add(new Case { Id = 3, Worker = "Demo3", Observer = 40 });

                var controller = new CaseController(context);
                var result = controller.GetCases() as TestCaseDbSet;

                Assert.IsNotNull(result);
                Assert.AreEqual(3, result.Local.Count);
            }

            [Test]
            public void DeleteProduct_ShouldReturnOK()
            {
                var context = new TestCaseAppContext();
                var item = GetDemoProduct();
                context.Cases.Add(item);

                var controller = new CaseController(context);
                var result = controller.DeleteCase(3) as OkNegotiatedContentResult<Case>;

                Assert.IsNotNull(result);
                Assert.AreEqual(item.Id, result.Content.Id);
            }

            Case GetDemoProduct()
            {
                return new Case() { Id = 3, Worker = "Demo name", Observer = 5 };
            }
        }
    }
}