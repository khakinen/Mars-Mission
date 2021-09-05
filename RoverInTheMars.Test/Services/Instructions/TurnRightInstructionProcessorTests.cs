using RoverInTheMars.Models;
using RoverInTheMars.Services.Instructions;
using Xunit;

namespace RoverInTheMars.Test.Services.Instructions
{
    public class TurnRightInstructionProcessorTests
    {
        [InlineData('R')]
        [Theory(DisplayName = "TurnRightInstructionProcessor should match when the character is 'R'")]
        public void Test1(char c)
        {
            var instructionProcessor = new TurnRightInstructionProcessor();

            var result = instructionProcessor.IsMatched(c);

            Assert.True(result);
        }

        [InlineData('X')]
        [InlineData('M')]
        [InlineData('L')]
        [Theory(DisplayName = "TurnRightInstructionProcessor should not match when the character isn't 'R'")]
        public void Test2(char c)
        {
            var instructionProcessor = new TurnRightInstructionProcessor();

            var result = instructionProcessor.IsMatched(c);

            Assert.False(result);
        }

        [ClassData(typeof(PositionTestData))]
        [Theory(DisplayName = "TurnRightInstructionProcessor should return a correct position with only changing Direction'")]
        public void GetNextPositionTest2(Position inutPosition)
        {
            var instructionProcessor = new TurnRightInstructionProcessor();

            var actualPosition = instructionProcessor.GetNextPosition(inutPosition);

            switch (inutPosition.Direction)
            {
                case Direction.N:
                    Assert.Equal(Direction.E, actualPosition.Direction);
                    break;

                case Direction.S:
                    Assert.Equal(Direction.W, actualPosition.Direction);
                    break;

                case Direction.E:
                    Assert.Equal(Direction.S, actualPosition.Direction);
                    break;

                case Direction.W:
                    Assert.Equal(Direction.N, actualPosition.Direction);
                    break;
                default:
                    break;
            }

            Assert.Equal(inutPosition.Y, actualPosition.Y);
            Assert.Equal(inutPosition.X, actualPosition.X);
        }
    }
}
