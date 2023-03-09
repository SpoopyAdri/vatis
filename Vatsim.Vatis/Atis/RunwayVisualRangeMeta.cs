using System.Collections.Generic;
using System.Text.RegularExpressions;
using Vatsim.Vatis.Common;
using Vatsim.Vatis.MetarParser.Entity;

namespace Vatsim.Vatis.Atis;

public class RunwayVisualRangeMeta : AtisMeta
{
    public override void Parse(DecodedMetar metar)
    {
        List<string> tts = new List<string>();
        List<string> acars = new List<string>();

        if (metar.RunwaysVisualRange != null)
        {
            foreach (RunwayVisualRange rvr in metar.RunwaysVisualRange)
            {
                Match m = Regex.Match(rvr.Raw, @"( ?R(\d{2}(R|L|C)?)\/(M|P)?(\d{4})(V(\d{4}))?(VP(\d{4}))?(FT|U|N|D)?)");
                if (m.Success)
                {
                    acars.Add(rvr.Raw);

                    var vis = "";
                    var rwyPosition = "";

                    switch (m.Groups[3].Value)
                    {
                        case "L":
                            rwyPosition = " left";
                            break;
                        case "R":
                            rwyPosition = " right";
                            break;
                        case "C":
                            rwyPosition = " center";
                            break;
                    }

                    var rwyNumber = m.Groups[2].Value.Replace("R", "").Replace("L", "").Replace("C", "");

                    if (m.Groups[6].Value.StartsWith("V"))
                    {
                        if (m.Groups[4].Value == "M")
                        {
                            int dist1 = int.Parse(m.Groups[5].Value);
                            int dist2 = int.Parse(m.Groups[7].Value);
                            vis = string.Format($" variable from less than {dist1.NumbersToWordsGroup()} to {dist2.NumbersToWordsGroup()}");
                        }
                        else
                        {
                            int dist1 = int.Parse(m.Groups[5].Value);
                            int dist2 = int.Parse(m.Groups[7].Value);
                            vis = string.Format($" variable between {dist1.NumbersToWordsGroup()} and {dist2.NumbersToWordsGroup()}");
                        }
                    }
                    else if (m.Groups[8].Value.StartsWith("VP"))
                    {
                        if (m.Groups[4].Value == "M")
                        {
                            int dist1 = int.Parse(m.Groups[5].Value);
                            int dist2 = int.Parse(m.Groups[9].Value);
                            vis = string.Format($" variable from less than {dist1.NumbersToWordsGroup()} to greater than {dist2.NumbersToWordsGroup()}");
                        }
                        else
                        {
                            int dist1 = int.Parse(m.Groups[5].Value);
                            int dist2 = int.Parse(m.Groups[9].Value);
                            vis = string.Format($" {dist1.NumbersToWords()} variable to greater than {dist2.NumbersToWordsGroup()}");
                        }
                    }
                    else
                    {
                        if (m.Groups[4].Value == "M")
                        {
                            int dist = int.Parse(m.Groups[5].Value);
                            vis += string.Format($" less than {dist.NumbersToWordsGroup()}");
                        }
                        else if (m.Groups[4].Value == "P")
                        {
                            int dist = int.Parse(m.Groups[5].Value);
                            vis += string.Format($" more than {dist.NumbersToWordsGroup()}");
                        }
                        else
                        {
                            int dist = int.Parse(m.Groups[5].Value);
                            vis += dist.NumbersToWordsGroup();
                        }
                    }

                    if (m.Groups[10].Value != "N")
                    {
                        string tendency = "";
                        switch (m.Groups[10].Value)
                        {
                            case "U":
                                tendency = " going up ";
                                break;
                            case "D":
                                tendency = " going down ";
                                break;
                        }
                        vis += tendency;
                    }

                    string ret = string.Format($"Runway {rwyNumber.NumberToSingular().Trim()}{rwyPosition} R-V-R {vis.Trim()}.");
                    tts.Add(ret);
                }
            }
        }

        Acars = string.Join(" ", acars);
        TextToSpeech = string.Join(" ", tts).TrimEnd('.');
    }
}