using Autofac;

namespace MyFinance.Persistence.Autofac;

public class MyFinancePersistenceModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(ThisAssembly).AsSelf().AsImplementedInterfaces();
    }
}