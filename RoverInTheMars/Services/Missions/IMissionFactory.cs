using System.Threading;

namespace RoverInTheMars.Services.Missions
{
    public interface IMissionFactory
    {
        Mission CreateMission(string commandText, CancellationToken cancellationToken = default);
    }
}
