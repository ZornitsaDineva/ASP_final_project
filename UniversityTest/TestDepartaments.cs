
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

using System.Threading.Tasks;

namespace ContosoUniversity.Controllers
{
    class TestDepartaments
    {
        public class DepartmentsControllerTests
        {
            private SchoolContext _context;
            private readonly object budget;

            [SetUp]
            public void Setup()
            {
                var options = new DbContextOptionsBuilder<SchoolContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

                _context = new SchoolContext(options);
                DbInitializer.Initialize(_context);
            }

            [Test]
            public async Task DetailsTestAsync()
            {
                DepartmentsController controller = new DepartmentsController(_context);

                var actResult = await controller.Details(null);
                Assert.IsTrue(typeof(NotFoundResult).IsInstanceOfType(actResult));
                Assert.That(actResult, Is.AssignableFrom<NotFoundResult>());

                actResult = await controller.Details(2687658);
                Assert.IsTrue(typeof(NotFoundResult).IsInstanceOfType(actResult));
                Assert.That(actResult, Is.AssignableFrom<NotFoundResult>());

                actResult = await controller.Details(2);
                Assert.IsTrue(typeof(ViewResult).IsInstanceOfType(actResult));
                Assert.That(actResult, Is.AssignableFrom<ViewResult>());

                ViewResult viewResult = actResult as ViewResult;
                Department department = (Department)viewResult.Model;
                decimal budget = department.Budget;
                Assert.That(budget, Is.Not.Null);
            }
           

        }
    }
}

