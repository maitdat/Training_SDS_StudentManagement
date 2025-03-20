using System.ComponentModel.DataAnnotations.Schema;
using Training_SDS_StudentManagement.Model.Base;

namespace Training_SDS_StudentManagement.Model
{
    public class Classes : BaseEntity
    {
        public string Name { get; set; }
        public string Subject { get; set; }
        
        public long TeacherId { get; set; }
        public Teacher? Teacher { get; set; }
    }
}
