using System.Collections.Generic;
using Vatsim.Vatis.MetarParser.ChunkDecoder.Abstract;

namespace Vatsim.Vatis.MetarParser.ChunkDecoder;

public class RemarksChunkDecoder : MetarChunkDecoder
{
    public const string RemarksParameterName = "Remarks";
    public const string RegexPattern = @"^(?:\(.*\))?(?: RMK(.*?)\.)";

    public override string GetRegex()
    {
        return RegexPattern;
    }

    public override Dictionary<string, object> Parse(string remainingMetar, bool withCavok = false)
    {
        var consumed = Consume(remainingMetar);
        var found = consumed.Value;
        var newRemainingMetar = consumed.Key;
        var result = new Dictionary<string, object>();

        if (found.Count > 1)
        {
            if(!string.IsNullOrEmpty(found[1].Value))
            {
                result.Add(RemarksParameterName, found[1].Value);
            }
            //var remarks = new Remark();
            //string[] split = found[1].Value.Split(new char[] { ' ' }, StringSplitOptions.None);
            //foreach (var r in split)
            //{
            //    if (!string.IsNullOrEmpty(r))
            //    {
            //        remarks.AddRemark(r);
            //    }
            //}
            //result.Add(RemarksParameterName, remarks);
        }

        return GetResults(newRemainingMetar, result);
    }
}