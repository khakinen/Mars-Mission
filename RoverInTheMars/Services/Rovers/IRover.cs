using System.Threading;
using System.Threading.Tasks;
using RoverInTheMars.Models;

namespace RoverInTheMars.Services.Rovers
{
    public interface IRover
    {
        RoverStatus RoverStatus { get; }

        Position CurrentPosition { get; }

        string Name { get; }

        Task Go(RoverCommand roverCommand, Dimension plateauDimension, string roverName, CancellationToken cancellationToken = default);
    }
}