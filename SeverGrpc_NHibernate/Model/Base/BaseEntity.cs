using System.ComponentModel.DataAnnotations;

namespace SeverGrpc_NHibernate.Model.Base
{
    public class BaseEntity
    {
        [Key]
        public virtual int Id { get; set; }
    }
}
