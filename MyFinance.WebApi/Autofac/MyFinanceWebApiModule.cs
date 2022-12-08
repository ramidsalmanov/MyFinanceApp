using Autofac;

namespace MyFinance.WebApi.Autofac;

public class MyFinanceWebApiModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(ThisAssembly)
            .AsSelf()
            .AsImplementedInterfaces()
            .PropertiesAutowired();
        
        builder.RegisterType<HttpContextAccessor>().AsImplementedInterfaces();
    }
}