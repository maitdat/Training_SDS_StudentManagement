using SeverGrpc_NHibernate.Model.Base;

namespace SeverGrpc_NHibernate.Model
{
    public class Class : BaseEntity
    {
        public virtual string Name { get; set; }
        public virtual string Subject { get; set; }
        
        public virtual long TeacherId { get; set; }
        public virtual Teacher? Teacher { get; set; }
        public virtual IList<Student> Students { get; set; } = new List<Student>();
    }
}
