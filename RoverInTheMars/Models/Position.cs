namespace RoverInTheMars.Models
{
    public class Position
    {
        public Direction Direction { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public override string ToString() => $"({X},{Y})";
    }
}
