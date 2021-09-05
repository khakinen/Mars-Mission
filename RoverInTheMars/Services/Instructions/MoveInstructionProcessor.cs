using System;
using RoverInTheMars.Models;

namespace RoverInTheMars.Services.Instructions
{
    public class MoveInstructionProcessor : IInstructionProcessor
    {
        public const char InstructionSpecification = 'M';

        public bool IsMatched(char specification) => InstructionSpecification == specification;

        public Position GetNextPosition(Position position)
        {
            return new Position
            {

                X = position.X + (int)Math.Round(Math.Sin((int)position.Direction * (Math.PI / 2))),
                Y = position.Y + (int)Math.Round(Math.Cos((int)position.Direction * (Math.PI / 2))),

                Direction = position.Direction
            };
        }
    }
}
