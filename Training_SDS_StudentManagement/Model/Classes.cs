using System.ComponentModel.DataAnnotations.Schema;
using Training_SDS_StudentManagement.Model.Base;

namespace Training_SDS_StudentManagement.Model
{
    public class Classes : BaseEntity
    {
        public string Name { get; set; }
        public string Subject { get; set; }
        [Column("TeacherId")]
        public Teacher Teacher { get; set; }
    }
}
