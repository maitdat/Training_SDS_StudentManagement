using SeverGrpc_NHibernate.Model.Base;

namespace SeverGrpc_NHibernate.Model
{
    public class Teacher : BaseEntity
    {
        public virtual string Name { get; set; }
        public virtual DateTime DateOfBirth { get; set; }
        public virtual IList<Class> Classes { get; set; } = new List<Class>();
    }
}
