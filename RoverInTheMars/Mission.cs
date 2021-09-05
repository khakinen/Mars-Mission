using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RoverInTheMars.Rovers;
using RoverInTheMars.Services.Parsers;
using RoverInTheMars.Services.Traffic;
using RoverInTheMars.Validators;

namespace RoverInTheMars
{
    public class Mission
    {
        private readonly ICommandParser _commandParser;
        private readonly ICommandValidator _commandValidator;
        private readonly ITrafficPolice _trafficPolice;
        private readonly ILogger<Mission> _logger;

        public Mission(ICommandParser commandParser, ICommandValidator commandValidator, ITrafficPolice trafficPolice, ILogger<Mission> logger)
        {
            _commandParser = commandParser;
            _commandValidator = commandValidator;
            _trafficPolice = trafficPolice;
            _logger = logger;
        }

        public async Task Start(string commandText, Rover[] rovers, CancellationToken cancellationToken = default)
        {
            try
            {
                var command = await _commandParser.Parse(commandText);

                _commandValidator.Validate(command, rovers.Length);

                _trafficPolice.Start(command.PlateauDimension, command.RowerCommands.Select(rc => rc.InitialPosition));

                Parallel.For(0, command.RowerCommands.Length, (i, state) =>
                {
                    var roverName = $"[Rover{i}]";

                    rovers[i].Go(command.RowerCommands[i], command.PlateauDimension, roverName, cancellationToken);
                });

                Task.Run(async () =>
                {
                    while (!cancellationToken.IsCancellationRequested)
                    {
                        var sb = new StringBuilder();
                        {
                            sb.AppendLine("Current Positions:");
                            sb.AppendJoin(" - ", rovers.Select(r => $"{r.Name}: {r.CurrentPosition}"));
                            _logger.LogInformation(sb.ToString());
                        }

                        await Task.Delay(3000);
                    }
                });
            }
            catch (Exception)
            {
                throw;
            }


        }
    }
}
