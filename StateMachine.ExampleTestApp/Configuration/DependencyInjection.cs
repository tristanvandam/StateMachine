using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Autofac;
using Autofac.Core;
using StateMachine.Core;
using StateMachine.Core.StateManagement;
using StateMachine.Core.StateManagement.States;
using StateMachine.ExampleTestApp.Logger;

namespace StateMachine.ExampleTestApp.Configuration
{
    public static class DependencyInjection
    {

        public static IContainer ConfigureDependencies(ContainerBuilder containerBuilder = null)
        {
            //Let's start off with the current latch being home

            //containerBuilder = containerBuilder?? new ContainerBuilder();
            containerBuilder ??= new ContainerBuilder();
            var cancellationTokenSource = new CancellationTokenSource();

            var nameHome = "home";
            var nameBed = "bed";
            var nameWork = "work";
            var someOtherState = new Binary2BitState(Guid.NewGuid(), 0, 0);


            containerBuilder.RegisterNamedLatch(BinaryState.One, nameWork, new Dictionary<Actions, string>() { [TransportActions.TakeTrain] = nameHome } );
            containerBuilder.RegisterNamedLatch(BinaryState.Zero,"home",new Dictionary<Actions, string>() { [TransportActions.TakeTrain] = nameWork, [TransportActions.FallAsleep] = nameBed } );
            containerBuilder.RegisterNamedLatch(someOtherState, nameBed,new Dictionary<Actions, string>() { [TransportActions.WakeUp] = nameHome } );



            //containerBuilder.Register((cc) =>
            //{
            //    var dictionary = new Dictionary<Actions, Func<ILatch>>()
            //    {
            //        [TransportActions.TakeTrain] = () => cc.ResolveKeyed<ILatch>("home")
            //    };

            //    var workLatch = new Latch(BinaryState.One, dictionary, "Home - location");
            //    return workLatch;
            //}).Keyed<ILatch>("work");

            //var homeLatch = new Latch(new Binary2BitState(Guid.NewGuid(), 0, 0), "Home - location");



            //var workLatch = new Latch(new Binary2BitState(Guid.NewGuid(), 0, 0), "Work - location");
   

            //var bedLatch = new Latch(new Binary2BitState(Guid.NewGuid(), 0, 0), "Bed - location");
            //bedLatch.OnEnter = async (state, o) =>
            //{
            //    //Eg. Make api call etc... 
            //    Console.WriteLine("Alarm set for 8am");
            //};

            ////TODO - Setup Action Factory
            //workLatch.AddActions(new Dictionary<Actions, Func<string, Latch>>()
            //{
            //    [TransportActions.TakeTrain] = (s) => homeLatch
            //});



            //homeLatch.AddActions(new Dictionary<Actions, Func<string, Latch>>()
            //{
            //    [TransportActions.TakeTrain] = (s) => workLatch,
            //    [TransportActions.FallAsleep] = (s) => bedLatch
            //});

            //bedLatch.AddActions(new Dictionary<Actions, Func<string, Latch>>()
            //{
            //    [TransportActions.WakeUp] = (s) => homeLatch
            //});


            containerBuilder.RegisterType<ConsoleLogger>().As<ILogger>();

            containerBuilder.Register<FiniteStateMachine>(context =>
            {
                var logger = context.Resolve<ILogger>();
                var initialLatch = context.ResolveKeyed<ILatch>(nameHome) as Latch;
                return new FiniteStateMachine(initialLatch, logger.Log);
            }).As<IFiniteStateMachine>();

            containerBuilder.Register<Program>(context =>
            {
                var finiteStateMachine = context.Resolve<IFiniteStateMachine>();
                var logger = context.Resolve<ILogger>();
                return new Program(finiteStateMachine,logger,cancellationTokenSource);
            }).As<IProgram>();


            return containerBuilder.Build();
        }

        private static void RegisterNamedLatch(this ContainerBuilder builder, State state, string name, IEnumerable<KeyValuePair<Actions, string>> latchActions, bool doNothingOnReEnter = false)
        {
            builder.Register((cc) =>
            {
                var dictionary = new Dictionary<Actions, Func<ILatch>>();
                foreach (var (action, latchToResolveName) in latchActions)
                {
                    dictionary.Add(action, () => cc.ResolveKeyed<ILatch>(latchToResolveName));
                }

                var newLatch = new Latch(state, dictionary, name, doNothingOnReEnter);
                return newLatch;
            }).Keyed<ILatch>(name);
        }
    }
}
