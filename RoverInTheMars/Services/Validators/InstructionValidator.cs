using RoverInTheMars.Models;

namespace RoverInTheMars.Validators
{
    public class InstructionValidator : IInstructionValidator
    {
        public void Validate(Position nextPosition, Dimension plateauDimension)
        {
            if (nextPosition.X > plateauDimension.Width - 1
                || nextPosition.Y > plateauDimension.Height - 1
                || nextPosition.X < 0
                || nextPosition.Y < 0)

            {
                throw new DriveOffException("Drive-Off!");
            }

        }

    }
}
