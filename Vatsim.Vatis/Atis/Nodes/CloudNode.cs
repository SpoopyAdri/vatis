using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
        int altitude = layer.Altitude * 100;
        if (Composite.AtisFormat.Clouds.ConvertToMetric)
            altitude = (int)(altitude * 0.30);

        if (Composite.AtisFormat.Clouds.ConvertToMetric
            && (layer.CloudType == Weather.Enums.CloudType.Few
                || layer.CloudType == Weather.Enums.CloudType.Scattered
                || layer.CloudType == Weather.Enums.CloudType.Broken
                || layer.CloudType == Weather.Enums.CloudType.Overcast
                || layer.CloudType == Weather.Enums.CloudType.VerticalVisibility))
        {
            return $"{EnumTranslator.GetEnumDescription(layer.CloudType)}{altitude / 100:000}{(layer.ConvectiveCloudType != Weather.Enums.ConvectiveCloudType.None ? EnumTranslator.GetEnumDescription(layer.ConvectiveCloudType) : "")}";
        }

        return layer.RawValue;
    }

    private string FormatCloudsVoice(CloudLayer layer)
    {
        int altitude = layer.Altitude * 100;
        if (Composite.AtisFormat.Clouds.ConvertToMetric)
            altitude = (int)(altitude * 0.30);

        return FormatCloudTypeFromTemplate(altitude, layer);
    }

    private string FormatCloudTypeFromTemplate(int altitude, CloudLayer layer)
    {
        var cloudType = EnumTranslator.GetEnumDescription(layer.CloudType);
        var convectiveType = EnumTranslator.GetEnumDescription(layer.ConvectiveCloudType);

        if (Composite.AtisFormat.Clouds.Types.ContainsKey(cloudType))
        {
            var template = Composite.AtisFormat.Clouds.Types[cloudType];
            template = Regex.Replace(template, "{altitude}", altitude.NumbersToWords(), RegexOptions.IgnoreCase);

            if (layer.ConvectiveCloudType != Weather.Enums.ConvectiveCloudType.None
                && Composite.AtisFormat.Clouds.ConvectiveTypes.ContainsKey(convectiveType))
            {
                template = Regex.Replace(template, "{convective}", Composite.AtisFormat.Clouds.ConvectiveTypes[convectiveType], RegexOptions.IgnoreCase);
            }
            else
            {
                template = Regex.Replace(template, "{convective}", "", RegexOptions.IgnoreCase);
            }

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