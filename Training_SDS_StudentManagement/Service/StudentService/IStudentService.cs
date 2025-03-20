using Training_SDS_StudentManagement.Model;

namespace Training_SDS_StudentManagement.Service.StudentService
{
    public interface IStudentService
    {
        List<Student> SortStudentsByName();
    }
}
