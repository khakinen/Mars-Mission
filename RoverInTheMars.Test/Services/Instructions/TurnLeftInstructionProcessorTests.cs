using RoverInTheMars.Models;
using RoverInTheMars.Services.Instructions;
using Xunit;

namespace RoverInTheMars.Test.Services.Instructions
{
    public class TurnLeftInstructionProcessorTests
    {
        [InlineData('L')]
        [Theory(DisplayName = "TurnLeftInstructionProcessor should match when the character is 'L'")]
        public void Test1(char c)
        {
            var instructionProcessor = new TurnLeftInstructionProcessor();

            var result = instructionProcessor.IsMatched(c);

            Assert.True(result);
        }

        [InlineData('X')]
        [InlineData('M')]
        [InlineData('R')]
        [Theory(DisplayName = "TurnLeftInstructionProcessor should not match when the character isn't 'L'")]
        public void Test2(char c)
        {
            var instructionProcessor = new TurnLeftInstructionProcessor();

            var result = instructionProcessor.IsMatched(c);

            Assert.False(result);
        }

        [ClassData(typeof(PositionTestData))]
        [Theory(DisplayName = "TurnLeftInstructionProcessor should return a correct position with only changing Direction'")]
        public void GetNextPositionTest2(Position inutPosition)
        {
            var instructionProcessor = new TurnLeftInstructionProcessor();

            var actualPosition = instructionProcessor.GetNextPosition(inutPosition);

            switch (inutPosition.Direction)
            {
                case Direction.N:
                    Assert.Equal(Direction.W, actualPosition.Direction);
                    break;

                case Direction.S:
                    Assert.Equal(Direction.E, actualPosition.Direction);
                    break;

                case Direction.E:
                    Assert.Equal(Direction.N, actualPosition.Direction);
                    break;

                case Direction.W:
                    Assert.Equal(Direction.S, actualPosition.Direction);
                    break;
                default:
                    break;
            }

            Assert.Equal(inutPosition.Y, actualPosition.Y);
            Assert.Equal(inutPosition.X, actualPosition.X);
        }
    }
}
