using Training_SDS_StudentManagement.Model.Base;

namespace Training_SDS_StudentManagement.Model
{
    public class Teacher : BaseEntity
    {
        public string Name { get; set; }
        public DateTime DateOfBirth{ get; set; }
    }
}
