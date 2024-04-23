using Autofac;
using CareerMate.Infrastructure.Persistence;
using CareerMate.Infrastructure.Persistence.Repositories.SysAdmins;

namespace CareerMate.API.AutofacModules
{
    public class PersistenceModules : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(SysAdminRepository).Assembly).AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterType<AppDbContext>().AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}
