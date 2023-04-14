namespace Vatsim.Vatis.Profiles.AtisFormat.Nodes;
public class Dewpoint : BaseFormat
{
    public Dewpoint()
    {
        Template = new()
        {
            Text = "{dewpoint}",
            Voice = "DEWPOINT {dewpoint}"
        };
    }

    public bool UsePlusPrefix { get; set; }
    public bool PronounceLeadingZero { get; set; }
}
