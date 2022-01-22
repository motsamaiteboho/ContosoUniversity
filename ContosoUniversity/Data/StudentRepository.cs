using ContosoUniversity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace ContosoUniversity.Data
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _appDbContext;

        public StudentRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void AddStudent(Student student)
        {
            _appDbContext.Add(student);
            _appDbContext.SaveChanges();
        }

        public void DeleteStudent(Student student)
        {
            _appDbContext.Remove(student);
            _appDbContext.SaveChanges();
        }

        public IQueryable<Student> GetAllStudents()
        {
            return _appDbContext.Students;
        }

        public Student GetStudentById(int id)
        {
            return _appDbContext.Students.FirstOrDefault(p => p.StudentID == id);
        }

        public Student GetStudentWithEnrollmentDetails(int id)
        {
            return _appDbContext.Students
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
                .FirstOrDefault(s => s.StudentID == id);
        }

        public void UpdateStudent(Student student)
        {
            _appDbContext.Update(student);
        }
    }
}
