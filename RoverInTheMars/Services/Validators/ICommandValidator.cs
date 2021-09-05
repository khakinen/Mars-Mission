namespace RoverInTheMars.Validators
{
    public interface ICommandValidator
    {
        void Validate(RoverInTheMars.Models.Command command, int length);
    }
}
