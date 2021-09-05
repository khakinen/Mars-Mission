using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RoverInTheMars.Services.Instructions;
using RoverInTheMars.Services.Missions;
using RoverInTheMars.Services.Parsers;
using RoverInTheMars.Services.Rovers;
using RoverInTheMars.Services.Traffic;
using RoverInTheMars.Validators;

namespace RoverInTheMars
{

    class Program
    {
        private static ILogger<Program> _logger;

        static async Task Main(string[] args)
        {
            try
            {
                using IHost host = CreateHostBuilder(args).Build();

                var commandText = File.ReadAllText("input.txt");

                await StartMission(host.Services, commandText, 4);

                await host.RunAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);

                var innerException = ex.InnerException;

                while (innerException != null)
                {
                    Console.WriteLine(innerException.Message);
                    Console.WriteLine(innerException.StackTrace);

                    innerException = innerException.InnerException;
                }
            }
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
          Host.CreateDefaultBuilder(args)
           .ConfigureServices(ConfigureServices);

        private static async Task StartMission(IServiceProvider services, string commandText, int roverCount)
        {
            using IServiceScope serviceScope = services.CreateScope();

            IServiceProvider provider = serviceScope.ServiceProvider;

            var missionFactory = provider.GetRequiredService<IMissionFactory>();

            var mission = missionFactory.CreateMission(commandText, roverCount);

            var missionStarter = provider.GetRequiredService<IMissionStarter>();

            await missionStarter.StartMission(mission);
        }

        private static void ConfigureServices(HostBuilderContext hostBuilderContext, IServiceCollection services)
        {
            var configuration = hostBuilderContext.Configuration;

            services.AddLogging(logging =>
            {
                logging.AddConsole();
            });

            services
                .AddSingleton<IRoverFactory, RoverFactory>()
                .AddSingleton<IMissionFactory, MissionFactory>()

                .AddScoped<IMissionStarter, MissionStarter>()

                .AddSingleton<ITrafficPolice, TrafficPolice>()
                .AddSingleton(new TrafficeConfiguration() { MovementAttemptDelayInMiliseconds = 1000 })

                .AddSingleton<ICommandParser, CommandParser>()
                .AddSingleton<ICommandValidator, CommandValidator>()

                .AddScoped<IRoverDriver, RoverDriver>()
                .AddScoped<Rover>()

                .AddSingleton<IInstructionValidator, InstructionValidator>()
                .AddSingleton<IInstructionProcessorProvider, InstructionProcessorProvider>()
                .AddSingleton<IInstructionProcessor, TurnLeftInstructionProcessor>()
                .AddSingleton<IInstructionProcessor, TurnRightInstructionProcessor>()
                .AddSingleton<IInstructionProcessor, MoveInstructionProcessor>();
        }
    }
}
