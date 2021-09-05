using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RoverInTheMars.Services.Parsers;
using RoverInTheMars.Services.Traffic;
using RoverInTheMars.Validators;

namespace RoverInTheMars.Services.Missions
{
    public class MissionStarter : IMissionStarter
    {
        private readonly ICommandParser _commandParser;
        private readonly ICommandValidator _commandValidator;
        private readonly ITrafficPolice _trafficPolice;
        private readonly ILogger<MissionStarter> _logger;


        public MissionStarter(ICommandParser commandParser, ICommandValidator commandValidator, ITrafficPolice trafficPolice, ILogger<MissionStarter> logger)
        {
            _commandParser = commandParser;
            _commandValidator = commandValidator;
            _trafficPolice = trafficPolice;
            _logger = logger;
        }

        public async Task StartMission(Mission mission, CancellationToken cancellationToken = default)
        {
            try
            {
                var command = await _commandParser.Parse(mission.CommandText);

                _commandValidator.Validate(command, mission.Rovers.Length);

                _trafficPolice.Start(command.PlateauDimension, command.RowerCommands.Select(rc => rc.InitialPosition));

                Parallel.For(0, command.RowerCommands.Length, (i, state) =>
                {
                    var roverName = $"[Rover{i}]";

                    mission.Rovers[i].Go(command.RowerCommands[i], command.PlateauDimension, roverName, cancellationToken);
                });


                await LogPositions(mission, cancellationToken = default);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task LogPositions(Mission mission, CancellationToken cancellationToken = default)
        {
            await Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var sb = new StringBuilder();
                    {
                        sb.AppendLine("Current Positions:");
                        sb.AppendJoin(" - ", mission.Rovers.Select(r => $"{r.Name}: {r.CurrentPosition}"));
                        _logger.LogInformation(sb.ToString());
                    }

                    await Task.Delay(3000);
                }
            });

        }
    }
}
