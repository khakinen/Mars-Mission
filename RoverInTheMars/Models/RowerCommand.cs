using System.Collections.Generic;

namespace RoverInTheMars.Models
{
    public class RoverCommand
    {
        public Position InitialPosition { get; set; }
        public char[] Instructions { get; set; }
    }
}
