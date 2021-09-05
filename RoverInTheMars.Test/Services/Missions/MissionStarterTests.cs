using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NSubstitute;
using RoverInTheMars.Models;
using RoverInTheMars.Services.Missions;
using RoverInTheMars.Services.Parsers;
using RoverInTheMars.Services.Rovers;
using RoverInTheMars.Services.Traffic;
using RoverInTheMars.Validators;
using Xunit;

namespace RoverInTheMars.Test.Services.Missions
{

    public class MissionStarterTests
    {
        private readonly ICommandParser _commandParser;
        private readonly ICommandValidator _commandValidator;
        private readonly ITrafficPolice _trafficPolice;
        private readonly ILogger<MissionStarter> _logger;

        public MissionStarterTests()
        {
            _commandParser = Substitute.For<ICommandParser>();
            _commandValidator = Substitute.For<ICommandValidator>(); ;
            _trafficPolice = Substitute.For<ITrafficPolice>(); ;
            _logger = Substitute.For<ILogger<MissionStarter>>(); ;
        }

        [Fact(DisplayName = "MissionStarter should start mission after required calls with correct parameters")]
        public void StartMissionTest()
        {
            var missionStarter = new MissionStarter(_commandParser, _commandValidator, _trafficPolice, _logger);

            var mission = new Mission("fake-command-text", new IRover[] { Substitute.For<IRover>(), Substitute.For<IRover>(), Substitute.For<IRover>() });

            var command = Substitute.For<Command>();

            var roverCommands = new List<RoverCommand> { Substitute.For<RoverCommand>(), Substitute.For<RoverCommand>(), Substitute.For<RoverCommand>() }
                .ToArray();

            command.RoverCommands.Returns(roverCommands);

            var plateauDimension = new Dimension();

            command.PlateauDimension.Returns(plateauDimension);

            _commandParser.Parse(Arg.Any<string>()).Returns(command);

            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;
            cancellationTokenSource.CancelAfter(300);

            missionStarter.StartMission(mission, cancellationToken);

            Received.InOrder(() =>
               {
                   _commandParser.Received().Parse(Arg.Any<string>());
                   _commandValidator.Received().Validate(Arg.Any<Command>(), Arg.Any<int>());
                   _trafficPolice.Received().Start(Arg.Any<Dimension>(), Arg.Any<IEnumerable<Position>>());

                   for (int i = 0; i < command.RoverCommands.Length; i++)
                   {
                       mission.Rovers[i].Received().Go(Arg.Is(command.RoverCommands[i]), plateauDimension, Arg.Any<string>(), default);
                   }
               }
            );
        }
    }
}


