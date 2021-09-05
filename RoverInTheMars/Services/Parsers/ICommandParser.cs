using System.Threading.Tasks;
using RoverInTheMars.Models;

namespace RoverInTheMars.Services.Parsers
{
    public interface ICommandParser
    {
        Task<Command> Parse(string commandText);
    }
}
