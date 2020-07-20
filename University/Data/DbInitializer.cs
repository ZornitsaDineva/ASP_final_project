using System;
using System.Linq;
using ContosoUniversity.Models;

namespace ContosoUniversity.Data
{
    public static class DbInitializer
    {
        public static void Initialize(SchoolContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Students.Any())
            {
                 return;   // DB has been seeded
            }

            var students = new Student[]
            {
                new Student { FirstName = "Мила",   LastName = "Милева",
                    EnrollmentDate = DateTime.Parse("2010-09-01") },
                new Student { FirstName = "Мария", LastName = "Петрова",
                    EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Student { FirstName = "Пенка",   LastName = "Димова",
                    EnrollmentDate = DateTime.Parse("2013-09-01") },
                new Student { FirstName = "Димитър",    LastName = "Паскалев",
                    EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Student { FirstName = "Жан",      LastName = "Валжан",
                    EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Student { FirstName = "Ния",    LastName = "Желязкова",
                    EnrollmentDate = DateTime.Parse("2011-09-01") },
                new Student { FirstName = "Лаура",    LastName = "Ботева",
                    EnrollmentDate = DateTime.Parse("2013-09-01") },
                new Student { FirstName = "Нино",     LastName = "Пиронков",
                    EnrollmentDate = DateTime.Parse("2025-09-01") }
            };

            foreach (Student s in students)
            {
                context.Students.Add(s);
            }
            context.SaveChanges();

            var instructors = new Instructor[]
            {
                new Instructor { FirstName = "Александър",     LastName = "Добрев",
                    HireDate = DateTime.Parse("1995-03-11") },
                new Instructor { FirstName = "Симеон",    LastName = "Иванов",
                    HireDate = DateTime.Parse("2002-07-06") },
                new Instructor { FirstName = "Крум",   LastName = "Славов",
                    HireDate = DateTime.Parse("1998-07-01") },
                new Instructor { FirstName = "Ния", LastName = "Златанова",
                    HireDate = DateTime.Parse("2001-01-15") },
                new Instructor { FirstName = "Петя",   LastName = "Вълканова",
                    HireDate = DateTime.Parse("2004-02-12") }
            };

            foreach (Instructor i in instructors)
            {
                context.Instructors.Add(i);
            }
            context.SaveChanges();

            var departments = new Department[]
            {
                new Department { Name = "Макро икономика",     Budget = 350000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorID  = instructors.Single( i => i.LastName == "Славов").ID },
                new Department { Name = "Микро икономика", Budget = 100000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorID  = instructors.Single( i => i.LastName == "Иванов").ID },
                new Department { Name = "Информатика", Budget = 350000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorID  = instructors.Single( i => i.LastName == "Добрев").ID },
                new Department { Name = "Екология",   Budget = 100000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorID  = instructors.Single( i => i.LastName == "Златанова").ID }
            };

            foreach (Department d in departments)
            {
                context.Departments.Add(d);
            }
            context.SaveChanges();

            var courses = new Course[]
            {
                new Course {CourseID = 1050, Title = "Фондови пазари",      Credits = 3,
                    DepartmentID = departments.Single( s => s.Name == "Макро икономика").DepartmentID
                },
                new Course {CourseID = 4022, Title = "Производителност на труда", Credits = 3,
                    DepartmentID = departments.Single( s => s.Name == "Микро икономика").DepartmentID
                },
                new Course {CourseID = 4041, Title = "Програмиране", Credits = 3,
                    DepartmentID = departments.Single( s => s.Name == "Информатика").DepartmentID
                },
                new Course {CourseID = 1045, Title = "Връзки с обществеността",       Credits = 4,
                    DepartmentID = departments.Single( s => s.Name == "Микро икономика").DepartmentID
                },
                new Course {CourseID = 3141, Title = "Работа в екип",   Credits = 4,
                    DepartmentID = departments.Single( s => s.Name == "Информатика").DepartmentID
                },
                new Course {CourseID = 2021, Title = "Опазване на околната среда",    Credits = 3,
                    DepartmentID = departments.Single( s => s.Name == "Екология").DepartmentID
                },
                new Course {CourseID = 2042, Title = "Емоционална интелегентност",     Credits = 4,
                    DepartmentID = departments.Single( s => s.Name == "Информатика").DepartmentID
                },
            };

            foreach (Course c in courses)
            {
                context.Courses.Add(c);
            }
            context.SaveChanges();

            var officeAssignments = new OfficeAssignment[]
            {
                new OfficeAssignment {
                    InstructorID = instructors.Single( i => i.LastName == "Златанова").ID,
                    Location = "Стара Загора" },
                new OfficeAssignment {
                    InstructorID = instructors.Single( i => i.LastName == "Иванов").ID,
                    Location = "Нова Згора" },
                new OfficeAssignment {
                    InstructorID = instructors.Single( i => i.LastName == "Добрев").ID,
                    Location = "София" },
            };

            foreach (OfficeAssignment o in officeAssignments)
            {
                context.OfficeAssignments.Add(o);
            }
            
            context.SaveChanges();

            var courseInstructors = new CourseAssignment[]
            {
                new CourseAssignment {
                    CourseID = courses.Single(c => c.Title == "Фондови пазари" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Златанова").ID
                    },
                new CourseAssignment {
                    CourseID = courses.Single(c => c.Title == "Производителност на труда" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Иванов").ID
                    },
                new CourseAssignment {
                    CourseID = courses.Single(c => c.Title == "Програмиране" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Добрев").ID
                    },
                new CourseAssignment {
                    CourseID = courses.Single(c => c.Title == "Опазване на околната среда" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Вълканова").ID
                    },
                new CourseAssignment {
                    CourseID = courses.Single(c => c.Title == "Връзки с обществеността" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Славов").ID
                    },
                new CourseAssignment {
                    CourseID = courses.Single(c => c.Title == "Работа в екип" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Вълканова").ID
                    },
                new CourseAssignment {
                    CourseID = courses.Single(c => c.Title == "Емоционална интелегентност" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Славов").ID
                    }
            };

            foreach (CourseAssignment ci in courseInstructors)
            {
                context.CourseAssignments.Add(ci);
            }
            context.SaveChanges();

            var enrollments = new Enrollment[]
            {
                new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Димова").ID,
                    CourseID = courses.Single(c => c.Title == "Фондови пазари" ).CourseID,
                    Grade = Grade.A
                },
                    new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Милева").ID,
                    CourseID = courses.Single(c => c.Title == "Производителност на труда" ).CourseID,
                    Grade = Grade.C
                    },
                    new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Валжан").ID,
                    CourseID = courses.Single(c => c.Title == "Работа в екип" ).CourseID,
                    Grade = Grade.B
                    },
                    new Enrollment {
                        StudentID = students.Single(s => s.LastName == "Петрова").ID,
                    CourseID = courses.Single(c => c.Title == "Програмиране" ).CourseID,
                    Grade = Grade.B
                    },
                    new Enrollment {
                        StudentID = students.Single(s => s.LastName == "Паскалев").ID,
                    CourseID = courses.Single(c => c.Title == "Опазване на околната среда" ).CourseID,
                    Grade = Grade.B
                    },
                    new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Пиронков").ID,
                    CourseID = courses.Single(c => c.Title == "Връзки с обществеността" ).CourseID,
                    Grade = Grade.B
                    },
                    new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Петрова").ID,
                    CourseID = courses.Single(c => c.Title == "Фондови пазари" ).CourseID
                    },
                    new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Ботева").ID,
                    CourseID = courses.Single(c => c.Title == "Фондови пазари").CourseID,
                    Grade = Grade.B
                    },
                new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Димова").ID,
                    CourseID = courses.Single(c => c.Title == "Програмиране").CourseID,
                    Grade = Grade.B
                    },
                    new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Петрова").ID,
                    CourseID = courses.Single(c => c.Title == "Връзки с обществеността").CourseID,
                    Grade = Grade.B
                    },
                    new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Димова").ID,
                    CourseID = courses.Single(c => c.Title == "Работа в екип").CourseID,
                    Grade = Grade.B
                    }
            };

            foreach (Enrollment e in enrollments)
            {
                var enrollmentInDataBase = context.Enrollments.Where(
                    s =>
                            s.Student.ID == e.StudentID &&
                            s.Course.CourseID == e.CourseID).SingleOrDefault();
                if (enrollmentInDataBase == null)
                {
                    context.Enrollments.Add(e);
                }
            }
            context.SaveChanges();
        }
    }
}