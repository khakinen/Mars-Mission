using System.Threading.Tasks;
using RoverInTheMars.Models;

namespace RoverInTheMars.Services.Instructions
{
    public interface IInstructionProcessor
    {
        bool IsMatched(char origin);

        Position GetNextPosition(Position currentPosition);
    }
}
