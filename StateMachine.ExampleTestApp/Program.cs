using System;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using StateMachine.Core;
using StateMachine.ExampleTestApp.Configuration;
using StateMachine.ExampleTestApp.Logger;
using StateMachine.ExampleTestApp.UtilityClasses;

namespace StateMachine.ExampleTestApp
{
    //Will be implementing this StateMachine: https://miro.medium.com/max/1520/1*z-cGUTYS40cSjcMWgiOLww.png

    internal class Program : IProgram
    {
        internal static void Main(string[] args)
        {
            var container = DependencyInjection.ConfigureDependencies();
            var program = container.Resolve<IProgram>();
            var runTask = program.Run(args);
            runTask.Wait();
            program.Dispose();
        }

        internal Program(IFiniteStateMachine finiteStateMachine, ILogger logger, CancellationTokenSource cancellationTokenSource)
        {
            _finiteStateMachine = finiteStateMachine;
            _logger = logger;
            _cts = cancellationTokenSource;
        }

        private readonly IFiniteStateMachine _finiteStateMachine;
        private readonly ILogger _logger;
        private readonly CancellationTokenSource _cts;

        public async Task Run(string[] args)
        {
            _logger.Log("Starting Application");
            ExtendedConsole.PrintHelpText();

            while (!_cts.IsCancellationRequested)
            {
                var key = Console.ReadKey();
                ExtendedConsole.BlankLine();
                switch (key.Key)
                {
                    case ConsoleKey.W:
                        //This could be for example on an event or message received from RabbitMQ, etc... 
                       await _finiteStateMachine.TriggerAction(TransportActions.WakeUp);
                        break;
                    case ConsoleKey.T:
                        await _finiteStateMachine.TriggerAction(TransportActions.TakeTrain);
                        break;
                    case ConsoleKey.S:
                        await _finiteStateMachine.TriggerAction(TransportActions.FallAsleep);
                        break;
                    case ConsoleKey.L:
                        _logger.Log(_finiteStateMachine.GetCurrentState());
                        break;
                    case ConsoleKey.Escape:
                    case ConsoleKey.E:
                        _cts.Cancel();
                        break;
                    default:
                        Console.WriteLine($"You Pressed {key.Key}");
                        ExtendedConsole.PrintHelpText();
                        ExtendedConsole.BlankLine();
                        break;
                }
            }

            _logger.Log($"Closing Application");
        }

        public void Dispose()
        {
            _cts?.Dispose();
            _logger?.Dispose();
        }
    }
}
