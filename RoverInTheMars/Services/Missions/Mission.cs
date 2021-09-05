using RoverInTheMars.Services.Rovers;

namespace RoverInTheMars.Services.Missions
{
    public class Mission
    {
        public string CommandText { get; internal set; }
        public Rover[] Rovers { get; internal set; }

        public Mission(string commandText, Rover[] rovers)
        {
            CommandText = commandText;
            Rovers = rovers;
        }
    }
}
