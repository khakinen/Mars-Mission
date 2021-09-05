using System.Threading;
using System.Threading.Tasks;
using RoverInTheMars.Models;

namespace RoverInTheMars.Services.Rovers
{
    public interface IRoverDriver
    {
        Task Drive(RoverCommand roverCommand, Dimension plateauDimension, string roverName, CancellationToken cancellationToken = default);

        Position CurrentPosition { get; }

        public string Name { get; }
    }
}
