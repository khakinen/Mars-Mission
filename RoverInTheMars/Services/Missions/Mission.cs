using RoverInTheMars.Services.Rovers;

namespace RoverInTheMars.Services.Missions
{
    public class Mission
    {
        public string CommandText { get; internal set; }
        public IRover[] Rovers { get; internal set; }

        public Mission(string commandText, IRover[] rovers)
        {
            CommandText = commandText;
            Rovers = rovers;
        }
    }
}
