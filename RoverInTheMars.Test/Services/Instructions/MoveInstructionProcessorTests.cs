using RoverInTheMars.Models;
using RoverInTheMars.Services.Instructions;
using Xunit;

namespace RoverInTheMars.Test.Services.Instructions
{
    public class MoveInstructionProcessorTests
    {
        [InlineData('M')]
        [Theory(DisplayName = "MoveInstructionProcessor should match when the character is 'M'")]
        public void Test1(char c)
        {
            var instructionProcessor = new MoveInstructionProcessor();

            var result = instructionProcessor.IsMatched(c);

            Assert.True(result);
        }

        [InlineData('X')]
        [InlineData('L')]
        [InlineData('R')]
        [Theory(DisplayName = "MoveInstructionProcessor should not match when the character isn't 'M'")]
        public void Test2(char c)
        {
            var instructionProcessor = new MoveInstructionProcessor();

            var result = instructionProcessor.IsMatched(c);

            Assert.False(result);
        }

        [ClassData(typeof(PositionTestData))]
        [Theory(DisplayName = "MoveInstructionProcessor should return a correct position with only changing X Y'")]
        public void GetNextPositionTest2(Position inutPosition)
        {
            var instructionProcessor = new MoveInstructionProcessor();

            var actualPosition = instructionProcessor.GetNextPosition(inutPosition);

            switch (inutPosition.Direction)
            {
                case Direction.N:
                    Assert.Equal(inutPosition.Y + 1, actualPosition.Y);
                    break;
                case Direction.S:
                    Assert.Equal(inutPosition.Y - 1, actualPosition.Y);

                    Assert.Equal(inutPosition.X, actualPosition.X);
                    break;
                case Direction.E:
                    Assert.Equal(inutPosition.X + 1, actualPosition.X);
                    break;
                case Direction.W:
                    Assert.Equal(inutPosition.X - 1, actualPosition.X);

                    Assert.Equal(inutPosition.Y, actualPosition.Y);
                    break;
                default:
                    break;
            }

            Assert.Equal(inutPosition.Direction, actualPosition.Direction);
        }
    }
}
