using System;
using RoverInTheMars.Models;

namespace RoverInTheMars.Services.Parsers
{
    public class RoverCommandParser : IContentParser<RoverCommand>
    {
        public RoverCommand Parse(string[] lines)
        {
            var splittedLine = lines[0].Split(' ');
            var instructionsLine = lines[1];

            return new RoverCommand
            {
                InitialPosition = new Position
                {
                    X = int.Parse(splittedLine[0]),
                    Y = int.Parse(splittedLine[1]),
                    Direction = Enum.Parse<Direction>(splittedLine[2])
                },

                Instructions = instructionsLine.ToCharArray()
            };
        }
    }
}
