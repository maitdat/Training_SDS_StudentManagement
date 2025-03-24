using Microsoft.Identity.Client;
using SeverGrpc_NHibernate.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeverGrpc_NHibernate.Model
{
    public class Class : BaseEntity
    {
        public virtual string Name { get; set; }
        public virtual string Subject { get; set; }

        [ForeignKey("Teacher")]
        public virtual int TeacherId { get; set; }
        public virtual Teacher? Teacher { get; set; }
        public virtual IList<Student> Students { get; set; } = new List<Student>();
    }
}
