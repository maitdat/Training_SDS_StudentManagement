using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace SeverGrpc_NHibernate.NHibernateHelper
{
    public class SessionFactoryBuilder
    {
        public static ISessionFactory CreateSessionFactory()
        {
            var connectionString = "Data Source=ADMIN\\IMDATSERVER;Initial Catalog=StudentManagementGrpc;Integrated Security=True; TrustServerCertificate=True";
            return Fluently.Configure()
              .Database(
                MsSqlConfiguration.MsSql2012
                .ConnectionString(connectionString)
              )
              .Mappings(m =>
                m.FluentMappings.AddFromAssemblyOf<Program>())
              .BuildSessionFactory();
        }

    }

}

