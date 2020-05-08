using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using StateMachine.Core;

namespace StateMachine.ExampleTestApp
{
    internal class Program : IProgram
    {
        private readonly IFiniteStateMachine _finiteStateMachine;
        private readonly CancellationTokenSource _cts;

        private Program(IFiniteStateMachine finiteStateMachine, CancellationTokenSource cancellationTokenSource)
        {
            _finiteStateMachine = finiteStateMachine;
            _cts = cancellationTokenSource;


        }

        internal static void Main(string[] args)
        {
            //TODO - Configure DI
            //Create Latches.. 
            var workLatch = new Latch(new Binary2BitState(Guid.NewGuid(), 0, 0), "Work - location");
            var homeLatch = new Latch(new Binary2BitState(Guid.NewGuid(), 0, 0), "Home - location");
            var bedLatch = new Latch(new Binary2BitState(Guid.NewGuid(), 0, 0), "Bed - location");

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
            //Let's start off with the current latch being home

            //TODO - Drive State Machine

            var stateMachine = new FiniteStateMachine(homeLatch, Console.WriteLine);
            var cancellationTokenSource = new CancellationTokenSource();

            //Run program
            var program = new Program(stateMachine, cancellationTokenSource);
            var runTask = program.Run();
            runTask.Wait();
            program.Dispose();
            Console.WriteLine("Hello World!");
        }

        public async Task Run()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _cts?.Dispose();
        }
    }
}
