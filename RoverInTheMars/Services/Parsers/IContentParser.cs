namespace RoverInTheMars.Services.Parsers
{
    public interface IContentParser<TReturn>
    {
        TReturn Parse(string[] lines);
    }
}
