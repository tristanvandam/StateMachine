using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Autofac;
using Autofac.Core;
using StateMachine.Core;
using StateMachine.ExampleTestApp.Logger;

namespace StateMachine.ExampleTestApp.Configuration
{
    public static class DependencyInjection
    {

        public static IContainer ConfigureDependencies(ContainerBuilder containerBuilder = null)
        {
            //Let's start off with the current latch being home

            var internalContainerBuilder = containerBuilder?? new ContainerBuilder();
            var cancellationTokenSource = new CancellationTokenSource();


            var homeLatch = new Latch(new Binary2BitState(Guid.NewGuid(), 0, 0), "Home - location");

            var workLatch = new Latch(new Binary2BitState(Guid.NewGuid(), 0, 0), "Work - location");
            workLatch.OnExit = async (state, o) =>
            {
                //Eg. Make api call etc... 
                Console.WriteLine("Think about going to gym on the way home...");
            };

            var bedLatch = new Latch(new Binary2BitState(Guid.NewGuid(), 0, 0), "Bed - location");
            bedLatch.OnEnter = async (state, o) =>
            {
                //Eg. Make api call etc... 
                Console.WriteLine("Alarm set for 8am");
            };

            //TODO - Setup Action Factory
            workLatch.AddActions(new Dictionary<Actions, Func<Latch>>()
            {
                [TransportActions.TakeTrain] = () => homeLatch
            });

            homeLatch.AddActions(new Dictionary<Actions, Func<Latch>>()
            {
                [TransportActions.TakeTrain] = () => workLatch,
                [TransportActions.FallAsleep] = () => bedLatch
            });

            bedLatch.AddActions(new Dictionary<Actions, Func<Latch>>()
            {
                [TransportActions.WakeUp] = () => homeLatch
            });




            containerBuilder.Register<FiniteStateMachine>(context =>
            {
                var logger = context.Resolve<ILogger>();
                return new FiniteStateMachine(homeLatch, logger.Log);
            }).As<IFiniteStateMachine>();

            containerBuilder.Register<Program>(context =>
            {
                var finiteStateMachine = context.Resolve<IFiniteStateMachine>();
                var logger = context.Resolve<ILogger>();
                return new Program(finiteStateMachine,logger,cancellationTokenSource);
            }).As<IProgram>();


            return internalContainerBuilder.Build();
        }

    }
}
