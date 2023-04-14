namespace Vatsim.Vatis.Profiles.AtisFormat.Nodes;
public class ObservationTime : BaseFormat
{
    public ObservationTime()
    {
        Template = new()
        {
            Text = "{time}Z",
            Voice = "{time} ZULU {special}"
        };
    }
    public int StandardUpdateTime { get; set; }
}
