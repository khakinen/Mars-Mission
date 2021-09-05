namespace RoverInTheMars.Models
{
    public class Command
    {
        public virtual Dimension PlateauDimension { get; set; }
        public virtual RoverCommand[] RoverCommands { get; set; }
    }
}
