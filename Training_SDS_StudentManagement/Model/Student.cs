using System.ComponentModel.DataAnnotations.Schema;
using Training_SDS_StudentManagement.Model.Base;

namespace Training_SDS_StudentManagement.Model
{
    public class Student : BaseEntity
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        [Column("ClassId")]
        public virtual Classes? Classes { get; set; }

    }
}
