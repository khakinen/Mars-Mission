using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using RoverInTheMars.Models;

namespace RoverInTheMars.Services.Parsers
{
    public class CommandParser : ICommandParser
    {
        private readonly IContentParser<Dimension> _plateauDimensionParser;
        private readonly IContentParser<RoverCommand> _roverCommandParser;

        public CommandParser(IContentParser<Dimension> plateauDimensionParser, IContentParser<RoverCommand> roverCommandParser)
        {
            _plateauDimensionParser = plateauDimensionParser;
            _roverCommandParser = roverCommandParser;
        }

        public async Task<Command> Parse(string commandText)
        {
            try
            {
                var command = new Command();

                using (var stringReader = new StringReader(commandText))
                {
                    var line = await stringReader.ReadLineAsync();

                    command.PlateauDimension = _plateauDimensionParser.Parse(new string[] { line });

                    var roverCommands = new List<RoverCommand>();

                    while ((line = (await stringReader.ReadLineAsync())) != null)
                    {
                        var roverCommand = _roverCommandParser.Parse(new string[] { line, (await stringReader.ReadLineAsync()) });

                        roverCommands.Add(roverCommand);
                    }

                    command.RoverCommands = roverCommands.ToArray();
                }

                return command;
            }
            catch (Exception ex)
            {
                throw new InvalidCommandException(ex);
            }

        }
    }
}

