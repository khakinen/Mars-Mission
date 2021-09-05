using System.Threading.Tasks;
using RoverInTheMars.Models;

namespace RoverInTheMars.Services.Instructions
{
    public class NullInstructionProcessor : IInstructionProcessor
    {
        public bool IsMatched(char origin) => false;

        public Position GetNextPosition(Position currentPosition)
        {
            throw new InvalidCommandException();
        }
    }
}
