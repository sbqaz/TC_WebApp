using System.Net;
using System.Web.Http.Results;
using API.Controllers;
using API.Models;
using NUnit.Framework;
using WebLib.DependencyInjection;
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
                var controller = new CaseController(new TestAppContext());

                var item = GetDemoDTOProduct();

                var result =
                    controller.PostCase(item) as CreatedAtRouteNegotiatedContentResult<CaseDTO>;

                Assert.IsNotNull(result);
                Assert.AreEqual(result.RouteName, "DefaultApi");
                Assert.AreEqual(result.RouteValues["id"], result.Content.Id);
                Assert.AreEqual(result.Content.Worker, item.Worker);
            }

            [Test]
            public void PutProduct_ShouldReturnStatusCode()
            {
                var controller = new CaseController(new TestAppContext());

                var item = GetDemoProduct();

                var result = controller.PutCase(item.Id, item) as StatusCodeResult;
                Assert.IsNotNull(result);
                Assert.IsInstanceOf(typeof(StatusCodeResult), result);
                Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
            }

            [Test]
            public void PutProduct_ShouldFail_WhenDifferentID()
            {
                var controller = new CaseController(new TestAppContext());

                var badresult = controller.PutCase(999, GetDemoProduct());
                Assert.IsInstanceOf(typeof(BadRequestResult), badresult);
            }

            [Test]
            public void GetProduct_ShouldReturnProductWithSameID()
            {
                var context = new TestAppContext();
                context.Cases.Add(GetDemoProduct());

                var controller = new CaseController(context);
                var result = controller.GetCase(3) as OkNegotiatedContentResult<Case>;

                Assert.IsNotNull(result);
                Assert.AreEqual(3, result.Content.Id);
            }

            [Test]
            public void GetProducts_ShouldReturnAllProducts()
            {
                var context = new TestAppContext();
                context.Cases.Add(new Case { Id = 1, InstallationId = new Installation {Id =1 } });
                context.Cases.Add(new Case { Id = 2, InstallationId = new Installation { Id = 2 } });
                context.Cases.Add(new Case { Id = 3, InstallationId = new Installation { Id = 3} });

                var controller = new CaseController(context);
                var result = controller.GetCases() as TestCaseDbSet;

                Assert.IsNotNull(result);
                Assert.AreEqual(3, result.Local.Count);
            }

            Case GetDemoProduct()
            {
                return new Case { Id = 3, Worker = "Demo name", Observer = Case.ObserverSelection.own };
            }

            CaseDTO GetDemoDTOProduct()
            {
                return new CaseDTO {Id = 1, InstallationId = 2};
            }
        }
    }
}