using System;
using System.Threading.Tasks;
using NSubstitute;
using RoverInTheMars.Models;
using RoverInTheMars.Services.Parsers;
using Xunit;

namespace RoverInTheMars.Test.Services.Parsers
{
    public class CommandParserTest
    {
        private readonly IContentParser<Dimension> _plateauDimensionParser;
        private readonly IContentParser<RoverCommand> _roverCommandParser;

        public CommandParserTest()
        {
            _plateauDimensionParser = Substitute.For<IContentParser<Dimension>>();
            _roverCommandParser = Substitute.For<IContentParser<RoverCommand>>();
        }

        [Fact(DisplayName = "Commandparser should parse with calling dependent parsers in required times")]
        public async Task ParseTest1()
        {
            var commandText =
             $"5 5{Environment.NewLine}"
           + $"1 2 N{Environment.NewLine}"
           + $"LMLTLMLMLMLMLMLMLMLM{Environment.NewLine}"
           + $"2 3 E{Environment.NewLine}"
           + $"LMRMLMRMLMRMRMRMRMRM{Environment.NewLine}";

            var commandParser = new CommandParser(_plateauDimensionParser, _roverCommandParser);

            await commandParser.Parse(commandText);

            var lines = commandText.Split(Environment.NewLine);

            _plateauDimensionParser.Received(1).Parse(Arg.Is<string[]>(ln => ln[0] == lines[0]));

            _roverCommandParser.Received(2).Parse(Arg.Any<string[]>());
        }
    }
}
