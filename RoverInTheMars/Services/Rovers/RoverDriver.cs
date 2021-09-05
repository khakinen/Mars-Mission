using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RoverInTheMars.Models;
using RoverInTheMars.Services.Instructions;
using RoverInTheMars.Services.Traffic;

namespace RoverInTheMars.Rovers
{
    public class RoverDriver : IRoverDriver
    {
        private readonly IInstructionProcessorProvider _instructionProcessorProvider;
        private readonly IInstructionValidator _instructionValidator;
        private readonly ITrafficPolice _trafficPolice;
        private readonly ILogger<RoverDriver> _logger;

        private Position _currentPosition;

        public Position CurrentPosition => _currentPosition;
        public string Name { get; internal set; }


        public RoverDriver(IInstructionProcessorProvider instructionProcessorProvider, IInstructionValidator instructionValidator, ITrafficPolice trafficPolice, ILogger<RoverDriver> logger)
        {

            _instructionProcessorProvider = instructionProcessorProvider;
            _instructionValidator = instructionValidator;
            _trafficPolice = trafficPolice;
            _logger = logger;
        }

        public async Task Drive(RoverCommand roverCommand, Dimension plateauDimension, string roverName, CancellationToken cancellationTokeen)
        {
            try
            {
                Name = roverName;
                _currentPosition = roverCommand.InitialPosition;

                for (int i = 0; i < roverCommand.Instructions.Length; i++)
                {
                    var instructionProcessor = _instructionProcessorProvider.GetInstructionProcessor(roverCommand.Instructions[i]);

                    var nextPosition = instructionProcessor.GetNextPosition(_currentPosition);

                    if (CurrentPosition.Direction != nextPosition.Direction)
                    {
                        _logger.LogInformation($"{roverName}: {_currentPosition.Direction} -> {nextPosition.Direction}");

                        _currentPosition = nextPosition;

                        continue;
                    }

                    _instructionValidator.Validate(nextPosition, plateauDimension);

                    await _trafficPolice.PlaceSafe(roverName, _currentPosition, nextPosition, () => _currentPosition = nextPosition);
                }
            }

            catch (System.Exception)
            {
                throw;
            }

        }
    }
}

