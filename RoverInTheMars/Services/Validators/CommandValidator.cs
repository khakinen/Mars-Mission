using RoverInTheMars.Models;

namespace RoverInTheMars.Validators
{
    public class CommandValidator : ICommandValidator
    {
        public void Validate(Command command, int roversCount)
        {
            if (command.RoverCommands.Length > roversCount)
                throw new InvalidCommandException("Deployed rover count is not enough to accomplish this mission!");
        }
    }
}
