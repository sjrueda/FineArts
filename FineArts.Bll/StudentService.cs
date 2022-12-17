using FineArts.Dal;
using FineArts.Entities;

namespace FineArts.Bll
{
    public class StudentService
    {
        //public bool IsStudentInClass(int studentId, string @class)
        //{
        //    bool result = false;
        //    Repository repository = new();
        //    repository.IsStudentInClass(studentId, @class);
        //    return result;
        //}
        public bool AddStudent(Student student)
        {
            bool result = false;
            Repository repository = new();
            result = repository.AddStudent(student);
            return result;
        }
        public bool DeleteStudent(Student student)
        {
            bool result = false;
            Repository repository = new();
            result = repository.DeleteStudent(student);
            return result;
        }
        public bool EditStudent(Student student)
        {
            bool result = false;
            Repository repository = new();
            result = repository.EditStudent(student);
            return result;
        }
    }
}
