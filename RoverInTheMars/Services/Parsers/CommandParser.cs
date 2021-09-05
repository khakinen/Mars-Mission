using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using RoverInTheMars.Models;

namespace RoverInTheMars.Services.Parsers
{
    public class CommandParser : ICommandParser
    {
        public async Task<Command> Parse(string commandText)
        {
            try
            {
                var command = new Command();

                using (var stringReader = new StringReader(commandText))
                {
                    var line = (await stringReader.ReadLineAsync()).Split(' ');

                    command.PlateauDimension = new Dimension()
                    {
                        Height = int.Parse(line[0]),
                        Width = int.Parse(line[1])
                    };

                    var roverCommands = new List<RoverCommand>();

                    while ((line = (await stringReader.ReadLineAsync())?.Split(' ')) != null)
                    {
                        roverCommands.Add(await GetRoverCommand(stringReader, line));
                    }

                    command.RowerCommands = roverCommands.ToArray();
                }

                return command;

            }
            catch (Exception ex)
            {
                throw new InvalidCommandException(ex);
                //return await Task.FromException<Command>(new InvalidCommandException(ex));
            }

        }

        private async Task<RoverCommand> GetRoverCommand(StringReader stringReader, string[] line)
        {
            return new RoverCommand
            {
                InitialPosition = new Position
                {
                    X = int.Parse(line[0]),
                    Y = int.Parse(line[1]),
                    Direction = Enum.Parse<Direction>(line[2])
                },

                Instructions = (await stringReader.ReadLineAsync()).ToCharArray()
            };
        }
    }
}
