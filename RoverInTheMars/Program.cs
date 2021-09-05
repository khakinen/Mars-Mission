using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RoverInTheMars;
using RoverInTheMars.Rovers;
using RoverInTheMars.Services;
using RoverInTheMars.Services.Instructions;
using RoverInTheMars.Services.Parsers;
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

                //var commandText = File.ReadAllText("input.txt");
                var commandText = $"5 5{Environment.NewLine}"
                    + $"1 2 N{Environment.NewLine}"
                    + $"LMLTLMMMMRLMMLMLMLMLMLMLMLMMLMLMLMLMLMLMLMMLMLMLMLMLMLMLMMLMLMLMLMLMLMLMLMLMMMMRLMMLMLMLMLMLMLMLMMLMLMLMLMLMLMLMMLMLMLMLMLMLMLMMLMLMLMLMLMLMLMLMLMMMMRLMMLMLMLMLMLMLMLMMLMLMLMLMLMLMLMMLMLMLMLMLMLMLMMLMLMLMLMLMLMLMLMLMMMMRLMMLMLMLMMMMRLMMLMLMLMLMLMLMLMMLMLMLMLMLMLMLMMLMLMLMLMLMLMLMMLMLMLMLMLMLMLMLMLMMMMRLMMLMLMLMLMLMLMLMMLMLMLMLMLMLMLMMLMLMLMLMLMLMLMMLMLMLMLMLMLMLMLMLMMMMRLMMLMLMLMLMLMLMLMMLMLMLMLMLMLMLMMLMLMLMLMLMLMLMMLMLMLMLMLMLMLMLMLMLMLMLMLMMLMLMLMLMLMLMLMMLMLMLMLMLMLMLMMLMLMLMLMLMLMLMLMLMMMMRLMMLMLMLMLMLMLMLMMLMLMLMLMLMLMLMMLMLMLMLMLMLMLMMLMLMLMLMLMLM{Environment.NewLine}"
                    + $"2 3 E{Environment.NewLine}"
                    + $"LMRMLMRMLMRMRMRMRMRM{Environment.NewLine}"
                    + $"4 3 W{Environment.NewLine}"
                    + $"LMLMLMLMR{Environment.NewLine}"
                    + $"0 3 E{Environment.NewLine}"
                    + $"MMLMMLMMLMLMLMLMLMLMR{Environment.NewLine}";
                await CreateMission(host.Services, commandText, 4);

                await host.RunAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
          Host.CreateDefaultBuilder(args)
           .ConfigureAppConfiguration(ConfigureApp)
           .ConfigureServices(ConfigureServices);

        private static async Task CreateMission(IServiceProvider services, string commandText, int roverCount, CancellationToken cancellationToken = default)
        {
            using IServiceScope serviceScope = services.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;

            _logger = provider.GetRequiredService<ILogger<Program>>();

            var rovers = new List<Rover>();

            for (int i = 0; i < roverCount; i++)
            {
                var rover = provider.GetRequiredService<Rover>();

                rovers.Add(rover);
            }

            var mission = provider.GetRequiredService<Mission>();

            await mission.Start(commandText, rovers.ToArray(), cancellationToken);
        }

        private static void ConfigureApp(HostBuilderContext hostBuilderContext, IConfigurationBuilder configurationBuilder)
        {
            var env = hostBuilderContext.HostingEnvironment;
        }

        private static void ConfigureServices(HostBuilderContext hostBuilderContext, IServiceCollection services)
        {
            var configuration = hostBuilderContext.Configuration;

            services.AddLogging(logging =>
            {
                logging.AddConfiguration(configuration.GetSection("Logging"));
                logging.AddConsole();
            });

            services
                .AddScoped<Mission>()
                .AddScoped<ICommandParser, CommandParser>()
                .AddScoped<ICommandValidator, CommandValidator>()

                .AddTransient<IRoverDriver, RoverDriver>()
                .AddTransient<Rover>()

                .AddTransient<IInstructionValidator, InstructionValidator>()
                .AddTransient<IInstructionProcessorProvider, InstructionProcessorProvider>()
                .AddTransient<IInstructionProcessor, TurnLeftInstructionProcessor>()
                .AddTransient<IInstructionProcessor, TurnRightInstructionProcessor>()
                .AddTransient<IInstructionProcessor, MoveInstructionProcessor>()

                .AddSingleton<ITrafficPolice, TrafficPolice>()
               .AddSingleton<TrafficeConfiguration>(new TrafficeConfiguration() { MovementAttemptDelayInMiliseconds = 1000 });
        }
    }
}
