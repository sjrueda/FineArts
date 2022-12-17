using FineArts.Entities;
using Microsoft.EntityFrameworkCore;

namespace FineArts.Dal
{
    public class Repository
    {
        //Consulta en la tabla Teachers si existe data
        public bool HasTeachers()
        {
            using var Context = new FineArtsContext();

            return Context.Teachers.Any(); //Usamos LINQ
        }
        public List<Teacher> GetTeachers()
        {
            List<Teacher> Result = new();
            using FineArtsContext Context = new FineArtsContext();
            Result = Context.Teachers.ToList();
            return Result;
        }
        public bool IsStudentInClass(int studentId, string @class)
        {
            using FineArtsContext Context = new FineArtsContext();
            Student? student = Context.Students.Where(x => x.Id == studentId).Include(t => t.Teacher).FirstOrDefault();
            return student.Teacher.Class.Equals(@class);
        }
        public bool AddTeachers(Teacher[] teachers)
        {
            bool Result = false;
            using FineArtsContext Context = new FineArtsContext();
            Context.AddRange(teachers);
            int Response = Context.SaveChanges();
            if (Response > 0)
            {
                Result = true;
            }
            return Result;
        }
        public bool AddStudent(Student student)
        {
            bool result = false;
            using FineArtsContext Context = new FineArtsContext();
            Context.Students.Add(student);
            int Response = Context.SaveChanges();
            if (Response > 0)
            {
                result = true;
            }
            return result;
        }
        public List<Student> GetStudentsByTeacherId(int teacherId)
        {
            List<Student> Result = new List<Student>();
            // select * from Students where TeacherId = teacherId
            using FineArtsContext Context = new FineArtsContext();
            //Lambda expresion
            Result = Context.Students.Where(S => S.TeacherId == teacherId).ToList();

            //Query expresion
            //Result = (from S in Context.Students
            // where S.TeacherId == teacherId
            // select S).ToList();
            return Result;
        }
        public bool DeleteStudent(Student student)
        {
            bool result = false;
            using FineArtsContext context = new FineArtsContext();
            context.Students.Remove(student);
            result = context.SaveChanges() > 0;
            return result;
        }
        public bool EditStudent(Student student)
        {
            bool result = false;
            using FineArtsContext context = new FineArtsContext();
            context.Students.Update(student);
            result = context.SaveChanges() > 0;
            return result;
        }
    }
}
