using System.Text.RegularExpressions;
using Vatsim.Vatis.Utils;
using Vatsim.Vatis.Weather.Objects;

namespace Vatsim.Vatis.Atis.Nodes;

public class ObservationTimeNode : BaseNode<ObservationDayTime>
{
    private const string SPECIAL_TEXT = "SPECIAL";
    private bool mIsSpecialAtis = false;

    public ObservationTimeNode()
    { }

    public override void Parse(Metar metar)
    {
        Parse(metar.ObservationDayTime);
    }

    public void Parse(ObservationDayTime node)
    {
        var minutes = node.Time.Minutes;

        mIsSpecialAtis = Composite.AtisFormat.ObservationTime.StandardUpdateTime != null
            && !Composite.AtisFormat.ObservationTime.StandardUpdateTime.Contains(minutes);

        VoiceAtis = ParseVoiceVariables(node, Composite.AtisFormat.ObservationTime.Template.Voice);
        TextAtis = ParseTextVariables(node, Composite.AtisFormat.ObservationTime.Template.Text);
    }

    public override string ParseTextVariables(ObservationDayTime node, string format)
    {
        if (node == null)
            return "";

        format = Regex.Replace(format, "{time}", $"{node.Time.Hours:00}{node.Time.Minutes:00}", RegexOptions.IgnoreCase);
        format = Regex.Replace(format, "{hours}", $"{node.Time.Hours:00}", RegexOptions.IgnoreCase);
        format = Regex.Replace(format, "{minutes}", $"{node.Time.Minutes:00}", RegexOptions.IgnoreCase);
        format = Regex.Replace(format, "{special}", mIsSpecialAtis ? SPECIAL_TEXT : "", RegexOptions.IgnoreCase);

        return format;
    }

    public override string ParseVoiceVariables(ObservationDayTime node, string format)
    {
        if (node == null)
            return "";

        format = Regex.Replace(format, "{time}", $"{node.Time.Hours.ToString("00").ToSerialForm()} {node.Time.Minutes.ToString("00").ToSerialForm()}", RegexOptions.IgnoreCase);
        format = Regex.Replace(format, "{hours}", node.Time.Hours.ToString("00").ToSerialForm(), RegexOptions.IgnoreCase);
        format = Regex.Replace(format, "{minutes}", node.Time.Minutes.ToString("00").ToSerialForm(), RegexOptions.IgnoreCase);
        format = Regex.Replace(format, "{special}", mIsSpecialAtis ? SPECIAL_TEXT : "", RegexOptions.IgnoreCase);

        return format;
    }
}