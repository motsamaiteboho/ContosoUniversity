using ContosoUniversity.Models;
using System.Linq;

namespace ContosoUniversity.Data
{
    public interface IStudentRepository
    {
        IQueryable<Student> GetAllStudents();
        Student GetStudentById(int id);
        Student GetStudentWithEnrollmentDetails(int id);
        void AddStudent(Student student);
        void UpdateStudent(Student student);
        void DeleteStudent(Student student);
    }
}
