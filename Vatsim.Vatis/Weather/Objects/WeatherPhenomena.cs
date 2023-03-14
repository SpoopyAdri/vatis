using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Vatsim.Vatis.Weather.Enums;
using Vatsim.Vatis.Weather.Extensions;

namespace Vatsim.Vatis.Weather.Objects
{
    /// <summary>
    /// Special weather conditions
    /// </summary>
    [DataContract(Name = "weatherPhenomena")]
    public class WeatherPhenomena
    {
        /// <summary>
        /// Ordered array of weather conditions
        /// </summary>
        [DataMember(Name = "weatherConditions", EmitDefaultValue = false)]
        public WeatherCondition[] WeatherConditions { get; set; }

        /// <summary>
        /// Intensity or proximity
        /// </summary>
        [DataMember(Name = "intensityProximity", EmitDefaultValue = false)]
        public string IntensityProximity { get; set; }

        /// <summary>
        /// Weather descriptor
        /// </summary>
        [DataMember(Name = "descriptor", EmitDefaultValue = false)]
        public string Descriptor { get; set; }

        /// <summary>
        /// Weather type
        /// </summary>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string Type { get; set; }

        /// <summary>
        /// The raw weather phenomena string
        /// </summary>
        [DataMember(Name = "rawValue", EmitDefaultValue = false)]
        public string RawValue { get; set; }

        #region Constructors

        /// <summary>
        /// Default
        /// </summary>
        public WeatherPhenomena() { }

        /// <summary>
        /// Parser constructor
        /// </summary>
        /// <param name="tokens">Weather token</param>
        /// <param name="errors">Parse errors list</param>
        internal WeatherPhenomena(string[] tokens, List<string> errors)
        {
            if (tokens.Length == 0)
            {
                errors.Add("Array of present weather tokens is empty");
                return;
            }

            RawValue = string.Join("", tokens);

            var parsedData = new List<WeatherCondition>();
            var weatherToken = tokens.First();
            var noChangedToken = weatherToken;

            if (weatherToken.Equals("NSW"))
            {
                WeatherConditions = new[] { WeatherCondition.NoSignificantWeather };
                return;
            }

            if (weatherToken.StartsWith("RE"))
                weatherToken = weatherToken.Replace("RE", "");

            if (weatherToken.StartsWith("-"))
            {
                IntensityProximity = weatherToken[..1];
                parsedData.Add(WeatherCondition.Light);
                weatherToken = weatherToken[1..];
            }

            if (weatherToken.StartsWith("+"))
            {
                IntensityProximity = weatherToken[..1];
                parsedData.Add(WeatherCondition.Heavy);
                weatherToken = weatherToken[1..];
            }

            if (weatherToken.StartsWith("VC"))
            {
                IntensityProximity = weatherToken[..2];
                parsedData.Add(WeatherCondition.Vicinity);
                weatherToken = weatherToken[2..];
            }

            var weatherCodes = SplitIntoCodes(weatherToken);
            if (weatherCodes.Length == 0)
            {
                errors.Add($"Cannot parse weather token: \"{noChangedToken}\"");
                return;
            }
            parsedData.AddRange(weatherCodes.Select(EnumTranslator.GetValueByDescription<WeatherCondition>));
            WeatherConditions = parsedData.ToArray();

            if (weatherToken.Length > 2)
            {
                Descriptor = weatherToken[..2];
                weatherToken = weatherToken[2..];

                Type = weatherToken[..2];
                weatherToken = weatherToken[2..];
            }
            else
            {
                string[] descriptors = { "MI", "PR", "BC", "DR", "BL", "SH", "TS", "FZ" };
                if (descriptors.Contains(weatherToken[..2]))
                {
                    Descriptor = weatherToken[..2];
                }
                else
                {
                    Type = weatherToken[..2];
                }
                weatherToken = weatherToken[2..];
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Splits weather string into weather codes
        /// </summary>
        /// <param name="weatherToken">Weather token</param>
        /// <returns></returns>
        private string[] SplitIntoCodes(string weatherToken)
        {
            var length = weatherToken.Length / 2;
            var outcome = new string[length];

            for (var i = 0; i < length; i++)
            {
                outcome[i] = weatherToken.Substring(i * 2, 2);
            }

            return outcome;
        }

        #endregion
    }
}
