namespace Vatsim.Vatis.Profiles.AtisFormat.Nodes;
public class Temperature : BaseFormat
{
    public Temperature()
    {
        Template = new()
        {
            Text = "{temp}",
            Voice = "TEMPERATURE {temp}"
        };
    }

    public bool UsePlusPrefix { get; set; }
    public bool PronounceLeadingZero { get; set; }
}