using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RoverInTheMars.Models;

namespace RoverInTheMars.Services.Traffic
{
    public interface ITrafficPolice
    {
        void Start(Dimension plateauDimension, IEnumerable<Position> initialPositions);

        Task PlaceSafe(string name, Position currentPosition, Position nextPosition, Action action, CancellationToken cancellationToken = default);
    }
}
