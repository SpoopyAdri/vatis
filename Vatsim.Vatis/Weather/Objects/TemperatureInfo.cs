using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Vatsim.Vatis.Weather.Objects
{
    /// <summary>
    /// Information about air temperature and dew point
    /// </summary>
    [DataContract(Name = "temperatureInfo")]
    public class TemperatureInfo
    {
        /// <summary>
        /// Temperature value in Celsius
        /// </summary>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public int? Temperature { get; init; }

        /// <summary>
        /// Temperature dew point in Celsius
        /// </summary>
        [DataMember(Name = "dewPoint", EmitDefaultValue = false)]
        public int? DewPoint { get; init; }

        #region Constructors

        /// <summary>
        /// Default
        /// </summary>
        public TemperatureInfo() { }

        /// <summary>
        /// Parse constructor
        /// </summary>
        /// <param name="tokens">Temperature token</param>
        /// <param name="errors">Errors list</param>
        internal TemperatureInfo(string[] tokens, List<string> errors)
        {
            if (tokens.Length == 0)
            {
                errors.Add("Array with temperature token is empty");
                return;
            }

            var temperatureToken = tokens.First();
            if (string.IsNullOrEmpty(temperatureToken))
            {
                errors.Add("Cannot parse empty temperature token");
                return;
            }

            var values = temperatureToken.Split('/');
            Temperature = GetTemperatureValue(values[0]);
            DewPoint = GetTemperatureValue(values[1]);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Convert temperature value to integer considering sign "M"
        /// </summary>
        /// <param name="stringValue">Temperature value</param>
        /// <returns></returns>

        private int? GetTemperatureValue(string stringValue)
        {
            if (string.IsNullOrEmpty(stringValue))
                return null;

            if (stringValue.Contains("M"))
                stringValue = stringValue.Replace("M", "-");

            return int.Parse(stringValue);
        }

        #endregion
    }
}
