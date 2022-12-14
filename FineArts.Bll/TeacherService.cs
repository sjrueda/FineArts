using FineArts.Dal;
using FineArts.Entities;

namespace FineArts.Bll
{
    public class TeacherService
    {
        public static bool SeedData()
        {
            bool HasTestData = false;
            Repository Repository = new Repository();

            //Repository.HasTeachers();
            if (Repository.HasTeachers() == false)

            {
                Teacher[] Teachers = new Teacher[]
                {
                     new Teacher()
                     {
                         FirstName = "Esther",
                         LastName = "Valle",
                         Class = "3C",
                         Email = "evalle@finearts.com",
                         Students = new List<Student>
                         {
                             new Student()
                             {
                                 FirstName = "Kevin",
                                 LastName = "Liu",
                                 DateOfBirth = new DateTime(2005, 10, 10)
                             },
                             new Student()
                             {
                                 FirstName = "Martin",
                                 LastName = "Weber",
                                 DateOfBirth = new DateTime(2005, 09, 07)
                             },
                             new Student()
                             {
                                 FirstName = "George",
                                 LastName = "Li",
                                 DateOfBirth = new DateTime(2005, 08, 10)
                             },
                             new Student()
                             {
                                 FirstName = "Lissa",
                                 LastName = "Miller",
                                 DateOfBirth = new DateTime(2005, 05, 04)
                             },
                             new Student()
                             {
                                 FirstName = "Run",
                                 LastName = "Run",
                                 DateOfBirth = new DateTime(2005, 10, 23)
                             }
                         }
                     },

                     new Teacher("Belinda", "Newman", "2A", "bnewman@finearts.com", new List<Student>
                     {
                             new Student()
                             {
                                 FirstName = "Vijay",
                                 LastName = "Sundaram",
                                 DateOfBirth = new DateTime(2007, 02, 03)
                             },
                             new Student()
                             {
                                 FirstName = "Erik",
                                 LastName = "Gruber",
                                 DateOfBirth = new DateTime(2007, 05, 26)
                             },
                             new Student()
                             {
                                 FirstName = "Chris",
                                 LastName = "Meyer",
                                 DateOfBirth = new DateTime(2006, 05, 09)
                             },
                             new Student()
                             {
                                 FirstName = "Yuhong",
                                 LastName = "Li",
                                 DateOfBirth = new DateTime(2007, 05, 28)
                             },
                             new Student()
                             {
                                 FirstName = "Yan",
                                 LastName = "Li",
                                 DateOfBirth = new DateTime(2007, 03, 31)
                             }
                     }),
                     new Teacher("David", "Waite", "4B", "dwaite@finearts.com", new List<Student>
                     {                         
                             new Student()
                             {
                                 FirstName = "Sean",
                                 LastName = "Stiwart",
                                 DateOfBirth = new DateTime(2003, 02, 18)
                             },
                             new Student()
                             {
                                 FirstName = "Olinda",
                                 LastName = "Turner",
                                 DateOfBirth = new DateTime(2003, 05, 17)
                             },
                             new Student()
                             {
                                 FirstName = "Jhon",
                                 LastName = "Orton",
                                 DateOfBirth = new DateTime(2002, 08, 10)
                             },
                             new Student()
                             {
                                 FirstName = "Tobin",
                                 LastName = "Nixon",
                                 DateOfBirth = new DateTime(2002, 12, 15)
                             },
                             new Student()
                             {
                                 FirstName = "Daniela",
                                 LastName = "Guinot",
                                 DateOfBirth = new DateTime(2002, 08, 01)
                             }
                     })
                };
                HasTestData = Repository.AddTeachers(Teachers);
            }
            return HasTestData;
        }
        public List<Teacher> GetTeachers()
        {
            List<Teacher> Result = new List<Teacher>();
            Repository Repository = new Repository();
            Result = Repository.GetTeachers();
            return Result;
        }
        public List<Student> GetStudentsByTeacher(int teacherId)
        {
            List<Student> Result = new List<Student>();
            Repository Repository = new Repository();
            Result = Repository.GetStudentsByTeacherId(teacherId);
            return Result;
        }
    }
}