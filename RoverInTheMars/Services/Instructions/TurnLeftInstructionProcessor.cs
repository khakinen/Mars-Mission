using System.Threading.Tasks;
using RoverInTheMars.Models;

namespace RoverInTheMars.Services.Instructions
{
    public class TurnLeftInstructionProcessor : IInstructionProcessor
    {
        public const char InstructionSpecification = 'L';

        public bool IsMatched(char specification) => InstructionSpecification == specification;

        public Position GetNextPosition(Position currentPosition)
        {
            return new Position
            {
                Direction = (Direction)(((int)currentPosition.Direction + 3) % 4),
                X = currentPosition.X,
                Y = currentPosition.Y
            };
        }
    }
}
