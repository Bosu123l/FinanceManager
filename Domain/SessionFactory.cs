using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace Domain
{
    public class SessionProvider
    {
        private readonly string _connectionString;
        private static ISessionFactory _sessionFactory;

        public ISessionFactory SessionFactory
        {
            get { return _sessionFactory ?? (_sessionFactory = CreateSessionFactory()); }
        }

        public SessionProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        private ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(
                        MsSqlConfiguration.MsSql2008.ConnectionString(_connectionString).UseReflectionOptimizer().AdoNetBatchSize(1000)).CurrentSessionContext("thread_static")
                .ExposeConfiguration(x =>
                        {
                            // Increase the timeout for long running queries
                            x.SetProperty("command_timeout", "600");

                            // Allows you to have non-virtual and non-public methods in your entities
                            x.SetProperty("use_proxy_validator", "false");
                        })
                .Mappings(m => m.FluentMappings.AddFromAssembly(System.Reflection.Assembly.GetExecutingAssembly()))
                .BuildSessionFactory();
        }
    }
}