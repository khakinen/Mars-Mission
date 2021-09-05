using System;
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

        public Mission CreateMission(string commandText, CancellationToken cancellationToken = default)
        {
            var roverCount = (commandText.Split(Environment.NewLine).Length - 1) / 2;

            var rovers = new List<IRover>();

            for (int i = 0; i < roverCount; i++)
            {
                var rover = _roverFactory.CreateRover();

                rovers.Add(rover);
            }

            return new Mission(commandText, rovers.ToArray());
        }
    }
}
