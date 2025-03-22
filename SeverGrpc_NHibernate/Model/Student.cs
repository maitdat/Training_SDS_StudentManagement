using SeverGrpc_NHibernate.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeverGrpc_NHibernate.Model
{
    public class Student : BaseEntity
    {
        public virtual string Name { get; set; }
        public virtual DateTime DateOfBirth { get; set; }
        public virtual string Address { get; set; }
        [Column("ClassId")]
        public virtual Class? Class { get; set; }

    }
}
