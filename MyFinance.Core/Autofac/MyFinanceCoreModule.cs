using Autofac;

namespace MyFinance.Core.Autofac;

public class MyFinanceCoreModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(ThisAssembly).AsSelf().AsImplementedInterfaces().PropertiesAutowired();
    }
}