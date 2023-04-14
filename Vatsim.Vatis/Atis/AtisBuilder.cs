using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Serilog;
using Vatsim.Vatis.Atis.Nodes;
using Vatsim.Vatis.AudioForVatsim;
using Vatsim.Vatis.Io;
using Vatsim.Vatis.NavData;
using Vatsim.Vatis.Profiles;
using Vatsim.Vatis.TextToSpeech;
using Vatsim.Vatis.Utils;
using Vatsim.Vatis.Weather.Objects;

namespace Vatsim.Vatis.Atis;

public class AtisBuilder : IAtisBuilder
{
    private readonly INavaidDatabase mNavData;
    private readonly ITextToSpeechRequest mTextToSpeechRequest;
    private readonly IAudioManager mAudioManager;
    private readonly IDownloader mDownloader;

    public AtisBuilder(INavaidDatabase airportDatabase, ITextToSpeechRequest textToSpeechRequest, IAudioManager audioManager, IDownloader downloader)
    {
        mNavData = airportDatabase;
        mTextToSpeechRequest = textToSpeechRequest;
        mAudioManager = audioManager;
        mDownloader = downloader;
    }

    public void BuildTextAtis(Composite composite)
    {
        if (composite == null)
        {
            throw new Exception("Composite is null");
        }

        if (composite.CurrentPreset == null)
        {
            throw new Exception("CurrentPreset is null");
        }

        if (composite.DecodedMetar == null)
        {
            throw new Exception("DecodedMetar is null");
        }

        if (composite.AirportData == null)
        {
            composite.AirportData = mNavData.GetAirport(composite.Identifier) ?? throw new Exception($"{composite.Identifier} not found in airport database.");
        }

        ParseNodesFromMetar(composite, out string atisLetter, out List<AtisVariable> variables);

        var template = composite.CurrentPreset.Template;

        foreach (var variable in variables)
        {
            template = template.Replace($"[{variable.Find}:VOX]", variable.VoiceReplace);
            template = template.Replace($"${variable.Find}:VOX", variable.VoiceReplace);

            template = template.Replace($"[{variable.Find}]", variable.TextReplace);
            template = template.Replace($"${variable.Find}", variable.TextReplace);

            if (variable.Aliases != null)
            {
                foreach (var alias in variable.Aliases)
                {
                    template = template.Replace($"[{alias}:VOX]", variable.VoiceReplace);
                    template = template.Replace($"${alias}:VOX", variable.VoiceReplace);

                    template = template.Replace($"[{alias}]", variable.TextReplace);
                    template = template.Replace($"${alias}", variable.TextReplace);
                }
            }
        }

        template = Regex.Replace(template, @"\s+(?=[.,?!])", ""); // remove extra spaces before punctuation
        template = Regex.Replace(template, @"\s+", " ");
        template = Regex.Replace(template, @"(?<=\*)(-?[\,0-9]+)", "$1");
        template = Regex.Replace(template, @"(?<=\#)(-?[\,0-9]+)", "$1");
        template = Regex.Replace(template, @"(?<=\+)([A-Z]{3})", "$1");
        template = Regex.Replace(template, @"(?<=\+)([A-Z]{4})", "$1");

        if (!composite.CurrentPreset.HasClosingVariable && composite.AtisFormat.ClosingStatement.AutoIncludeClosingStatement)
        {
            var voiceTemplate = composite.AtisFormat.ClosingStatement.Template.Text;
            voiceTemplate = Regex.Replace(voiceTemplate, @"{letter}", composite.CurrentAtisLetter);
            voiceTemplate = Regex.Replace(voiceTemplate, @"{letter\|word}", atisLetter);
            template += voiceTemplate;
        }

        composite.TextAtis = template;
    }

    public async Task BuildVoiceAtis(Composite composite, CancellationToken cancellationToken)
    {
        if (composite == null)
        {
            throw new Exception("Composite is null");
        }

        if (composite.CurrentPreset == null)
        {
            throw new Exception("CurrentPreset is null");
        }

        if (composite.DecodedMetar == null)
        {
            throw new Exception("DecodedMetar is null");
        }

        composite.AirportData = mNavData.GetAirport(composite.Identifier) ?? throw new Exception($"{composite.Identifier} not found in airport database.");

        ParseNodesFromMetar(composite, out string atisLetter, out List<AtisVariable> variables);

        // build ATIS using external source
        if (composite.CurrentPreset.ExternalGenerator.Enabled)
        {
            var externalAtis = await BuildAtisFromExternalSource(composite, composite.DecodedMetar, variables);

            if (externalAtis == null)
            {
                throw new Exception("Failed to create external ATIS");
            }

            composite.TextAtis = externalAtis.ToUpper();

            return;
        }

        // build standard ATIS
        BuildTextAtis(composite);

        var template = composite.CurrentPreset.Template;

        foreach (var variable in variables)
        {
            template = template.Replace($"[{variable.Find}:VOX]", variable.VoiceReplace);
            template = template.Replace($"${variable.Find}:VOX", variable.VoiceReplace);

            template = template.Replace($"[{variable.Find}]", variable.VoiceReplace);
            template = template.Replace($"${variable.Find}", variable.VoiceReplace);

            if (variable.Aliases != null)
            {
                foreach (var alias in variable.Aliases)
                {
                    template = template.Replace($"[{alias}:VOX]", variable.VoiceReplace);
                    template = template.Replace($"${alias}:VOX", variable.VoiceReplace);

                    template = template.Replace($"[{alias}]", variable.VoiceReplace);
                    template = template.Replace($"${alias}", variable.VoiceReplace);
                }
            }
        }

        if (!composite.CurrentPreset.HasClosingVariable && composite.AtisFormat.ClosingStatement.AutoIncludeClosingStatement)
        {
            var voiceTemplate = composite.AtisFormat.ClosingStatement.Template.Voice;
            voiceTemplate = Regex.Replace(voiceTemplate, @"{letter}", composite.CurrentAtisLetter);
            voiceTemplate = Regex.Replace(voiceTemplate, @"{letter\|word}", atisLetter);
            template += voiceTemplate;
        }

        if (composite.AtisVoice.UseTextToSpeech)
        {
            var text = FormatForTextToSpeech(template.ToUpper(), composite);
            text = Regex.Replace(text, @"[!?.]*([!?.])", "$1"); // clean up duplicate punctuation one last time
            text = Regex.Replace(text, "\\s+([.,!\":])", "$1");

            // catches multiple ATIS letter button presses in quick succession
            await Task.Delay(5000, cancellationToken);

            var synthesizedAudio = await mTextToSpeechRequest.RequestSynthesizedText(text, cancellationToken);

            if (synthesizedAudio != null)
            {
                await UpdateIds(composite, cancellationToken);

                await mAudioManager.AddOrUpdateBot(synthesizedAudio, composite.AtisCallsign, composite.AfvFrequency, composite.AirportData.Latitude, composite.AirportData.Longitude);
            }
        }
        else
        {
            if (composite.RecordedMemoryStream != null)
            {
                await UpdateIds(composite, cancellationToken);

                await mAudioManager.AddOrUpdateBot(composite.RecordedMemoryStream.ToArray(), composite.AtisCallsign, composite.AfvFrequency, composite.AirportData.Latitude, composite.AirportData.Longitude);
            }
        }
    }

    public async Task UpdateIds(Composite composite, CancellationToken cancellationToken)
    {
        if (Debugger.IsAttached)
            return;

        if (string.IsNullOrEmpty(composite.IDSEndpoint) || composite.CurrentPreset == null)
            return;

        var json = new IdsUpdateRequest
        {
            Facility = composite.Identifier,
            Preset = composite.CurrentPreset.Name,
            AtisLetter = composite.CurrentAtisLetter,
            AirportConditions = composite.CurrentPreset.AirportConditions.StripNewLineChars(),
            Notams = composite.CurrentPreset.Notams.StripNewLineChars(),
            Timestamp = DateTime.UtcNow,
            Version = Assembly.GetExecutingAssembly().GetName().Version.ToString(),
            AtisType = composite.AtisType.ToString().ToLowerInvariant()
        };

        try
        {
            await mDownloader.PostJsonAsync(composite.IDSEndpoint, json, cancellationToken);
        }
        catch (TaskCanceledException) { }
        catch (HttpRequestException ex)
        {
            Log.Error(ex.ToString());
        }
        catch (Exception ex)
        {
            throw new Exception("PostIdsUpdate Error: " + ex.Message);
        }
    }

    private async Task<string> BuildAtisFromExternalSource(Composite composite, Metar metar, List<AtisVariable> variables)
    {
        if (composite == null)
        {
            throw new Exception("Composite is null");
        }

        if (composite.CurrentPreset == null)
        {
            throw new Exception("CurrentPreset is null");
        }

        if (metar == null)
        {
            throw new Exception("Metar is null");
        }

        var preset = composite.CurrentPreset;
        var data = preset.ExternalGenerator;

        if (data == null)
        {
            throw new Exception("ExternalGenerator is null");
        }

        var url = data.Url;

        if (!string.IsNullOrEmpty(url))
        {
            url = url.Replace("$metar", System.Web.HttpUtility.UrlEncode(metar.RawMetar));
            url = url.Replace("$arrrwy", data.Arrival);
            url = url.Replace("$deprwy", data.Departure);
            url = url.Replace("$app", data.Approaches);
            url = url.Replace("$remarks", data.Remarks);
            url = url.Replace("$atiscode", composite.CurrentAtisLetter);

            var aptcond = variables.FirstOrDefault(x => x.Find == "ARPT_COND");
            if (aptcond != null)
            {
                url = url.Replace("$aptcond", aptcond.TextReplace);
            }

            var notams = variables.FirstOrDefault(x => x.Find == "NOTAMS");
            if (notams != null)
            {
                url = url.Replace("$notams", notams.TextReplace);
            }

            try
            {
                var response = await mDownloader.DownloadStringAsync(url);
                response = Regex.Replace(response, @"\[(.*?)\]", " $1 ");
                response = Regex.Replace(response, @"\s+", " ");
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("External ATIS Error: " + ex.Message);
            }
        }

        return null;
    }

    private void ParseNodesFromMetar(Composite composite, out string atisLetter, out List<AtisVariable> variables)
    {
        if (composite.DecodedMetar == null)
        {
            atisLetter = null;
            variables = null;
            return;
        }

        var metar = composite.DecodedMetar;
        var time = NodeParser.Parse<ObservationTimeNode, ObservationDayTime>(metar, composite);
        var surfaceWind = NodeParser.Parse<SurfaceWindNode, SurfaceWind>(metar, composite);
        var rvr = NodeParser.Parse<RunwayVisualRangeNode, RunwayVisualRange>(metar, composite);
        var visibility = NodeParser.Parse<PrevailingVisibilityNode, PrevailingVisibility>(metar, composite);
        var presentWeather = NodeParser.Parse<PresentWeatherNode, WeatherPhenomena>(metar, composite);
        var clouds = NodeParser.Parse<CloudNode, CloudLayer>(metar, composite);
        var temp = NodeParser.Parse<TemperatureNode, TemperatureInfo>(metar, composite);
        var dew = NodeParser.Parse<DewpointNode, TemperatureInfo>(metar, composite);
        var pressure = NodeParser.Parse<AltimeterSettingNode, AltimeterSetting>(metar, composite);
        var trends = NodeParser.Parse<TrendNode, Trend>(metar, composite);

        atisLetter = char.Parse(composite.CurrentAtisLetter).LetterToPhonetic();
        var completeWxStringVoice = $"{surfaceWind.VoiceAtis} {visibility.VoiceAtis} {rvr.VoiceAtis} {presentWeather.VoiceAtis} {clouds.VoiceAtis} {temp.VoiceAtis} {dew.VoiceAtis} {pressure.VoiceAtis}";
        var completeWxStringAcars = $"{surfaceWind.TextAtis} {visibility.TextAtis} {rvr.TextAtis} {presentWeather.TextAtis} {clouds.TextAtis} {temp.TextAtis}{(!string.IsNullOrEmpty(temp.TextAtis) || !string.IsNullOrEmpty(dew.TextAtis) ? "/" : "")}{dew.TextAtis} {pressure.TextAtis}";

        var airportConditions = "";
        if (!string.IsNullOrEmpty(composite.CurrentPreset.AirportConditions) || composite.AirportConditionDefinitions.Any(t => t.Enabled))
        {
            if (composite.AirportConditionsBeforeFreeText)
            {
                airportConditions = string.Join(" ", new[] { string.Join(". ", composite.AirportConditionDefinitions.Where(t => t.Enabled).Select(t => t.Text)), composite.CurrentPreset.AirportConditions });
            }
            else
            {
                airportConditions = string.Join(" ", new[] { composite.CurrentPreset.AirportConditions, string.Join(". ", composite.AirportConditionDefinitions.Where(t => t.Enabled).Select(t => t.Text)) });
            }
        }

        airportConditions = Regex.Replace(airportConditions, @"[!?.]*([!?.])", "$1"); // clean up duplicate punctuation
        airportConditions = Regex.Replace(airportConditions, "\\s+([.,!\":])", "$1");

        var notamVoice = "";
        var notamText = "";
        if (!string.IsNullOrEmpty(composite.CurrentPreset.Notams) || composite.NotamDefinitions.Any(t => t.Enabled))
        {
            if (composite.UseNotamPrefix)
            {
                notamVoice = composite.IsFaaAtis ? "Notices to air missions. " : "Notices to airmen. ";
            }

            if (composite.NotamsBeforeFreeText)
            {
                notamText = string.Join(" ", new[] { string.Join(". ", composite.NotamDefinitions.Where(t => t.Enabled).Select(t => t.Text)), composite.CurrentPreset.Notams });
                notamVoice += string.Join(" ", new[] { string.Join(". ", composite.NotamDefinitions.Where(t => t.Enabled).Select(t => t.Text)), composite.CurrentPreset.Notams });
            }
            else
            {
                notamText = string.Join(". ", new[] { composite.CurrentPreset.Notams, string.Join(" ", composite.NotamDefinitions.Where(t => t.Enabled).Select(t => t.Text)) });
                notamVoice += string.Join(" ", new[] { composite.CurrentPreset.Notams, string.Join(". ", composite.NotamDefinitions.Where(t => t.Enabled).Select(t => t.Text)) });
            }
        }

        notamVoice = Regex.Replace(notamVoice, @"[!?.]*([!?.])", "$1"); // clean up duplicate punctuation
        notamVoice = Regex.Replace(notamVoice, "\\s+([.,!\":])", "$1");
        notamText = Regex.Replace(notamText, @"[!?.]*([!?.])", "$1"); // clean up duplicate punctuation
        notamText = Regex.Replace(notamText, "\\s+([.,!\":])", "$1");

        if (!string.IsNullOrEmpty(notamText) && composite.IsFaaAtis)
        {
            notamText = "NOTAMS... " + notamText;
        }

        var transitionLevelVoice = "";
        var transitionLevelText = "";
        if (!composite.IsFaaAtis)
        {
            transitionLevelText = "TL N/A";
            transitionLevelVoice = "Transition level not determined";
            if (composite.TransitionLevels != null)
            {
                var myMetar = metar;
                var tlValue = composite.TransitionLevels.FirstOrDefault(t =>
                {
                    return myMetar.AltimeterSetting.Value >= t.Low
                        && myMetar.AltimeterSetting.Value <= t.High;
                });

                if (tlValue != null)
                {
                    transitionLevelText = $"Transition level " +
                                          $"{(composite.UseTransitionLevelPrefix ? "FL " : "")}" +
                                          $"{tlValue.Altitude}";

                    transitionLevelVoice = composite.UseTransitionLevelPrefix
                        ? $"Transition level, flight level {tlValue.Altitude.NumberToSingular()}"
                        : $"Transition level {tlValue.Altitude.NumberToSingular()}";
                }
            }
        }

        variables = new List<AtisVariable>
        {
            new AtisVariable("FACILITY", composite.AirportData.ID, composite.AirportData.Name),
            new AtisVariable("ATIS_LETTER", composite.CurrentAtisLetter, atisLetter,  new [] {"LETTER","ATIS_CODE","ID"}),
            new AtisVariable("TIME", time.TextAtis, time.VoiceAtis, new []{"OBS_TIME","OBSTIME"}),
            new AtisVariable("WIND", surfaceWind.TextAtis, surfaceWind.VoiceAtis, new[]{"SURFACE_WIND"}),
            new AtisVariable("RVR", rvr.TextAtis, rvr.VoiceAtis),
            new AtisVariable("VIS", visibility.TextAtis, visibility.VoiceAtis, new[]{"PREVAILING_VISIBILITY"}),
            new AtisVariable("PRESENT_WX", presentWeather.TextAtis, presentWeather.VoiceAtis, new[]{"PRESENT_WEATHER"}),
            new AtisVariable("CLOUDS", clouds.TextAtis, clouds.VoiceAtis),
            new AtisVariable("TEMP", temp.TextAtis, temp.VoiceAtis),
            new AtisVariable("DEW", dew.TextAtis, dew.VoiceAtis),
            new AtisVariable("PRESSURE", pressure.TextAtis, pressure.VoiceAtis, new[]{"QNH"}),
            new AtisVariable("WX", completeWxStringAcars, completeWxStringVoice, new[]{"FULL_WX_STRING"}),
            new AtisVariable("ARPT_COND", airportConditions, airportConditions, new[]{"ARRDEP"}),
            new AtisVariable("NOTAMS", notamText, notamVoice),
            new AtisVariable("TREND", trends.TextAtis, trends.VoiceAtis),
            new AtisVariable("TL", transitionLevelText, transitionLevelVoice)
        };

        var closingTextTemplate = composite.AtisFormat.ClosingStatement.Template.Text;
        var closingVoiceTemplate = composite.AtisFormat.ClosingStatement.Template.Voice;

        if (!string.IsNullOrEmpty(closingTextTemplate) && !string.IsNullOrEmpty(closingVoiceTemplate))
        {
            closingTextTemplate = Regex.Replace(closingTextTemplate, @"{letter}", composite.CurrentAtisLetter);
            closingTextTemplate = Regex.Replace(closingTextTemplate, @"{letter\|word}", atisLetter);

            closingVoiceTemplate = Regex.Replace(closingVoiceTemplate, @"{letter}", atisLetter);
            closingVoiceTemplate = Regex.Replace(closingVoiceTemplate, @"{letter\|word}", atisLetter);

            variables.Add(new AtisVariable("CLOSING", closingTextTemplate, closingVoiceTemplate));
        }
        else
        {
            variables.Add(new AtisVariable("CLOSING", "", ""));
        }
    }

    private string FormatForTextToSpeech(string input, Composite composite)
    {
        // parse zulu times
        input = Regex.Replace(input, @"([0-9])([0-9])([0-9])([0-8])Z",
            m => string.Format($"{int.Parse(m.Groups[1].Value).NumberToSingular()} " +
                               $"{int.Parse(m.Groups[2].Value).NumberToSingular()} " +
                               $"{int.Parse(m.Groups[3].Value).NumberToSingular()} " +
                               $"{int.Parse(m.Groups[4].Value).NumberToSingular()} zulu"));

        // vhf frequencies
        input = Regex.Replace(input, @"(1\d\d\.\d\d?\d?)", m => m.Groups[1].Value.NumberToSingular(composite.UseDecimalTerminology));

        // letters
        input = Regex.Replace(input, @"\*([A-Z]{1,2}[0-9]{0,2})", m => string.Format("{0}", m.Value.ConvertAlphaNumericToWordGroup())).Trim();

        // parse taxiways
        input = Regex.Replace(input, @"\bTWY ([A-Z]{1,2}[0-9]{0,2})\b", m => $"TWY {m.Groups[1].Value.ConvertAlphaNumericToWordGroup()}");
        input = Regex.Replace(input, @"\bTWYS ([A-Z]{1,2}[0-9]{0,2})\b", m => $"TWYS {m.Groups[1].Value.ConvertAlphaNumericToWordGroup()}");

        // parse runways
        input = Regex.Replace(input, @"\b(RY|RWY|RWYS|RUNWAY|RUNWAYS)\s?([0-9]{1,2})([LRC]?)\b", m =>
            StringExtensions.RwyNumbersToWords(int.Parse(m.Groups[2].Value), m.Groups[3].Value,
                prefix: !string.IsNullOrEmpty(m.Groups[1].Value),
                plural: !string.IsNullOrEmpty(m.Groups[1].Value) &&
                        (m.Groups[1].Value == "RWYS" || m.Groups[1].Value == "RUNWAYS"),
                leadingZero: !composite.IsFaaAtis));

        // read numbers in group format, prefixed with # or surrounded with {}
        input = Regex.Replace(input, @"\*(-?[\,0-9]+)", m => int.Parse(m.Groups[1].Value.Replace(",", "")).NumbersToWordsGroup());
        input = Regex.Replace(input, @"\{(-?[\,0-9]+)\}", m => int.Parse(m.Groups[1].Value.Replace(",", "")).NumbersToWordsGroup());

        // read numbers in serial format
        input = Regex.Replace(input, @"([+-])?([0-9]+\.?[0-9]*|\.[0-9]+)(?![^{]*\})", m => m.Value.NumberToSingular(composite.UseDecimalTerminology));

        // user defined contractions
        foreach (var x in composite.Contractions)
        {
            input = input.SafeReplace(x.String.ToUpper(), x.Spoken.ToUpper(), true);
        }

        // default contractions
        foreach (var word in Translations)
        {
            input = input.SafeReplace(word.Key.ToUpper(), word.Value.ToUpper(), true);
        }

        // format navaids identifiers
        var navaids = Regex.Matches(input, @"(?<=\+)([A-Z]{3})");
        if (navaids.Count > 0)
        {
            foreach (var m in navaids.Where(m => m.Success))
            {
                try
                {
                    var find = mNavData.GetNavaid(m.Value);
                    if (find != null)
                    {
                        input = Regex
                            .Replace(input, $@"\b(?<=\+){m.Value}\b", x => find.Name)
                            .Replace("+", "");
                    }
                }
                catch { }
            }
        }

        // format airport identifiers
        var airports = Regex.Matches(input, @"(?<=\+)([A-Z0-9]{4})");
        if (airports.Count > 0)
        {
            foreach (var m in airports.Where(m => m.Success))
            {
                try
                {
                    var find = mNavData.GetAirport(m.Value);
                    if (find != null)
                    {
                        input = Regex
                            .Replace(input, $@"\b(?<=\+){m.Value}\b", x => find.Name)
                            .Replace("+", "");
                    }
                }
                catch { }
            }
        }

        input = Regex.Replace(input, @"(?<=\*)(-?[\,0-9]+)", "$1");
        input = Regex.Replace(input, @"(?<=\#)(-?[\,0-9]+)", "$1");
        input = Regex.Replace(input, @"\{(-?[\,0-9]+)\}", "$1");
        input = Regex.Replace(input, @"(?<=\+)([A-Z]{3})", "$1");
        input = Regex.Replace(input, @"(?<=\+)([A-Z]{4})", "$1");
        input = Regex.Replace(input, @"[!?.]*([!?.])", "$1 "); // clean up duplicate punctuation
        input = Regex.Replace(input, "\\s+([.,!\":])", "$1 ");
        input = Regex.Replace(input, @"\s+", " ");
        input = Regex.Replace(input, @"\s\,", ",");
        input = Regex.Replace(input, @"\&", "and");
        input = Regex.Replace(input, @"\*", "");

        return input.ToUpper();
    }

    private static Dictionary<string, string> Translations => new Dictionary<string, string>
    {
        {"ACFT", "AIRCRAFT"},
        {"ADVS", "ADVISE"},
        {"ADVSD", "ADVISED"},
        {"ADVZY", "ADVISORY"},
        {"ADVZYS", "ADVISORIES"},
        {"ALS", "APPROACH LIGHTING SYSTEM"},
        {"ALT", "ALTITUDE"},
        {"ALTS", "ALTITUDES"},
        {"APCH", "APPROACH"},
        {"APCHS", "APPROACHES"},
        {"APP", "APPROACH"},
        {"APPR", "APPROACH"},
        {"APPRS", "APPROACHES"},
        {"APPS", "APPROACHES"},
        {"ARPT", "AIRPORT"},
        {"ARPTS", "AIRPORTS"},
        {"ARR", "ARRIVAL"},
        {"ARRS", "ARRIVALS"},
        {"ATTN", "ATTENTION"},
        {"AUTH", "AUTHORIZED"},
        {"AVBL", "AVAILABLE"},
        {"BA", "BRAKING ACTION"},
        {"BAA", "BRAKING ACTION ADVISORIES"},
        {"BC", "BACKCOURSE"},
        {"BTWN", "BETWEEN"},
        {"CAUT", "CAUTION"},
        {"CLNC", "CLEARANCE"},
        {"CLR", "CLEAR"},
        {"CLRD", "CLEARED"},
        {"CLSD", "CLOSED"},
        {"CMSN", "COMMISSION"},
        {"CMSND", "COMMISSIONED"},
        {"CTC", "CONTACT"},
        {"CTL", "CONTROL"},
        {"CTLD", "CONTROLLED"},
        {"CD", "CLEARANCE DELIVERY"},
        {"DCMSN", "DE-COMISSIONED"},
        {"DCMSND", "DE-COMISSIONED"},
        {"DEP", "DEPARTURE"},
        {"DEPS", "DEPARTURES"},
        {"DEPTS", "DEPARTURES"},
        {"DEPTG", "DEPARTING"},
        {"DIST", "DISTANCE"},
        {"DRCTN", "DIRECTION"},
        {"DURG", "DURING"},
        {"DURN", "DURATION"},
        {"EFCT", "EFFECT"},
        {"EXPC", "EXPECT"},
        {"EFF", "EFFECTIVE"},
        {"EQPT", "EQUIPMENT"},
        {"EXPT", "EXPECT"},
        {"FLT", "FLIGHT"},
        {"FREQ", "FREQUENCY"},
        {"FT", "FEET"},
        {"GND", "GROUND"},
        {"GS", "GLIDE-SLOPE"},
        {"HDG", "HEADING"},
        {"HDGS", "HEADINGS"},
        {"HELI", "HELICOPTER"},
        {"HS", "HOLD SHORT"},
        {"HAZD WX INFO", "HAZARDOUS WEATHER INFORMATION"},
        {"INBD", "INBOUND"},
        {"INTXN", "INTERSECTION"},
        {"INST", "INSTRUMENT"},
        {"INVOF", "IN VICINITY OF"},
        {"LAHSO", "LAND AND HOLD SHORT OPERATIONS"},
        {"LDG", "LANDING"},
        {"LGT", "LIGHT"},
        {"LGTD", "LIGHTED"},
        {"LGTS", "LIGHTS"},
        {"LLWS", "LOW LEVEL WIND SHEAR"},
        {"LLZ", "LOCALIZER"},
        {"LOC", "LOCALIZER"},
        {"MOD", "MODERATE"},
        {"MULTI", "MULTIPLE"},
        {"NA", "NOT AUTHORIZED"},
        {"NOTAM", "NO-TAM"},
        {"NOTAMS", "NO-TAMS"},
        {"OPER", "OPERATE"},
        {"OPS", "OPERATIONS"},
        {"OTS", "OUT OF SERVICE"},
        {"OUBD", "OUTBOUND"},
        {"PIREP", "PILOT WEATHER REPORT"},
        {"PROC", "PROCEDURE"},
        {"PROG", "PROGRESS"},
        {"RMNG", "REMAINING"},
        {"RMVL", "REMOVAL"},
        {"RQST", "REQUEST"},
        {"RQSTD", "REQUESTED"},
        {"RWY", "RUNWAY"},
        {"RWYS", "RUNWAYS"},
        {"SIMUL", "SIMULTANEOUS"},
        {"SVC", "SERVICE"},
        {"SVCS", "SERVICES"},
        {"SVR", "SEVERE"},
        {"TFC", "TRAFFIC"},
        {"TFR", "TEMPORARY FLIGHT RESTRICTION"},
        {"TURB", "TURBULENCE"},
        {"TWY", "TAXIWAY"},
        {"TWYS", "TAXIWAYS"},
        {"US", "UNSERVICEABLE"},
        {"UNCTLD", "UNCONTROLLED"},
        {"UNUSBL", "UNUSABLE"},
        {"USBL", "USABLE"},
        {"VFY", "VERIFY"},
        {"VCTR", "VECTOR"},
        {"VCTRS", "VECTORS"},
        {"VIS", "VISUAL"},
        {"XPDR", "TRANSPONDER"},
        {"XPDRS", "TRANSPONDERS"},
        {"XPNDR", "TRANSPONDER"},
        {"XPNDRS", "TRANSPONDERS"},
        {"XTM", "EXTREME"},
        {"N", "NORTH"},
        {"NNE", "NORTH NORTHEAST"},
        {"NE", "NORTHEAST"},
        {"ENE", "EAST NORTHEAST"},
        {"E", "EAST"},
        {"ESE", "EAST SOUTHEAST"},
        {"SE", "SOUTHEAST"},
        {"SSE", "SOUTH SOUTHEAST"},
        {"S", "SOUTH"},
        {"SSW", "SOUTH SOUTHWEST"},
        {"SW", "SOUTHWEST"},
        {"WSW", "WEST SOUTHWEST"},
        {"W", "WEST"},
        {"WNW", "WEST NORTHWEST"},
        {"NW", "NORTHWEST"},
        {"NNW", "NORTH NORTHWEST"},
        {"READBACK", "REEDBACK"},
        {"READBACKS", "REEDBACKS"},
        {"ATIS", "ATE-IS"},
        {"RNAV", "AREA NAVIGATION"},
        {"INFO", "INFORMATION"},
        {"WIND", "WEND"},
        {"CALLSIGN", "CALL-SIGN"},
        {"ILS", "EYE EL ESS"},
        {"DEPG", "DEPARTING"},
        {"ARVNG", "ARRIVING"},
        {"VOR", "V-O-R"},
        {"HIWAS", "HIGH-WAZ"},
        {"FM", "FROM"},
        {"RY", "RUNWAY"},
        {"WX", "WEATHER"},
        {"FSS", "FLIGHT SERVICE STATION"},
        {"HAZD", "HAZARDOUS"},
        {"WPT", "WAYPOINT"},
        {"BTN", "BETWEEN" },
        {"TWR", "TOWER" },
        {"ADZ", "ADVISE" },
        {"MSL", "MEAN SEA LEVEL" },
        {"RLS", "RELEASE" },
        {"LNDG", "LANDING" },
        {"NUM", "NUMEROUS" },
        {"OAT", "OUTSIDE AIR TEMPERATURE" },
        {"RPTD", "REPORTED" },
        {"PAPI", "PRECISION APPROACH PATH INDICATOR" },
        {"NM", "NAUTICAL MILE" },
        {"LND", "LAND" },
        {"SERV", "SERVICE" },
        {"BIV", "BIRD ACTIVITY VICINITY" },
        {"AVLB", "AVAILABLE" },
        {"VCNTY", "VICINITY" },
        {"ACKMTS", "ACKNOWLEDGE-MENTS" },
        {"APRT", "AIRPORT" },
        {"RDBKS", "READBACKS" },
        {"SHRT", "SHORT" },
        {"EXP", "EXPECT" },
        {"GC", "GROUND CONTROL" },
        {"CLSGN", "CALL-SIGN" },
        {"VA", "VISUAL APPROACH" },
        {"INCL", "INCLUDE" },
        {"TRNSPNDR", "TRANSPONDER" },
        {"RQRD", "REQUIRED" },
        {"ACTVT", "ACTIVATE" },
        {"TYS", "TAXIWAYS" },
        {"INT", "INTERSECTION" },
        {"MI", "MILE" },
        {"UFN", "UNTIL FURTHER NOTICE" },
        {"SFC", "SURFACE" },
        {"TODA", "TAKE-OFF DISTANCE AVAILABLE" },
        {"VC", "VICINITY" },
        {"INSTR", "INSTRUMENT" },
        {"CNTD", "CONDUCTED" },
        {"INTL", "INTERNATIONAL" },
        {"ASDEX", "AS-DEX" },
        {"ASDE-X", "AS-DEX" }
    };
}