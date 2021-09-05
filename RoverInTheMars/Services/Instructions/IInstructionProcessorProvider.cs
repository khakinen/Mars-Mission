namespace RoverInTheMars.Services.Instructions
{
    public interface IInstructionProcessorProvider
    {
        IInstructionProcessor GetInstructionProcessor(char instructionSpecification);
    }
}
