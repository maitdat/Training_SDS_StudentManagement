using System.ComponentModel.DataAnnotations;

namespace Training_SDS_StudentManagement.Model.Base
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }
    }
}
