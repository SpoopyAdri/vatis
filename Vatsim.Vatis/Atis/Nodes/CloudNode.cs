using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Vatsim.Vatis.Profiles.AtisFormat.Nodes;
using Vatsim.Vatis.Utils;
using Vatsim.Vatis.Weather.Extensions;
using Vatsim.Vatis.Weather.Objects;

namespace Vatsim.Vatis.Atis.Nodes;

public class CloudNode : BaseNode<CloudLayer>
{
    private CloudLayer mCeilingLayer;

    public CloudNode()
    { }

    public override void Parse(Metar metar)
    {
        Parse(metar.CloudLayers);
    }

    public void Parse(CloudLayer[] nodes)
    {
        if (nodes == null)
            return;

        var voiceAtis = new List<string>();
        var textAtis = new List<string>();

        mCeilingLayer = nodes
            .Where(n => n.Altitude > 0 && (n.CloudType == Weather.Enums.CloudType.Overcast || n.CloudType == Weather.Enums.CloudType.Broken))
            .Select(n => n)
            .OrderBy(n => n.Altitude)
            .FirstOrDefault();

        foreach (var node in nodes)
        {
            voiceAtis.Add(FormatCloudsVoice(node));
            textAtis.Add(FormatCloudsText(node));
        }

        var voiceTemplate = Composite.AtisFormat.Clouds.Template.Voice;
        var textTemplate = Composite.AtisFormat.Clouds.Template.Text;

        VoiceAtis = Regex.Replace(voiceTemplate, "{clouds}", string.Join(", ", voiceAtis).Trim(',').Trim(' '), RegexOptions.IgnoreCase);
        TextAtis = Regex.Replace(textTemplate, "{clouds}", string.Join(" ", textAtis).Trim(' '), RegexOptions.IgnoreCase);
    }

    private string FormatCloudsText(CloudLayer layer)
    {
        int altitude = layer.Altitude;
        if (Composite.AtisFormat.Clouds.ConvertToMetric)
            altitude = (int)(altitude * 0.30);

        var cloudType = EnumTranslator.GetEnumDescription(layer.CloudType);
        var convectiveType = EnumTranslator.GetEnumDescription(layer.ConvectiveCloudType);

        if (Composite.AtisFormat.Clouds.Types.ContainsKey(cloudType))
        {
            var template = (Composite.AtisFormat.Clouds.Types[cloudType] as CloudType).Text;

            template = layer.IsCloudBelow
                ? Regex.Replace(template, "{altitude}", $" {Composite.AtisFormat.Clouds?.UndeterminedLayerAltitude.Text ?? "undetermined"} ", RegexOptions.IgnoreCase)
                : Regex.Replace(template, "{altitude}", altitude.ToString("000"), RegexOptions.IgnoreCase);

            if (layer.ConvectiveCloudType != Weather.Enums.ConvectiveCloudType.None
                && !Composite.AtisFormat.Clouds.ConvectiveTypes.ContainsKey(convectiveType))
            {
                return "";
            }

            template = layer.ConvectiveCloudType != Weather.Enums.ConvectiveCloudType.None
                ? Regex.Replace(template, "{convective}", convectiveType, RegexOptions.IgnoreCase)
                : Regex.Replace(template, "{convective}", "", RegexOptions.IgnoreCase);

            return template.Trim();
        }

        return "";
    }

    private string FormatCloudsVoice(CloudLayer layer)
    {
        int altitude = layer.Altitude * 100;
        if (Composite.AtisFormat.Clouds.ConvertToMetric)
            altitude = (int)(altitude * 0.30);

        var cloudType = EnumTranslator.GetEnumDescription(layer.CloudType);
        var convectiveType = EnumTranslator.GetEnumDescription(layer.ConvectiveCloudType);

        if (Composite.AtisFormat.Clouds.Types.ContainsKey(cloudType))
        {
            var template = (Composite.AtisFormat.Clouds.Types[cloudType] as CloudType).Voice;

            template = layer.IsCloudBelow
                ? Regex.Replace(template, "{altitude}", $" {Composite.AtisFormat.Clouds?.UndeterminedLayerAltitude.Voice ?? "undetermined"} ", RegexOptions.IgnoreCase)
                : Regex.Replace(template, "{altitude}", altitude.ToWordString(), RegexOptions.IgnoreCase);

            if (layer.ConvectiveCloudType != Weather.Enums.ConvectiveCloudType.None
                && !Composite.AtisFormat.Clouds.ConvectiveTypes.ContainsKey(convectiveType))
            {
                return "";
            }

            template = layer.ConvectiveCloudType != Weather.Enums.ConvectiveCloudType.None
                ? Regex.Replace(template, "{convective}", Composite.AtisFormat.Clouds.ConvectiveTypes[convectiveType], RegexOptions.IgnoreCase)
                : Regex.Replace(template, "{convective}", "", RegexOptions.IgnoreCase);

            return Composite.AtisFormat.Clouds.IdentifyCeilingLayer && layer == mCeilingLayer
                ? "ceiling " + template.Trim()
                : template.Trim();
        }

        return "";
    }

    public override string ParseTextVariables(CloudLayer node, string format)
    {
        throw new NotImplementedException();
    }

    public override string ParseVoiceVariables(CloudLayer node, string format)
    {
        throw new NotImplementedException();
    }
}