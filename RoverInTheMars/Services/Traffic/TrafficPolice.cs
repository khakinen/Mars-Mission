using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RoverInTheMars.Models;

namespace RoverInTheMars.Services.Traffic
{

    public class TrafficPolice : ITrafficPolice
    {
        private readonly TrafficeConfiguration _trafficeConfiguration;
        private readonly ILogger<TrafficPolice> _logger;
        private ConcurrentDictionary<(int, int), ParkSpot> _trafficMap = new ConcurrentDictionary<(int, int), ParkSpot>();

        public TrafficPolice(TrafficeConfiguration trafficeConfiguration, ILogger<TrafficPolice> logger)
        {
            _trafficeConfiguration = trafficeConfiguration;
            _logger = logger;
        }

        public void Start(Dimension plateauDimension, IEnumerable<Position> initialPositions)
        {
            for (int i = 0; i < plateauDimension.Width; i++)
            {
                for (int j = 0; j < plateauDimension.Height; j++)
                {
                    _trafficMap.TryAdd(
                        (i, j),
                        new ParkSpot
                        {
                            IsBusy = false,
                            LockObject = new object(),
                            Name = $"{i}{j}"
                        });
                }
            }

            foreach (var initialPosition in initialPositions)
            {
                _trafficMap[(initialPosition.X, initialPosition.Y)].IsBusy = true;
            }

        }

        public async Task PlaceSafe(string roverName, Position currentPosition, Position nextPosition, Action assignPosition, CancellationToken cancellationToken = default)
        {
            var newParkSpot = _trafficMap[(nextPosition.X, nextPosition.Y)];

            var formerParkSpot = _trafficMap[(currentPosition.X, currentPosition.Y)];

            while (!cancellationToken.IsCancellationRequested && newParkSpot.IsBusy)
            {
                _logger.LogInformation($"{roverName} is waiting for {nextPosition}");

                await Task.Delay(_trafficeConfiguration.MovementAttemptDelayInMiliseconds);
            }

            lock (newParkSpot.LockObject)
            {
                newParkSpot.IsBusy = true;

                lock (formerParkSpot.LockObject)
                {
                    assignPosition.Invoke();

                    formerParkSpot.IsBusy = false;

                    _logger.LogInformation($"{roverName}: {currentPosition} -> {nextPosition})");
                }
            }
        }
    }
}
