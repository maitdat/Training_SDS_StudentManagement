using FluentNHibernate.Mapping;
using SeverGrpc_NHibernate.Model;

namespace SeverGrpc_NHibernate.Mapping
{ 
    public class StudentMap : ClassMap<Student>
    {
        public StudentMap() 
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Name).Not.Nullable();
            Map(x => x.DateOfBirth).Not.Nullable();
            Map(x => x.Address).Not.Nullable();
            References(x => x.Class).Not.Nullable();
        }
    }
}
