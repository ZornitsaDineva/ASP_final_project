using ContosoUniversity.Controllers;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using ContosoUniversity.Models.SchoolViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace TestUniversity
{
    public class InstructorsControllerTests
    {
        private SchoolContext _context;

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
        public async Task IndexTestAsync()
        {
            InstructorsController obj = new InstructorsController(_context);

            var actResult = await obj.Index(null, null) as Microsoft.AspNetCore.Mvc.ViewResult;
            var model = actResult.Model as InstructorIndexData;
            var instructors = model.Instructors as IEnumerable;

            Assert.That(instructors.Cast<object>().Count(), Is.EqualTo(5));
            Assert.That(actResult.ViewName, Is.EqualTo(null));

            IEnumerator enumerator = instructors.GetEnumerator();
            enumerator.MoveNext();
            Instructor instructor = (Instructor)enumerator.Current;
            CourseAssignment ca = instructor.CourseAssignments.First();
            Course course = ca.Course;
            Department department = course.Department;

            Assert.That(course, Is.Not.Null);
            Assert.That(department, Is.Not.Null);
        }

        [Test]
        public async Task IndexInstructorTest()
        {
            InstructorsController obj = new InstructorsController(_context);

            var actResult = await obj.Index(13, null) as Microsoft.AspNetCore.Mvc.ViewResult;
            var model = actResult.Model as InstructorIndexData;
            var courses = model.Courses;

            Assert.That(courses.Cast<object>().Count(), Is.GreaterThan(0));
            Assert.That(courses.Cast<object>().Count(), Is.EqualTo(2));
            Assert.That(actResult.ViewName, Is.EqualTo(null));
        }
    }
}