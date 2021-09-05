using System.Collections.Generic;

namespace RoverInTheMars.Models
{
    public class Command
    {
        public Dimension PlateauDimension { get; set; }
        public RoverCommand[] RowerCommands { get; set; }
    }
}
