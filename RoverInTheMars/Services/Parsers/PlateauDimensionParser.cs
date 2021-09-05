using RoverInTheMars.Models;

namespace RoverInTheMars.Services.Parsers
{
    public class PlateauDimensionParser : IContentParser<Dimension>
    {
        public Dimension Parse(string[] lines)
        {
            var splittedLine = lines[0].Split(' ');

            return new Dimension
            {
                Height = int.Parse(splittedLine[0]),
                Width = int.Parse(splittedLine[1])

            };
        }
    }
}
