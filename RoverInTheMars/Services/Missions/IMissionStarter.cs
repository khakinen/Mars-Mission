using System.Threading;
using System.Threading.Tasks;

namespace RoverInTheMars.Services.Missions
{
    public interface IMissionStarter
    {
        Task StartMission(Mission mission, CancellationToken cancellationToken = default);
    }
}
