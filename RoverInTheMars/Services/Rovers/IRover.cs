using System.Threading;
using System.Threading.Tasks;
using RoverInTheMars.Models;

namespace RoverInTheMars.Rovers
{
    public interface IRover
    {
        RoverStatus RoverStatus { get; }

        Position CurrentPosition { get; }

        Task Go(RoverCommand roverCommand, Dimension plateauDimension, string roverName, CancellationToken cancellationToken = default);
    }
}