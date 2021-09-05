using System.Collections;
using System.Collections.Generic;
using RoverInTheMars.Models;

public class PositionTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] {
                    new Position
                    {
                         Direction=Direction.N,
                           X=1,
                           Y=2
                    } };
        yield return new object[] {
          new Position
          {
               Direction=Direction.E,
                 X=1,
                 Y=2
          } };
        yield return new object[] {
          new Position
          {
               Direction=Direction.S,
                 X=5,
                 Y=3
          } };
        yield return new object[] {
          new Position
          {
               Direction=Direction.W,
                 X=5,
                 Y=3
          } };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}