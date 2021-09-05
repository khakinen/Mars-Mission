using System.Collections.Generic;
using System.Linq;

namespace RoverInTheMars.Services.Instructions
{
    public class InstructionProcessorProvider : IInstructionProcessorProvider
    {
        private readonly IEnumerable<IInstructionProcessor> _instructionAppliers;

        public InstructionProcessorProvider(IEnumerable<IInstructionProcessor> instructionAppliers)
        {
            _instructionAppliers = instructionAppliers;
        }

        public IInstructionProcessor GetInstructionProcessor(char instructionSpecification)
        {
            return _instructionAppliers.FirstOrDefault(i => i.IsMatched(instructionSpecification)) ?? new NullInstructionProcessor();
        }
    }
}
