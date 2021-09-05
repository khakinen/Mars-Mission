using System.Collections.Generic;
using System.Threading;
using RoverInTheMars.Services.Rovers;

namespace RoverInTheMars.Services.Missions
{
    public class MissionFactory : IMissionFactory
    {
        private readonly IRoverFactory _roverFactory;

        public MissionFactory(IRoverFactory roverFactory)
        {
            _roverFactory = roverFactory;
        }

        public Mission CreateMission(string commandText, int roverCount, CancellationToken cancellationToken = default)
        {
            var rovers = new List<Rover>();

            for (int i = 0; i < roverCount; i++)
            {
                var rover = _roverFactory.CreateRover();

                rovers.Add(rover);
            }

            return new Mission(commandText, rovers.ToArray());
        }
    }
}
