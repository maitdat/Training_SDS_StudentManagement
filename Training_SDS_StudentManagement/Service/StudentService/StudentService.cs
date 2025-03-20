using Training_SDS_StudentManagement.Data;
using Training_SDS_StudentManagement.DesignPattern.Repository;
using Training_SDS_StudentManagement.Model;

namespace Training_SDS_StudentManagement.Service.StudentService
{
    public class StudentService : IStudentService
    {
        private readonly IGenericRepository<Student> _studentRepository;

        public StudentService(IGenericRepository<Student> studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public List<Student> SortStudentsByName()
        {
            return _studentRepository.GetAll().OrderBy(s => s.Name).ToList();
        }
    }
}
