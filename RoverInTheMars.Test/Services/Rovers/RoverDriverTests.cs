using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NSubstitute;
using RoverInTheMars.Models;
using RoverInTheMars.Services.Instructions;
using RoverInTheMars.Services.Rovers;
using RoverInTheMars.Services.Traffic;
using Xunit;

namespace RoverInTheMars.Test.Services.Rovers
{
    public class RoverDriverTests
    {
        private readonly IInstructionProcessorProvider _instructionProcessorProvider;
        private readonly IInstructionValidator _instructionValidator;
        private readonly ITrafficPolice _trafficPolice;
        private readonly ILogger<RoverDriver> _logger;

        public RoverDriverTests()
        {

            _instructionProcessorProvider = Substitute.For<IInstructionProcessorProvider>();
            _instructionValidator = Substitute.For<IInstructionValidator>();
            _trafficPolice = Substitute.For<ITrafficPolice>();
            _logger = Substitute.For<ILogger<RoverDriver>>();
        }

        [Fact(DisplayName = "RoverDriver should drive rover with making required service calls(with correct parameters) according to the instructions")]
        public async Task DriveTest()
        {
            var roverCommand = new RoverCommand
            {
                InitialPosition = new Position
                {
                    X = 1,
                    Y = 2,
                    Direction = Direction.E
                },
                Instructions = "MLRMMR".ToCharArray()
            };

            var plateauDimension = new Dimension
            {
                Height = 10,
                Width = 10
            };

            var roverDriver = new RoverDriver(_instructionProcessorProvider, _instructionValidator, _trafficPolice, _logger);

            var instructionProcessor = Substitute.For<IInstructionProcessor>();
            _instructionProcessorProvider.GetInstructionProcessor(Arg.Any<char>()).Returns(instructionProcessor);

            var nextPosition = new Position
            {
                Direction = Direction.E,
                X = 1,
                Y = 2
            };

            instructionProcessor.GetNextPosition(Arg.Any<Position>()).Returns(nextPosition);

            await roverDriver.Drive(roverCommand, plateauDimension, "fake-rover", default);

            Received.InOrder(() =>
            {
                for (int i = 0; i < roverCommand.Instructions.Length; i++)
                {
                    _instructionProcessorProvider.Received().GetInstructionProcessor(Arg.Is(roverCommand.Instructions[i]));
                    instructionProcessor.Received().GetNextPosition(Arg.Is(roverDriver.CurrentPosition));
                    if (roverDriver.CurrentPosition.Direction == nextPosition.Direction)
                    {
                        _instructionValidator.Received().Validate(Arg.Any<Position>(), Arg.Any<Dimension>());
                        _trafficPolice.Received().PlaceSafe(Arg.Any<string>(), Arg.Is(roverDriver.CurrentPosition), Arg.Any<Position>(), Arg.Any<Action>());
                    }
                }
            });
        }
    }
}
