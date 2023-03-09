using System.Collections.Generic;
using Vatsim.Vatis.MetarParser.ChunkDecoder.Abstract;
using Vatsim.Vatis.MetarParser.Entity;

namespace Vatsim.Vatis.MetarParser.ChunkDecoder;

public sealed class RunwayVisualRangeChunkDecoder : MetarChunkDecoder
{
    public const string RunwaysVisualRangeParameterName = "RunwaysVisualRange";
    public const string RunwayRegexPattern = @"( ?R(\d{2}(R|L|C)?)\/(M|P)?(\d{4})(V(\d{4}))?(VP(\d{4}))?(FT)?\/?(U|N|D)?)";

    public override string GetRegex()
    {
        return RunwayRegexPattern;
    }

    public override Dictionary<string, object> Parse(string remainingMetar, bool withCavok = false)
    {
        var consumed = ConsumeMulti(remainingMetar);
        var found = consumed.Value;
        var newRemainingMetar = consumed.Key;
        var result = new Dictionary<string, object>();

        if (found.Count > 0)
        {
            var runways = new List<RunwayVisualRange>();

            foreach (var match in found)
            {
                runways.Add(new RunwayVisualRange { Raw = match.Value });
            }

            result.Add(RunwaysVisualRangeParameterName, runways);
        }

        return GetResults(newRemainingMetar, result);
    }
}