using System;
using System.Threading.Tasks;
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

                X = position.X + (int)Math.Round(Math.Sin((int)position.Direction * (Math.PI / 2))),//, MidpointRounding.ToEven),
                Y = position.Y + (int)Math.Round(Math.Cos((int)position.Direction * (Math.PI / 2))),//, MidpointRounding.ToEven),

                // Y = position.Y + (int)Math.Round(Math.Cos((int)position.Direction * 90), MidpointRounding.ToEven),
                Direction = position.Direction
            };
        }
    }
}


//double degrees = 360;
//double radians = degrees * (Math.PI / 180);//Math.PI / 180 * degrees;



//Console.WriteLine(Math.Round(Math.Sin(radians)));

//Y = position.Y + (int)Math.Round(Math.Cos((int)position.Direction * 90), MidpointRounding.ToEven),