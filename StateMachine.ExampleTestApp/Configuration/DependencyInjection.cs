using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Autofac.Core;

namespace StateMachine.ExampleTestApp.Configuration
{
    public static class DependencyInjection
    {

        public static IContainer ConfigureDependencies(ContainerBuilder containerBuilder = null)
        {
            var internalContainerBuilder = containerBuilder?? new ContainerBuilder();




            return internalContainerBuilder.Build();
        }

    }
}
