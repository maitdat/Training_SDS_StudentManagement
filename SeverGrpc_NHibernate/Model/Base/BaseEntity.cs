using System.ComponentModel.DataAnnotations;

namespace SeverGrpc_NHibernate.Model.Base
{
    public class BaseEntity
    {
        [Key]
        public virtual long Id { get; set; }
    }
}
