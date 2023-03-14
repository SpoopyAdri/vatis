using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Vatsim.Vatis.Common;
using Vatsim.Vatis.Weather.Objects;

namespace Vatsim.Vatis.Atis;

public class RunwayVisualRangeMeta : AtisMeta
{
    public override void Parse(Metar metar)
    {
        var tts = new List<string>();
        var acars = new List<string>();

        if (metar.RunwayVisualRanges == null)
            return;

        foreach(var rvr in metar.RunwayVisualRanges)
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
                        result.Add($"variable from less than {minVis.NumbersToWordsGroup()} to {maxVis.NumbersToWordsGroup()}");
                    }
                    else
                    {
                        result.Add($"variable between {minVis.NumbersToWordsGroup()} and {maxVis.NumbersToWordsGroup()}");
                    }
                }
                else if (match.Groups[5].Value == "VP")
                {
                    var minVis = int.Parse(match.Groups[4].Value);
                    var maxVis = int.Parse(match.Groups[6].Value);

                    if (match.Groups[3].Value == "M")
                    {
                        result.Add($"variable from less than {minVis.NumbersToWordsGroup()} to greater than{maxVis.NumbersToWordsGroup()}");
                    }
                    else
                    {
                        result.Add($"{minVis.NumbersToWordsGroup()} variable to greater than {maxVis.NumbersToWordsGroup()}");
                    }
                }
                else
                {
                    var vis = int.Parse(match.Groups[4].Value);

                    if (match.Groups[3].Value == "M")
                    {
                        result.Add($"less than {vis.NumbersToWordsGroup()}");
                    }
                    else if (match.Groups[3].Value == "P")
                    {
                        result.Add($"more than {vis.NumbersToWordsGroup()}");
                    }
                    else
                    {
                        result.Add(vis.NumbersToWordsGroup());
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

                tts.Add($"Runway {rwyNumber.NumberToSingular()} {rwyDesignator} R-V-R {string.Join(" ", result)}");
            }
        }

        TextAtis = string.Join(" ", acars);
        VoiceAtis = string.Join(" ", tts).TrimEnd('.');
    }
}