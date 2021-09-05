using System.Threading;

namespace RoverInTheMars.Services.Missions
{
    public interface IMissionFactory
    {
        Mission CreateMission(string commandText, int roverCount, CancellationToken cancellationToken = default);
    }
}
