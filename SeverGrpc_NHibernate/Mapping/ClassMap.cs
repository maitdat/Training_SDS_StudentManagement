using FluentNHibernate.Mapping;
using SeverGrpc_NHibernate.Model;

namespace SeverGrpc_NHibernate.Mapping
{
    public class ClassMap : ClassMap<Class>
    {
        public ClassMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Name).Not.Nullable();
            Map(x => x.Subject).Not.Nullable();
            References(x => x.Teacher,"TeacherId").Nullable();
            HasMany(x => x.Students).Cascade.All().Inverse();
        }
    }
}
