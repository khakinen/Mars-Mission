using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using RoverInTheMars.Services.Missions;
using RoverInTheMars.Services.Rovers;
using Xunit;

namespace RoverInTheMars.Test.Services.Missions
{
    public class MissionFactoryTests
    {
        private readonly IRoverFactory _roverFactory;

        public MissionFactoryTests()
        {
            _roverFactory = Substitute.For<IRoverFactory>();
        }

        [InlineData("fake-command-text", 777)]
        [Theory(DisplayName = "MissionFactory should create  mission with required number of Rover and orrect commandText ")]
        public void CreateMissionTest(string commandText, int roverCount)
        {
            var rover = Substitute.For<IRover>();
            var rovers = new List<IRover>();

            for (int i = 0; i < roverCount; i++)
            {
                rovers.Add(rover);
            }

            _roverFactory.CreateRover().Returns(rover);

            var missionFactory = new MissionFactory(_roverFactory);

            var mission = missionFactory.CreateMission(commandText, roverCount);

            Assert.True(Enumerable.SequenceEqual(rovers.ToArray(), mission.Rovers));

            Assert.Equal(commandText, mission.CommandText);


        }
    }
}


