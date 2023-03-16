using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RestSharp;
using Vatsim.Vatis.AudioForVatsim;
using Vatsim.Vatis.Config;
using Vatsim.Vatis.NavData;
using Vatsim.Vatis.TextToSpeech;
using Vatsim.Vatis.UI.Dialogs;
using Vatsim.Vatis.Utils;
using Vatsim.Vatis.Weather.Objects;

namespace Vatsim.Vatis.Atis;

public class AtisBuilder : IAtisBuilder 
{
    private readonly INavaidDatabase mNavData;
    private readonly ITextToSpeechRequest mTextToSpeechRequest;
    private readonly IAudioManager mAudioManager;
    private Airport mAirport;

    public AtisBuilder(INavaidDatabase airportDatabase, ITextToSpeechRequest textToSpeechRequest, IAudioManager audioManager)
    {
        mNavData = airportDatabase;
        mTextToSpeechRequest = textToSpeechRequest;
        mAudioManager = audioManager;
    }

    public async Task BuildAtisAsync(AtisComposite composite, CancellationToken cancellationToken)
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

        mAirport = mNavData.GetAirport(composite.Identifier);
        if (mAirport == null)
        {
            throw new Exception($"{composite.Identifier} not found in airport database.");
        }

        ParseMetar(composite, out Metar metar, out string atisLetter, out List<Variable> variables);

        GenerateAcarsText(composite);

        var voiceString = new StringBuilder();

        if (composite.UseExternalAtisGenerator)
        {
            var result = BuildExternalAtis(composite, metar, variables);

            if (result == null)
                throw new Exception("Failed to create external ATIS");

            voiceString = new StringBuilder(result);
        }
        else
        {
            voiceString = new StringBuilder(composite.CurrentPreset.Template);

            foreach (var variable in variables)
            {
                voiceString.Replace($"[{variable.Find}:VOX]", variable.VoiceReplace);
                voiceString.Replace($"${variable.Find}:VOX", variable.VoiceReplace);

                voiceString.Replace($"[{variable.Find}]", variable.VoiceReplace);
                voiceString.Replace($"${variable.Find}", variable.VoiceReplace);

                if (variable.Aliases != null)
                {
                    foreach (var alias in variable.Aliases)
                    {
                        voiceString.Replace($"[{alias}:VOX]", variable.VoiceReplace);
                        voiceString.Replace($"${alias}:VOX", variable.VoiceReplace);

                        voiceString.Replace($"[{alias}]", variable.VoiceReplace);
                        voiceString.Replace($"${alias}", variable.VoiceReplace);
                    }
                }
            }
        }

        if (composite.AtisVoice.UseTextToSpeech)
        {
            var tts = FormatForTextToSpeech(voiceString.ToString().ToUpper(), composite);

            tts = Regex.Replace(tts, @"[!?.]*([!?.])", "$1"); // clean up duplicate punctuation one last time
            tts = Regex.Replace(tts, "\\s+([.,!\":])", "$1");

            System.Diagnostics.Debug.WriteLine(tts);

            await Task.Run(() =>
            {
                Task.Delay(5000, cancellationToken); // catches multiple atis letter button presses in quick succession

                var response = mTextToSpeechRequest.RequestSynthesizedText(tts, cancellationToken);

                if (response.Result != null)
                {
                    mAudioManager.AddOrUpdateBot(response.Result, composite.AtisCallsign, composite.AfvFrequency, mAirport.Latitude, mAirport.Longitude);

                    PostIdsUpdate(composite, cancellationToken);
                }
            }, cancellationToken);
        }
        else
        {
            if (composite.MemoryStream != null)
            {
                await mAudioManager.AddOrUpdateBot(composite.MemoryStream.ToArray(), composite.AtisCallsign, composite.AfvFrequency, mAirport.Latitude, mAirport.Longitude);

                PostIdsUpdate(composite, cancellationToken);
            }
        }
    }

    private string BuildExternalAtis(AtisComposite composite, Metar metar, List<Variable> variables)
    {
        var preset = composite.CurrentPreset;
        var data = preset.ExternalGenerator;

        if (data == null)
            return null;

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

            var result = "";

            System.Diagnostics.Debug.WriteLine(url);

            var client = new RestClient();
            var request = new RestRequest(url);
            var response = client.Get<string>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                result = Regex.Replace(response.Content, @"\[(.*?)\]", " $1 ");
                result = Regex.Replace(result, @"\s+", " ");
                return result;
            }

            throw new Exception("External ATIS Error: " + response.StatusDescription);
        }

        return null;
    }

    public void GenerateAcarsText(AtisComposite composite)
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

        mAirport = mNavData.GetAirport(composite.Identifier);
        if (mAirport == null)
        {
            throw new Exception($"{composite.Identifier} not found in airport database.");
        }

        ParseMetar(composite, out Metar metar, out string atisLetter, out List<Variable> variables);

        var acarsText = composite.CurrentPreset.Template;

        foreach (var variable in variables)
        {
            acarsText = acarsText.Replace($"[{variable.Find}:VOX]", variable.VoiceReplace);
            acarsText = acarsText.Replace($"${variable.Find}:VOX", variable.VoiceReplace);

            acarsText = acarsText.Replace($"[{variable.Find}]", variable.TextReplace);
            acarsText = acarsText.Replace($"${variable.Find}", variable.TextReplace);

            if (variable.Aliases != null)
            {
                foreach (var alias in variable.Aliases)
                {
                    acarsText = acarsText.Replace($"[{alias}:VOX]", variable.VoiceReplace);
                    acarsText = acarsText.Replace($"${alias}:VOX", variable.VoiceReplace);

                    acarsText = acarsText.Replace($"[{alias}]", variable.TextReplace);
                    acarsText = acarsText.Replace($"${alias}", variable.TextReplace);
                }
            }
        }

        acarsText = Regex.Replace(acarsText, @"\s+", " ");
        acarsText = Regex.Replace(acarsText, @"(?<=\*)(-?[\,0-9]+)", "$1");
        acarsText = Regex.Replace(acarsText, @"(?<=\#)(-?[\,0-9]+)", "$1");
        acarsText = Regex.Replace(acarsText, @"(?<=\+)([A-Z]{3})", "$1");
        acarsText = Regex.Replace(acarsText, @"(?<=\+)([A-Z]{4})", "$1");

        composite.AcarsText = acarsText.ToUpper();
    }

    private void ParseMetar(AtisComposite composite, out Metar metar, out string atisLetter, out List<Variable> variables)
    {
        metar = composite.DecodedMetar;
        var time = NodeParser.Parse<ObservationTimeNode>(metar, composite);
        var surfaceWind = NodeParser.Parse<SurfaceWindNode>(metar, composite);
        var rvr = NodeParser.Parse<RunwayVisualRangeNode>(metar, composite);
        var visibility = NodeParser.Parse<PrevailingVisibilityNode>(metar, composite);
        var presentWeather = NodeParser.Parse<PresentWeatherNode>(metar, composite);
        var clouds = NodeParser.Parse<CloudNode>(metar, composite);
        var temp = NodeParser.Parse<TemperatureNode>(metar, composite);
        var dew = NodeParser.Parse<DewpointNode>(metar, composite);
        var pressure = NodeParser.Parse<AltimeterSettingNode>(metar, composite);
        var trends = NodeParser.Parse<TrendNode>(metar, composite);

        atisLetter = char.Parse(composite.CurrentAtisLetter).LetterToPhonetic();
        var completeWxStringVoice = $"{surfaceWind.VoiceAtis} {visibility.VoiceAtis} {rvr.VoiceAtis} {presentWeather.VoiceAtis} {clouds.VoiceAtis} {temp.VoiceAtis} {dew.VoiceAtis} {pressure.VoiceAtis}";
        var completeWxStringAcars = $"{surfaceWind.TextAtis} {visibility.TextAtis} {rvr.TextAtis} {presentWeather.TextAtis} {clouds.TextAtis} {temp.TextAtis}/{dew.TextAtis} {pressure.TextAtis}";

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
                notamVoice = composite.UseFaaFormat ? "Notices to air missions. " : "Notices to airmen. ";
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

        if (!string.IsNullOrEmpty(notamText) && composite.UseFaaFormat)
        {
            notamText = "NOTAMS... " + notamText;
        }

        var transitionLevelVoice = "";
        var transitionLevelText = "";
        if (!composite.UseFaaFormat)
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

        variables = new List<Variable>
        {
            new Variable("FACILITY", mAirport.ID, mAirport.Name),
            new Variable("ATIS_LETTER", composite.CurrentAtisLetter, atisLetter,  new [] {"LETTER","ATIS_CODE","ID"}),
            new Variable("TIME", time.TextAtis, time.VoiceAtis, new []{"OBS_TIME","OBSTIME"}),
            new Variable("WIND", surfaceWind.TextAtis, surfaceWind.VoiceAtis, new[]{"SURFACE_WIND"}),
            new Variable("RVR", rvr.TextAtis, rvr.VoiceAtis),
            new Variable("VIS", visibility.TextAtis, visibility.VoiceAtis, new[]{"PREVAILING_VISIBILITY"}),
            new Variable("PRESENT_WX", presentWeather.TextAtis, presentWeather.VoiceAtis, new[]{"PRESENT_WEATHER"}),
            new Variable("CLOUDS", clouds.TextAtis, clouds.VoiceAtis),
            new Variable("TEMP", temp.TextAtis, temp.VoiceAtis),
            new Variable("DEW", dew.TextAtis, dew.VoiceAtis),
            new Variable("PRESSURE", pressure.TextAtis, pressure.VoiceAtis, new[]{"QNH"}),
            new Variable("WX", completeWxStringAcars, completeWxStringVoice, new[]{"FULL_WX_STRING"}),
            new Variable("ARPT_COND", airportConditions, airportConditions, new[]{"ARRDEP"}),
            new Variable("NOTAMS", notamText, notamVoice),
            new Variable("TREND", trends.TextAtis, trends.VoiceAtis),
            new Variable("TL", transitionLevelText, transitionLevelVoice),
            new Variable("CLOSING", $" ...ADVS YOU HAVE INFO {composite.CurrentAtisLetter }.", $"ADVISE ON INITIAL CONTACT, YOU HAVE INFORMATION {atisLetter}.")
        };
    }

    private string FormatForTextToSpeech(string input, AtisComposite composite)
    {
        System.Diagnostics.Debug.WriteLine(input);

        // parse zulu times
        input = Regex.Replace(input, @"([0-9])([0-9])([0-9])([0-8])Z",
            m => string.Format($"{ int.Parse(m.Groups[1].Value).NumberToSingular() } " +
                               $"{ int.Parse(m.Groups[2].Value).NumberToSingular() } " +
                               $"{ int.Parse(m.Groups[3].Value).NumberToSingular() } " +
                               $"{ int.Parse(m.Groups[4].Value).NumberToSingular() } zulu"));

        // vhf frequencies
        input = Regex.Replace(input, @"(1\d\d\.\d\d?\d?)", m => Convert.ToDouble(m.Groups[1].Value).DecimalToWordString(composite.UseDecimalTerminology));

        // letters
        input = Regex.Replace(input, @"\*([A-Z]{1,2}[0-9]{0,2})", m => string.Format("{0}", m.Value.ConvertAlphaNumericToWordGroup())).Trim();

        // parse taxiways
        input = Regex.Replace(input, @"\bTWY ([A-Z]{1,2}[0-9]{0,2})\b", m => $"TWY { m.Groups[1].Value.ConvertAlphaNumericToWordGroup() }");
        input = Regex.Replace(input, @"\bTWYS ([A-Z]{1,2}[0-9]{0,2})\b", m => $"TWYS { m.Groups[1].Value.ConvertAlphaNumericToWordGroup() }");

        // parse runways
        input = Regex.Replace(input, @"\b(RY|RWY|RWYS)\s?([0-9]{1,2})([LRC]?)\b", m =>
            StringExtensions.RwyNumbersToWords(int.Parse(m.Groups[2].Value), m.Groups[3].Value,
                prefix: !string.IsNullOrEmpty(m.Groups[1].Value),
                plural: !string.IsNullOrEmpty(m.Groups[1].Value) && m.Groups[1].Value == "RWYS",
                leadingZero: !composite.UseFaaFormat));

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
            foreach (Match m in navaids)
            {
                if (m.Success)
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
        }

        // format airport identifiers
        var airports = Regex.Matches(input, @"(?<=\+)([A-Z0-9]{4})");
        if (airports.Count > 0)
        {
            foreach (Match m in airports)
            {
                if (m.Success)
                {
                    try
                    {
                        var find = mNavData.GetAirport(m.Value);
                        if (find != null)
                        {
                            input = Regex
                                .Replace(input, $@"\b(?<=\+){ m.Value }\b", x => find.Name)
                                .Replace("+", "");
                        }
                    }
                    catch { }
                }
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

    private async void PostIdsUpdate(AtisComposite composite, CancellationToken token)
    {
        if (Debugger.IsAttached)
            return;

        if (string.IsNullOrEmpty(composite.IDSEndpoint) || composite.CurrentPreset == null)
            return;

        try
        {
            var client = new RestClient(composite.IDSEndpoint)
            {
                Timeout = 5000
            };
            var request = new RestRequest
            {
                Method = Method.POST
            };
            var json = new IdsRestRequest
            {
                Facility = composite.Identifier,
                Preset = composite.CurrentPreset.Name,
                AtisLetter = composite.CurrentAtisLetter,
                AirportConditions = composite.CurrentPreset.AirportConditions,
                Notams = composite.CurrentPreset.Notams,
                Timestamp = DateTime.UtcNow,
                Version = Assembly.GetExecutingAssembly().GetName().Version.ToString(),
                AtisType = composite.AtisType
            };
            request.AddParameter("application/json", JsonConvert.SerializeObject(json), ParameterType.RequestBody);
            await client.ExecuteAsync(request, token);
        }
        catch (TaskCanceledException) { }
        catch (Exception ex)
        {
            throw new Exception("PostIdsUpdate Error: " + ex.Message);
        }
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

[Serializable]
internal class IdsRestRequest
{
    public string Facility { get; set; }
    public string Preset { get; set; }
    public string AtisLetter { get; set; }
    public string AirportConditions { get; set; }
    public string Notams { get; set; }
    public DateTime Timestamp { get; set; }
    public string Version { get; set; }
    [JsonConverter(typeof(StringEnumConverter))]
    public AtisType AtisType { get; set; }
}

internal class Variable
{
    public string Find { get; set; }
    public string TextReplace { get; set; }
    public string VoiceReplace { get; set; }
    public string[] Aliases { get; set; }

    public Variable(string find, string textReplace, string voiceReplace, string[] aliases = null)
    {
        Find = find;
        TextReplace = textReplace;
        VoiceReplace = voiceReplace;
        Aliases = aliases;
    }
}