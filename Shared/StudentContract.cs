using Shared.DTOs.RequestModel;
using Shared.DTOs.ResponseModel;
using System.ServiceModel;

namespace Shared
{
    [ServiceContract]
    public interface IStudentService
    {
        //[OperationContract]
        //void AddStudent(StudentRequest request);

        //[OperationContract]
        //void UpdateStudent(StudentRequest request);

        //[OperationContract]
        //void DeleteStudent(int id);

        //[OperationContract]
        //StudentResponse GetStudent(int id);

        [OperationContract]
        List<StudentResponse> GetStudents();
    }
}
