using FluentNHibernate.Mapping;
using SeverGrpc_NHibernate.Model;

namespace SeverGrpc_NHibernate.Mapping
{
    public class TeacherMap : ClassMap<Teacher>
    {
        public TeacherMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Name).Not.Nullable();
            Map(x => x.DateOfBirth).Not.Nullable();
            HasMany(x => x.Classes).Cascade.All().Inverse();
        }
    }
}
