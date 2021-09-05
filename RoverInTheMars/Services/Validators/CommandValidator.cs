using RoverInTheMars.Models;

namespace RoverInTheMars.Validators
{
    public class CommandValidator : ICommandValidator
    {
        public void Validate(Command command, int roversCount)
        {
            if (command.RowerCommands.Length > roversCount)
                throw new InvalidCommandException();
        }
    }
}
