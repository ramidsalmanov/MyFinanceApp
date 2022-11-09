using Autofac;

namespace MyFinance.Persistence.Configurations;

public class MyFinancePersistenceModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(ThisAssembly).AsSelf().AsImplementedInterfaces().PropertiesAutowired();
    }
}