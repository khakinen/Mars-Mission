using RoverInTheMars.Models;

namespace RoverInTheMars
{
    public interface IInstructionValidator
    {
        void Validate(Position nextPosition, Dimension plateauDimension);
    }
}
