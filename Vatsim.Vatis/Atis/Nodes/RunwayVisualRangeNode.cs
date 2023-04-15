using System.Collections.Generic;
using System.Text.RegularExpressions;
using Vatsim.Vatis.Utils;
using Vatsim.Vatis.Weather.Objects;

namespace Vatsim.Vatis.Atis.Nodes;

public class RunwayVisualRangeNode : BaseNode<RunwayVisualRange>
{
    public RunwayVisualRangeNode()
    { }

    public override void Parse(Metar metar)
    {
        Parse(metar.RunwayVisualRanges);
    }

    public void Parse(RunwayVisualRange[] node)
    {
        var tts = new List<string>();
        var acars = new List<string>();

        if (node == null)
            return;

        foreach (var rvr in node)
        {
            var result = new List<string>();

            var match = Regex.Match(rvr.RawValue, @"^R([0-3]{1}\d{1})(L|C|R)?\/(M|P)?(\d{4})(V|VP)?(\d{4})?(FT)?(?:\/(U|D|N))?$");

            if (match.Success)
            {
                acars.Add(rvr.RawValue);

                var rwyNumber = match.Groups[1].Value;
                var rwyDesignator = "";

                switch (match.Groups[2].Value)
                {
                    case "L":
                        rwyDesignator = "left";
                        break;
                    case "R":
                        rwyDesignator = "right";
                        break;
                    case "C":
                        rwyDesignator = "center";
                        break;
                }

                if (match.Groups[5].Value == "V")
                {
                    var minVis = int.Parse(match.Groups[4].Value);
                    var maxVis = int.Parse(match.Groups[6].Value);

                    if (match.Groups[3].Value == "M")
                    {
                        result.Add($"variable from less than {minVis.ToGroupForm()} to {maxVis.ToGroupForm()}");
                    }
                    else
                    {
                        result.Add($"variable between {minVis.ToGroupForm()} and {maxVis.ToGroupForm()}");
                    }
                }
                else if (match.Groups[5].Value == "VP")
                {
                    var minVis = int.Parse(match.Groups[4].Value);
                    var maxVis = int.Parse(match.Groups[6].Value);

                    if (match.Groups[3].Value == "M")
                    {
                        result.Add($"variable from less than {minVis.ToGroupForm()} to greater than{maxVis.ToGroupForm()}");
                    }
                    else
                    {
                        result.Add($"{minVis.ToGroupForm()} variable to greater than {maxVis.ToGroupForm()}");
                    }
                }
                else
                {
                    var vis = int.Parse(match.Groups[4].Value);

                    if (match.Groups[3].Value == "M")
                    {
                        result.Add($"less than {vis.ToGroupForm()}");
                    }
                    else if (match.Groups[3].Value == "P")
                    {
                        result.Add($"more than {vis.ToGroupForm()}");
                    }
                    else
                    {
                        result.Add(vis.ToGroupForm());
                    }
                }

                if (match.Groups[8].Value != "N")
                {
                    var tendency = "";
                    switch (match.Groups[8].Value)
                    {
                        case "U":
                            tendency = "going up";
                            break;
                        case "D":
                            tendency = "going down";
                            break;
                    }
                    result.Add(tendency);
                }

                tts.Add($"Runway {rwyNumber.ToSerialForm()} {rwyDesignator} R-V-R {string.Join(" ", result)}");
            }
        }

        TextAtis = string.Join(" ", acars);
        VoiceAtis = string.Join(" ", tts).TrimEnd('.');
    }

    public override string ParseTextVariables(RunwayVisualRange node, string format)
    {
        throw new System.NotImplementedException();
    }

    public override string ParseVoiceVariables(RunwayVisualRange node, string format)
    {
        throw new System.NotImplementedException();
    }
}