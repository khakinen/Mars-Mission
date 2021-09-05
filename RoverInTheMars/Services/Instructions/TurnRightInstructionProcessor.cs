using System.Threading.Tasks;
using RoverInTheMars.Models;

namespace RoverInTheMars.Services.Instructions
{
    public class TurnRightInstructionProcessor : IInstructionProcessor
    {
        public const char InstructionSpecification = 'R';

        public bool IsMatched(char specification) => InstructionSpecification == specification;

        public Position GetNextPosition(Position currentPosition)
        {
            return new Position
            {
                Direction = (Direction)(((int)currentPosition.Direction + 1) % 4),
                X = currentPosition.X,
                Y = currentPosition.Y
            };
        }
    }
}
